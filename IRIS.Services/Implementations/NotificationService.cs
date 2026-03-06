using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRIS.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IrisDbContext _context;

        public NotificationService(IrisDbContext context)
        {
            _context = context;
        }

        public void MarkNotificationsAsRead(List<int> notificationIds)
        {
            var unreadNotifications = _context.Set<SystemNotification>()
                                              .Where(n => notificationIds.Contains(n.NotificationId) && !n.IsRead)
                                              .ToList();

            foreach (var notification in unreadNotifications)
            {
                notification.IsRead = true;
            }

            _context.SaveChanges();
        }

        public void CheckAllStockLevels()
        {
            // 1. FIX: Use strictly '<' so it perfectly matches your UI table logic
            var lowIngredients = _context.Ingredients
                                         .Where(i => i.CurrentStock < i.MinimumStock)
                                         .ToList();

            foreach (var ingredient in lowIngredients)
            {
                bool isCritical = ingredient.CurrentStock == 0;
                CreateLowStockNotification(ingredient.IngredientId, ingredient.Name, isCritical);
            }

            // 2. AUTO-CLEANUP: Find ingredients that are now well-stocked
            var wellStockedItemIds = _context.Ingredients
                                             .Where(i => i.CurrentStock >= i.MinimumStock)
                                             .Select(i => i.IngredientId)
                                             .ToList();

            // Find active notifications for items that don't need them anymore
            var staleNotifications = _context.SystemNotifications
    .Where(n => n.NotificationType == "LowStock"
             && !n.IsActionTaken
             && n.ReferenceId.HasValue
             && wellStockedItemIds.Contains(n.ReferenceId.Value))
    .ToList();

            // Mark them as resolved so they stop bothering you!
            foreach (var notif in staleNotifications)
            {
                notif.IsActionTaken = true;
                notif.IsRead = true; // Mark as read so it drops the unread count
                notif.ActionTakenByName = "System";
            }

            // Only save if there were stale notifications to clean up
            if (staleNotifications.Any())
            {
                _context.SaveChanges();
            }
        }

        // --- 1. FETCHING NOTIFICATIONS FOR THE UI ---
        public List<NotificationDto> GetNotificationsForUser(User currentUser)
        {
            var query = _context.SystemNotifications.AsQueryable();

            if (currentUser.Role == UserRole.Dean || currentUser.Role == UserRole.AssistantDean)
            {
                query = query.Where(n => n.TargetRole == UserRole.Dean ||
                                         n.TargetRole == UserRole.AssistantDean ||
                                         n.NotificationType == "LowStock");
            }
            else
            {
                query = query.Where(n => n.TargetUserId == currentUser.UserId ||
                                         n.TargetRole == currentUser.Role ||
                                         n.NotificationType == "LowStock");
            }

            var rawNotifications = query.OrderByDescending(n => n.CreatedAt).Take(15).ToList();

            var dtos = new List<NotificationDto>();
            foreach (var n in rawNotifications)
            {
                dtos.Add(new NotificationDto
                {
                    NotificationId = n.NotificationId,
                    Message = n.Message,
                    TimeAgo = CalculateTimeAgo(n.CreatedAt),
                    ShowTakeActionButton = !n.IsActionTaken,
                    ActionTakenText = n.IsActionTaken ? $"(Resolved by {n.ActionTakenByName})" : null,
                    ReferenceId = n.ReferenceId,
                    TargetPage = n.NotificationType == "LowStock" ? "RestockPage" : "RequestControl",
                    IsRead = n.IsRead
                });
            }
            return dtos;
        }

        public int GetUnreadCountForUser(User currentUser)
        {
            var query = _context.SystemNotifications.AsQueryable();

            if (currentUser.Role == UserRole.Dean || currentUser.Role == UserRole.AssistantDean)
            {
                query = query.Where(n => n.TargetRole == UserRole.Dean ||
                                         n.TargetRole == UserRole.AssistantDean ||
                                         n.NotificationType == "LowStock");
            }
            else
            {
                query = query.Where(n => n.TargetUserId == currentUser.UserId ||
                                         n.TargetRole == currentUser.Role ||
                                         n.NotificationType == "LowStock");
            }

            query = query.Where(n => !n.IsRead);

            return query.Count();
        }

        public void ResolveRequestNotification(int requestId, string newStatus, string actionBy)
        {
            var notification = _context.SystemNotifications
                .FirstOrDefault(n => n.ReferenceId == requestId && n.NotificationType == "NewRequest" && !n.IsActionTaken);

            if (notification != null)
            {
                notification.IsActionTaken = true;
                notification.ActionTakenByName = actionBy;

                notification.Message = notification.Message.Replace("New Request", $"{newStatus} Request");
                notification.Message = notification.Message.Replace("Pending", newStatus);

                _context.SaveChanges();
            }
        }

        // --- 2. THE "TAKE ACTION" UPDATE LOGIC ---
        public void MarkActionTaken(int notificationId, string actionTakenBy)
        {
            var notification = _context.SystemNotifications.Find(notificationId);
            if (notification != null && !notification.IsActionTaken)
            {
                notification.IsActionTaken = true;
                notification.ActionTakenByName = actionTakenBy;
                _context.SaveChanges();
            }
        }

        // --- NEW: THE DISMISS LOGIC ---
        public void DismissNotification(int notificationId)
        {
            var notification = _context.SystemNotifications.Find(notificationId);
            if (notification != null)
            {
                // Hard delete approach (Removes from DB so it never shows again):
                _context.SystemNotifications.Remove(notification);

                // ALTERNATIVE Soft Delete approach (if you added an IsHidden or IsDeleted column to your DB entity):
                // notification.IsDeleted = true;
                // notification.IsRead = true; 

                _context.SaveChanges();
            }
        }

        // --- 3. SYSTEM TRIGGERS (Call these from other services) ---
        public void CreateLowStockNotification(int itemId, string itemName, bool isCritical)
        {
            bool alreadyExists = _context.SystemNotifications.Any(n => n.ReferenceId == itemId && n.NotificationType == "LowStock" && !n.IsActionTaken);
            if (alreadyExists) return;

            string alertMessage = isCritical
                ? $"{itemName} is critically out of stock (0 remaining) and requires immediate restocking!"
                : $"{itemName} is running low on stock. Please restock soon.";

            var notif = new SystemNotification
            {
                NotificationType = "LowStock",
                Message = alertMessage,
                ReferenceId = itemId,
            };

            _context.SystemNotifications.Add(notif);
            _context.SaveChanges();
        }

        public void CreateNewRequestNotification(int requestId, string facultyName, string subject)
        {
            var notif = new SystemNotification
            {
                NotificationType = "NewRequest",
                TargetRole = UserRole.Dean,
                Message = $"New Request from {facultyName} for {subject}.",
                ReferenceId = requestId
            };
            _context.SystemNotifications.Add(notif);
            _context.SaveChanges();
        }

        public void CreateStatusUpdateNotification(int requestId, int staffUserId, RequestStatus newStatus, string actionBy)
        {
            var notif = new SystemNotification
            {
                NotificationType = "StatusUpdate",
                TargetUserId = staffUserId,
                Message = $"Your request was marked as {newStatus}.",
                ReferenceId = requestId,
                IsActionTaken = true,
                ActionTakenByName = actionBy
            };
            _context.SystemNotifications.Add(notif);
            _context.SaveChanges();
        }

        // --- HELPER METHOD ---
        private string CalculateTimeAgo(DateTime createdAt)
        {
            TimeSpan timeSince = DateTime.Now - createdAt;
            if (timeSince.TotalMinutes < 1) return "Just now";
            if (timeSince.TotalMinutes < 60) return $"{(int)timeSince.TotalMinutes} mins ago";
            if (timeSince.TotalHours < 24) return $"{(int)timeSince.TotalHours} hours ago";
            if (timeSince.TotalDays < 7) return $"{(int)timeSince.TotalDays} days ago";
            return createdAt.ToString("MMM dd, yyyy");
        }
    }
}
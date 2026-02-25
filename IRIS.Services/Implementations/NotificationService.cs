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
        public void CheckAllStockLevels()
        {

            // Find all ingredients where stock is at or below minimum
            var lowIngredients = _context.Ingredients
                                         .Where(i => i.CurrentStock <= i.MinimumStock)
                                         .ToList();

            foreach (var ingredient in lowIngredients)
            {
                bool isCritical = ingredient.CurrentStock == 0;
                // This will safely create the alert (and ignore it if it already exists!)
                CreateLowStockNotification(ingredient.IngredientId, ingredient.Name, isCritical);
            }
        }

        // --- 1. FETCHING NOTIFICATIONS FOR THE UI ---
        public List<NotificationDto> GetNotificationsForUser(User currentUser)
        {
            var query = _context.SystemNotifications.AsQueryable();

            // LOGIC CHECK: Who is looking?
            if (currentUser.Role == UserRole.Dean || currentUser.Role == UserRole.AssistantDean)
            {
                // Deans see Dean stuff AND LowStock alerts
                query = query.Where(n => n.TargetRole == UserRole.Dean ||
                                         n.TargetRole == UserRole.AssistantDean ||
                                         n.NotificationType == "LowStock");
            }
            else // Assuming OfficeStaff
            {
                // Staff see their own specific alerts AND LowStock alerts
                query = query.Where(n => n.TargetUserId == currentUser.UserId ||
                                         n.TargetRole == currentUser.Role ||
                                         n.NotificationType == "LowStock");
            }

            var rawNotifications = query.OrderByDescending(n => n.CreatedAt).Take(15).ToList();

            // Map to DTO
            var dtos = new List<NotificationDto>();
            foreach (var n in rawNotifications)
            {
                dtos.Add(new NotificationDto
                {
                    NotificationId = n.NotificationId,
                    Message = n.Message,
                    TimeAgo = CalculateTimeAgo(n.CreatedAt),
                    ShowTakeActionButton = !n.IsActionTaken, // If false, show the button!
                    ActionTakenText = n.IsActionTaken ? $"(Resolved by {n.ActionTakenByName})" : null,
                    ReferenceId = n.ReferenceId,

                    // Tell the UI exactly which page to open when clicked
                    TargetPage = n.NotificationType == "LowStock" ? "RestockPage" : "RequestControl"
                });
            }

            return dtos;
        }

        public int GetUnreadCountForUser(User currentUser)
        {
            var query = _context.SystemNotifications.AsQueryable();

            if (currentUser.Role == UserRole.Dean || currentUser.Role == UserRole.AssistantDean)
                query = query.Where(n => n.TargetRole == UserRole.Dean || n.TargetRole == UserRole.AssistantDean || n.NotificationType == "LowStock");
            else
                query = query.Where(n => n.TargetUserId == currentUser.UserId || n.TargetRole == currentUser.Role || n.NotificationType == "LowStock");

            // THE FIX: We added !n.IsActionTaken. It will ONLY count if no one has taken action!
            query = query.Where(n => !n.IsActionTaken &&
                                    (n.Message.Contains("Pending") ||
                                     n.Message.Contains("New") ||
                                     n.NotificationType == "LowStock"));

            return query.Count();
        }
        public void ResolveRequestNotification(int requestId, string newStatus, string actionBy)
        {
            // Find the original "New Request" notification linked to this specific request
            var notification = _context.SystemNotifications
                .FirstOrDefault(n => n.ReferenceId == requestId && n.NotificationType == "NewRequest" && !n.IsActionTaken);

            if (notification != null)
            {
                // 1. Mark it as resolved so the badge ignores it
                notification.IsActionTaken = true;
                notification.ActionTakenByName = actionBy;

                // 2. Magically change the word "New" to "Approved" or "Rejected" in the database!
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

        // --- 3. SYSTEM TRIGGERS (Call these from other services) ---
        public void CreateLowStockNotification(int itemId, string itemName, bool isCritical)
        {
            bool alreadyExists = _context.SystemNotifications.Any(n => n.ReferenceId == itemId && n.NotificationType == "LowStock" && !n.IsActionTaken);
            if (alreadyExists) return;

            // --- NEW: Dynamic Message based on stock level ---
            string alertMessage = isCritical
                ? $"{itemName} is critically out of stock (0 remaining) and requires immediate restocking!"
                : $"{itemName} is running low on stock. Please restock soon.";

            var notif = new SystemNotification
            {
                NotificationType = "LowStock", // Keep as LowStock so your UI badge still counts it!
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
                Message = $"New Request from {facultyName} for {subject}.", // "New" triggers the badge count!
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
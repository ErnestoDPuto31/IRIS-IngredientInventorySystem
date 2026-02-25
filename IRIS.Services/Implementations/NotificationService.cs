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
            // Similar logic to above, but just counting the ones where action is NOT taken yet
            var query = _context.SystemNotifications.Where(n => !n.IsActionTaken);

            if (currentUser.Role == UserRole.Dean || currentUser.Role == UserRole.AssistantDean)
                query = query.Where(n => n.TargetRole == UserRole.Dean || n.TargetRole == UserRole.AssistantDean || n.NotificationType == "LowStock");
            else
                query = query.Where(n => n.TargetUserId == currentUser.UserId || n.TargetRole == currentUser.Role || n.NotificationType == "LowStock");

            return query.Count();
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
        public void CreateLowStockNotification(int itemId, string itemName)
        {
            // Prevent spamming the DB if a low stock notification already exists and isn't resolved
            bool alreadyExists = _context.SystemNotifications.Any(n => n.ReferenceId == itemId && n.NotificationType == "LowStock" && !n.IsActionTaken);
            if (alreadyExists) return;

            var notif = new SystemNotification
            {
                NotificationType = "LowStock",
                Message = $"{itemName} is out of stock and requires immediate restocking.",
                ReferenceId = itemId,
                // TargetRole is null because BOTH Staff and Dean need to see this
            };
            _context.SystemNotifications.Add(notif);
            _context.SaveChanges();
        }

        public void CreateNewRequestNotification(int requestId, string facultyName, string subject)
        {
            var notif = new SystemNotification
            {
                NotificationType = "NewRequest",
                TargetRole = UserRole.Dean, // Only Deans approve requests
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
                TargetUserId = staffUserId, // Specifically targets the staff who made it
                Message = $"Your request was marked as {newStatus}.",
                ReferenceId = requestId,
                IsActionTaken = true, // It's just an FYI, no action needed by staff
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
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Data;
using IRIS.Services.Interfaces;

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
            var lowIngredients = _context.Ingredients
                                         .Where(i => i.CurrentStock < i.MinimumStock)
                                         .ToList();

            foreach (var ingredient in lowIngredients)
            {
                bool isCritical = ingredient.CurrentStock == 0;
                CreateLowStockNotification(ingredient.IngredientId, ingredient.Name, isCritical);
            }

            // AUTO-CLEANUP: Find ingredients that are now well-stocked
            var wellStockedItemIds = _context.Ingredients
                                             .Where(i => i.CurrentStock >= i.MinimumStock)
                                             .Select(i => i.IngredientId)
                                             .ToList();

            // Find stale notifications for items that have been restocked (regardless of ActionTaken)
            var staleNotifications = _context.SystemNotifications
                                             .Where(n => n.NotificationType == "LowStock"
                                                      && n.ReferenceId.HasValue
                                                      && wellStockedItemIds.Contains(n.ReferenceId.Value))
                                             .ToList();

            // Remove them from DB entirely when stock is replenished
            if (staleNotifications.Any())
            {
                _context.SystemNotifications.RemoveRange(staleNotifications);
                _context.SaveChanges();
            }
        }

        // --- 1. FETCHING NOTIFICATIONS FOR THE UI ---
        public List<NotificationDto> GetNotificationsForUser(User currentUser)
        {
            var query = _context.SystemNotifications.AsQueryable();

            // We do NOT filter out IsActionTaken here anymore, so it STAYS VISIBLE!

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

        public void DismissNotification(int notificationId)
        {
            var notification = _context.SystemNotifications.Find(notificationId);
            if (notification != null)
            {
                _context.SystemNotifications.Remove(notification);
                _context.SaveChanges();
            }
        }

        // --- 3. SYSTEM TRIGGERS ---
        public void CreateLowStockNotification(int itemId, string itemName, bool isCritical)
        {
            // FIX: Check if a notification already exists, REGARDLESS of whether you clicked "see now"
            var existingNotif = _context.SystemNotifications
                                        .FirstOrDefault(n => n.ReferenceId == itemId && n.NotificationType == "LowStock");

            string alertMessage = isCritical
                ? $"{itemName} is critically out of stock (0 remaining) and requires immediate restocking!"
                : $"{itemName} is running low on stock. Please restock soon.";

            if (existingNotif != null)
            {
                // If it exists, update the message if it went from "Low" to "Critical"
                if (existingNotif.Message != alertMessage)
                {
                    existingNotif.Message = alertMessage;
                    _context.SaveChanges();
                }

                // STOP HERE. We found an existing notification, so DO NOT create a new one!
                return;
            }

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
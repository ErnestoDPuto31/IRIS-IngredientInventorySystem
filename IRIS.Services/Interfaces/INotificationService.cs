using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using System.Collections.Generic;

namespace IRIS.Services.Interfaces
{
    public interface INotificationService
    {
        void MarkNotificationsAsRead(List<int> notificationIds);
        void CheckAllStockLevels();
        void ResolveRequestNotification(int requestId, string newStatus, string actionBy);
        List<NotificationDto> GetNotificationsForUser(User currentUser);
        int GetUnreadCountForUser(User currentUser);
        void MarkActionTaken(int notificationId, string actionTakenBy); 
        void CreateLowStockNotification(int itemId, string itemName, bool isCritical);

        void CreateNewRequestNotification(int requestId, string facultyName, string subject);
        void CreateStatusUpdateNotification(int requestId, int staffUserId, RequestStatus newStatus, string actionBy);
    }
}
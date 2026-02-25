using IRIS.Domain.Entities;
using IRIS.Domain.Enums;

using System.Collections.Generic;

namespace IRIS.Services.Interfaces
{
    public interface INotificationService
    {
        // For the UI to get the data
        List<NotificationDto> GetNotificationsForUser(User currentUser);
        int GetUnreadCountForUser(User currentUser);

        // For the UI to update the status when they click "Take Action"
        void MarkActionTaken(int notificationId, string actionTakenBy);

        // For the system to trigger new alerts
        void CreateLowStockNotification(int itemId, string itemName);
        void CreateNewRequestNotification(int requestId, string facultyName, string subject);
        void CreateStatusUpdateNotification(int requestId, int staffUserId, RequestStatus newStatus, string actionBy);
    }
}
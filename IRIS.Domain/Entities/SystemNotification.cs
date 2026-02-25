using IRIS.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRIS.Domain.Entities
{
    public class SystemNotification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required, MaxLength(255)]
        public string Message { get; set; } = string.Empty;

        // E.g., "LowStock", "NewRequest", "StatusUpdate"
        [Required, MaxLength(50)]
        public string NotificationType { get; set; } = string.Empty;

        // Links to the ItemId (if stock) or RequestId (if request) so we can open it!
        public int? ReferenceId { get; set; }

        // Who is supposed to see this? (If null, it's for everyone in the TargetRole)
        public int? TargetUserId { get; set; }
        public UserRole? TargetRole { get; set; }

        // --- THE "ACTION TAKEN" TRACKING ---
        public bool IsActionTaken { get; set; } = false;
        public string? ActionTakenByName { get; set; } // e.g., "Resolved by Dean Smith"
        public bool IsRead { get; set; } = false; // Add this line!
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace IRIS.Domain.Entities
    {
        public class NotificationDto
        {
            public int NotificationId { get; set; }
            public string Message { get; set; }
            public string TimeAgo { get; set; }

            // UI Helpers
            public bool IsRead { get; set; } // Add this!
            public bool ShowTakeActionButton { get; set; } // true if not resolved
            public string ActionTakenText { get; set; } // "Action taken by Maria"
            public string TargetPage { get; set; } // "RestockPage" or "RequestPage"
            public int? ReferenceId { get; set; } // The ID to load when they click
        }
    }

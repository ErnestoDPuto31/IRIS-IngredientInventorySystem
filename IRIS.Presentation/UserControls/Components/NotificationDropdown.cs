using IRIS.Domain.Entities;

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class NotificationDropdown : UserControl
    {
        public NotificationDropdown()
        {
            InitializeComponent();
        }

        public void LoadNotifications(List<NotificationDto> notifications)
        {
            flpNotifications.Controls.Clear();

            if (notifications == null || notifications.Count == 0)
            {
                Label emptyLabel = new Label
                {
                    Text = "No new notifications",
                    AutoSize = false,
                    Size = new Size(this.Width - 10, 50),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Gray
                };
                flpNotifications.Controls.Add(emptyLabel);
                return;
            }

            // We will build the individual notification items next!
            // For now, let's just show the text to prove it connects to the database.
            foreach (var notif in notifications)
            {
                Label lbl = new Label
                {
                    Text = $"{notif.Message}\n{notif.TimeAgo}",
                    AutoSize = false,
                    Size = new Size(this.Width - 25, 60),
                    Padding = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle
                };
                flpNotifications.Controls.Add(lbl);
            }
        }
    }
}
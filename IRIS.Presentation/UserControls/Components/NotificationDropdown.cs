using IRIS.Domain.Entities;
using IRIS.Presentation.Forms; // Ensure this is present to recognize MainForm
using IRIS.Presentation.UserControls.PagesUC;
using IRIS.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IRIS.Presentation.UserControls.Components
{
    public partial class NotificationDropdown : UserControl
    {
        public event EventHandler NotificationClicked;

        public NotificationDropdown()
        {
            InitializeComponent();

            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            if (flpNotifications != null)
            {
                flpNotifications.AutoScroll = true;
                flpNotifications.WrapContents = false;
                flpNotifications.FlowDirection = FlowDirection.TopDown;
                flpNotifications.Padding = new Padding(0);
            }
        }

        private Color GetStatusColor(string message)
        {
            if (string.IsNullOrEmpty(message)) return Color.Indigo;

            string msg = message.ToLower();

            // Stock Alert Colors
            if (msg.Contains("critically") || msg.Contains("out of stock")) return Color.Crimson;
            if (msg.Contains("low on stock") || msg.Contains("running low")) return Color.Goldenrod;

            // Request Colors
            if (msg.Contains("approved")) return Color.ForestGreen;
            if (msg.Contains("rejected")) return Color.Crimson;
            if (msg.Contains("released")) return Color.DodgerBlue;
            if (msg.Contains("new") || msg.Contains("pending")) return Color.DarkOrange;

            return Color.Indigo;
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
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 9f, FontStyle.Italic)
                };
                flpNotifications.Controls.Add(emptyLabel);
                return;
            }

            foreach (var notif in notifications)
            {
                Color statusColor = GetStatusColor(notif.Message);

                Panel itemPanel = new Panel
                {
                    Size = new Size(flpNotifications.ClientSize.Width - 5, 60),
                    Margin = new Padding(0),
                    BackColor = Color.White,
                    Cursor = Cursors.Default
                };

                itemPanel.Paint += (s, e) =>
                {
                    using (Pen pen = new Pen(Color.WhiteSmoke, 1))
                    {
                        e.Graphics.DrawLine(pen, 10, itemPanel.Height - 1, itemPanel.Width - 10, itemPanel.Height - 1);
                    }
                };

                Panel circleIndicator = new Panel
                {
                    Size = new Size(8, 8),
                    Location = new Point(12, 16)
                };
                circleIndicator.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (SolidBrush brush = new SolidBrush(statusColor))
                    {
                        e.Graphics.FillEllipse(brush, 0, 0, 8, 8);
                    }
                };

                Label lblTime = new Label
                {
                    Text = notif.TimeAgo,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 8.5f),
                    ForeColor = Color.DimGray
                };

                Label lblAction = new Label
                {
                    Text = "See now.",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                    ForeColor = Color.Indigo,
                    Cursor = Cursors.Hand
                };

                lblAction.MouseEnter += (s, e) => lblAction.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold | FontStyle.Underline);
                lblAction.MouseLeave += (s, e) => lblAction.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);

                // --- NAVIGATION LOGIC ---
                lblAction.Click += (sender, e) =>
                {
                    if (this.ParentForm is MainForm main)
                    {
                        // 1. Navigate to the correct page based on TargetPage property
                        if (notif.TargetPage == "RestockPage")
                        {
                            main.LoadPage(new RestockPage());
                        }
                        else if (notif.TargetPage == "RequestControl")
                        {
                            main.LoadPage(new RequestControl());
                        }

                        // 2. Mark as read so the badge updates
                        var notifService = (INotificationService)Program.Services.GetService(typeof(INotificationService));
                        notifService.MarkActionTaken(notif.NotificationId, UserSession.CurrentUser?.Username ?? "System");
                    }

                    NotificationClicked?.Invoke(this, EventArgs.Empty);
                    this.Visible = false;
                };

                RichTextBox rtbMessage = new RichTextBox
                {
                    Location = new Point(25, 10),
                    Width = itemPanel.Width - 40,
                    BorderStyle = BorderStyle.None,
                    BackColor = Color.White,
                    ReadOnly = true,
                    ScrollBars = RichTextBoxScrollBars.None,
                    Font = new Font("Segoe UI", 9.5f),
                    Cursor = Cursors.Default
                };

                rtbMessage.ContentsResized += (s, e) =>
                {
                    rtbMessage.Height = e.NewRectangle.Height + 2;
                    lblTime.Location = new Point(25, rtbMessage.Bottom + 5);
                    lblAction.Location = new Point(25 + lblTime.PreferredSize.Width + 5, rtbMessage.Bottom + 5);
                    itemPanel.Height = lblTime.Bottom + 12;
                    itemPanel.Invalidate();
                };

                rtbMessage.Text = notif.Message;

                string[] keywords = { "Approved", "Rejected", "Released", "Pending", "New", "Critically", "Low on stock" };
                foreach (var word in keywords)
                {
                    int index = rtbMessage.Text.IndexOf(word, StringComparison.OrdinalIgnoreCase);
                    if (index != -1)
                    {
                        rtbMessage.Select(index, word.Length);
                        rtbMessage.SelectionColor = GetStatusColor(word);
                        rtbMessage.SelectionFont = new Font(rtbMessage.Font, FontStyle.Bold);
                    }
                }
                rtbMessage.DeselectAll();
                rtbMessage.Enter += (s, e) => { itemPanel.Focus(); };

                itemPanel.Controls.Add(circleIndicator);
                itemPanel.Controls.Add(rtbMessage);
                itemPanel.Controls.Add(lblTime);
                itemPanel.Controls.Add(lblAction);

                flpNotifications.Controls.Add(itemPanel);
            }
        }
    }
}
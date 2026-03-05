using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Presentation.UserControls.Components;
using IRIS.Services.Interfaces;
using System.ComponentModel;

namespace IRIS.Presentation.UserControls.PagesUC
{
    public partial class DashboardControl : UserControl
    {
        private INotificationService _notificationService;
        private IRequestService _requestService;
        private IIngredientService _ingredientService;

        public DashboardControl()
        {
            InitializeComponent();
            if (Program.Services != null)
            {
                _notificationService = (INotificationService)Program.Services.GetService(typeof(INotificationService));
                _requestService = (IRequestService)Program.Services.GetService(typeof(IRequestService));
                _ingredientService = (IIngredientService)Program.Services.GetService(typeof(IIngredientService));
            }

            dashboardCardTotalIngredients.TypeOfCard = CardType.TotalIngredients;
            dashboardCardLowStock.TypeOfCard = CardType.LowStockItems;
            dashboardCardPending.TypeOfCard = CardType.PendingRequests;
            dashboardCardApproved.TypeOfCard = CardType.ApprovedRequests;

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            if (UserSession.CurrentUser != null)
            {
                UpdateWelcomeMessage(UserSession.CurrentUser.Role);
            }
            else
            {
                lblWelcomeText.Text = "Welcome to IRIS.";
                lblWelcomeDescription.Text = "System Dashboard";
            }

            RefreshDashboard();
        }

        private void UpdateWelcomeMessage(UserRole role)
        {
            switch (role)
            {
                case UserRole.OfficeStaff:
                    lblWelcomeText.Text = "Welcome, Office Staff.";
                    lblWelcomeDescription.Text = "Manage inventory and encode requests";
                    break;

                case UserRole.AssistantDean:
                    lblWelcomeText.Text = "Welcome, Assistant Dean.";
                    lblWelcomeDescription.Text = "Review and approve pending requests";
                    break;

                case UserRole.Dean:
                    lblWelcomeText.Text = "Welcome, Dean.";
                    lblWelcomeDescription.Text = "Monitor system activities, reports and release ingredients";
                    break;

                case UserRole.QA:
                    lblWelcomeText.Text = "Welcome, Quality Assurance.";
                    lblWelcomeDescription.Text = "Audit compliance and generate reports";
                    break;

                default:
                    lblWelcomeText.Text = "Welcome to IRIS.";
                    lblWelcomeDescription.Text = "System Dashboard";
                    break;
            }
        }

        private void LoadAlerts()
        {
            if (_notificationService == null || UserSession.CurrentUser == null || flowLayoutPanelAlerts == null) return;

            flowLayoutPanelAlerts.SuspendLayout();
            flowLayoutPanelAlerts.Controls.Clear();

            try
            {
                var notifications = _notificationService.GetNotificationsForUser(UserSession.CurrentUser)
                                                        .OrderByDescending(n => n.NotificationId)
                                                        .Take(4)
                                                        .ToList();

                if (notifications.Count == 0)
                {
                    Label emptyLabel = new Label { Text = "No recent alerts.", ForeColor = System.Drawing.Color.Gray, AutoSize = true, Padding = new Padding(10) };
                    flowLayoutPanelAlerts.Controls.Add(emptyLabel);
                }
                else
                {
                    foreach (var notif in notifications)
                    {
                        string safeMessage = string.IsNullOrWhiteSpace(notif.Message)
                                             ? "Unknown System Alert"
                                             : notif.Message;

                        bool isStockAlert = safeMessage.ToLower().Contains("stock");

                        var alertItem = new DashboardAlertItem
                        {
                            Message = safeMessage,
                            DateText = string.IsNullOrWhiteSpace(notif.TimeAgo) ? "Recently" : notif.TimeAgo,
                            BadgeType = isStockAlert ? AlertBadgeType.Stock : AlertBadgeType.Approval,
                            Width = flowLayoutPanelAlerts.Width - 25
                        };

                        flowLayoutPanelAlerts.Controls.Add(alertItem);
                    }
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = new Label { Text = "Error loading alerts: " + ex.Message, ForeColor = System.Drawing.Color.Red, AutoSize = true };
                flowLayoutPanelAlerts.Controls.Add(errorLabel);
            }

            flowLayoutPanelAlerts.ResumeLayout();
        }

        private void LoadOverviews()
        {
            try
            {

                if (statusOverviewStock != null)
                {
                    var allIngredients = _ingredientService.GetAllIngredients().ToList();

                    int outOfStock = allIngredients.Count(i => i.CurrentStock <= 0);
                    int lowStock = allIngredients.Count(i => i.CurrentStock > 0 && i.CurrentStock <= i.MinimumStock);
                    int goodStock = allIngredients.Count(i => i.CurrentStock > i.MinimumStock);

                    statusOverviewStock.Value1 = goodStock;
                    statusOverviewStock.Value2 = lowStock;
                    statusOverviewStock.Value3 = outOfStock;
                }

                // --- REQUEST OVERVIEW ---
                if (statusOverviewRequests != null)
                {
                    var allRequests = _requestService.GetAllRequests();

                    int pending = allRequests.Count(r => r.Status == RequestStatus.Pending);
                    int approved = allRequests.Count(r => r.Status == RequestStatus.Approved);
                    int released = allRequests.Count(r => r.Status == RequestStatus.Released);

                    statusOverviewRequests.Value1 = pending;
                    statusOverviewRequests.Value2 = approved;
                    statusOverviewRequests.Value3 = released;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void RefreshDashboard()
        {
            dashboardCardTotalIngredients.LoadData();
            dashboardCardLowStock.LoadData();
            dashboardCardPending.LoadData();
            dashboardCardApproved.LoadData();

            LoadAlerts();
            LoadOverviews();
        }
    }
}
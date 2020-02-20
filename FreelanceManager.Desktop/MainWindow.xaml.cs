using DataAccess;
using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.View.Bills;
using FreelanceManager.Desktop.View.UserControls;
using FreelanceManager.Desktop.View.WorkProjects;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FreelanceManager.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int HIDDEN_SIDEBAR_SIZE = 20;

        private readonly Dictionary<ButtonSidebar, Action> sidebarButtons;

        private readonly Context _ctx;

        public MainWindow()
        {
            InitializeComponent();

            _ctx = ContextProducer.Ctx.Value;

            sidebarButtons = GetSidebarButtons();
            HideSidebar();

            BillLoaded();

            OpenProjects();
            BtnProjects.Toggle(true);


        }

        private Dictionary<ButtonSidebar, Action> GetSidebarButtons()
        {
            var result = new Dictionary<ButtonSidebar, Action>()
            {
                { BtnProjects, OpenProjects },
                { BtnBills, OpenBills }
            };

            foreach (var b in result)
            {
                b.Key.Click += ToggleSidebarButtonsOff;
                b.Key.Click += b.Value;
            }

            return result;
        }

        private void ToggleSidebarButtonsOff()
        {
            foreach (var b in sidebarButtons.Keys)
            {
                b.Toggle(false);
            }
        }

        private void OpenProjects()
        {
            var projectsController = new WorkProjectsController(_ctx);

            FrameOpt.Content = new WorkProjectsView(projectsController);
        }

        private void OpenBills()
        {
            var billsController = new BillsController(_ctx);
            billsController.BillLoaded += BillLoaded;

            FrameOpt.Content = new BillsView(billsController);
        }

        private void BillLoaded()
        {
            BtnBills.Text = $"Bills ({_ctx.Bill?.Id})";
        }

        private void Sidebar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ShowSidebar();
        }

        private void Sidebar_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            HideSidebar();
        }

        private void ShowSidebar()
        {
            Sidebar.Width = double.NaN;
            SetSidebarTextVisibility(Visibility.Visible);
        }

        private void HideSidebar()
        {
            Sidebar.Width = HIDDEN_SIDEBAR_SIZE;
            SetSidebarTextVisibility(Visibility.Hidden);
        }

        private void SetSidebarTextVisibility(Visibility visibility)
        {
            foreach (var b in sidebarButtons)
            {
                b.Key.TextVisibility = visibility;
            }
        }
    }
}

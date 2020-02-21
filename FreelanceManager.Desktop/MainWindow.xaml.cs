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
        private const int HIDDEN_SIDEBAR_SIZE = 30;

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

        public void ScrollRight()
        {

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
            //TODO: Move to controller

            var controller = new WorkProjectsController(_ctx);

            var uc = new WorkProjectsView(controller);

            controller.BlazeAdded += () => MainScrollViewer.ScrollToRightEnd();
            controller.RemoveDialogRequested += ShowRemoveDialog;

            FrameOpt.Content = uc;
        }

        private void ShowRemoveDialog(IDataSetController s, int o, string n)
        {
            MainPopup.Child = new RemoveDialog(
                n,
                () =>
                {
                    s.Remove(o);
                    MainPopup.IsOpen = false;
                },
                () => MainPopup.IsOpen = false);
            MainPopup.IsOpen = true;
        }

        private void OpenBills()
        {
            //TOOD: Move to controller

            var controller = new BillsController(_ctx);
            controller.BillLoaded += BillLoaded;
            controller.RemoveDialogRequested += ShowRemoveDialog;

            FrameOpt.Content = new BillsView(controller);
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
            MainTitle.Opacity = visibility == Visibility.Visible ? 1 : 0.25;
            foreach (var b in sidebarButtons)
            {
                b.Key.TextVisibility = visibility;
            }
        }
    }
}

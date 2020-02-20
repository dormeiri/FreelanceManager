using DataAccess;
using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.View.Bills;
using FreelanceManager.Desktop.View.UserControls;
using FreelanceManager.Desktop.View.WorkProjects;
using System;
using System.Collections.Generic;
using System.Windows;

namespace FreelanceManager.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<ButtonSidebar, Action> sidebarButtons;

        private readonly Context _ctx;

        public MainWindow()
        {
            InitializeComponent();

            sidebarButtons = GetSidebarButtons();

            _ctx = ContextProducer.Ctx.Value;

            OpenProjects();
            BillLoaded();

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
                b.Key.Click += SidebarButtonClicked;
                b.Key.ClickAction = b.Value;
            }

            return result;
        }

        private void SidebarButtonClicked(ButtonSidebar obj)
        {
            foreach(var b in sidebarButtons.Keys)
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
            LabelLoadedBill.Content = _ctx.Bill?.Id;
        }
    }
}

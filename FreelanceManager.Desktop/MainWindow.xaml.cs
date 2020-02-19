using DataAccess;
using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.View.Bills;
using FreelanceManager.Desktop.View.WorkProjects;
using System.Windows;

namespace FreelanceManager.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Context _ctx;

        public MainWindow()
        {
            InitializeComponent();

            _ctx = ContextProducer.Ctx.Value;

            BillLoaded();
            OpenProjects();
        }


        private void BtnAddProject_Click(object sender, RoutedEventArgs e)
        {
            OpenProjects();
        }

        private void OpenProjects()
        {
            var projectsController = new WorkProjectsController(_ctx);
            var tasksController = new WorkTasksController(_ctx);
            var workTimeRangesController = new WorkTimeRangesController(_ctx);

            FrameOpt.Content = new WorkProjectsView(
                projectsController,
                tasksController,
                workTimeRangesController
            );
        }

        private void BtnBills_Click(object sender, RoutedEventArgs e)
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

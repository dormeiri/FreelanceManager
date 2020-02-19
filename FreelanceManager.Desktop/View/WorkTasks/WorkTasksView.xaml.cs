using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using FreelanceManager.Desktop.View.WorkTimeRanges;
using System;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.WorkTasks
{
    /// <summary>
    /// Interaction logic for WorkProjects.xaml
    /// </summary>
    public partial class WorkTasksView : UserControl
    {
        public Action Done;

        private readonly int _projectId;
        private readonly WorkTimeRangesController _workTimeRangesController;
        private readonly WorkTasksController _workTasksController;

        public WorkTasksView()
        {
            InitializeComponent();
        }

        public WorkTasksView(WorkTasksController controller, WorkTimeRangesController workTimeRangeController, int projectId) : this()
        {
            _projectId = projectId;
            _workTimeRangesController = workTimeRangeController;
            _workTasksController = controller;

            _workTasksController.ListChanged += ListChanged;

            ListChanged();
        }

        private void ListChanged()
        {
            MainListView.ItemsSource = _workTasksController.Get(_projectId);
        }

        private void BtnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var uc = new AddWorkTask(_workTasksController, _projectId);
            uc.Done += ReleaseFrame;

            MainFrame.Content = uc;
        }

        private void BtnRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = GetSelected();

            if (selected != null)
            {
                _workTasksController.Remove(selected.Id);
            }
        }


        private void ReleaseFrame()
        {
            MainFrame.Content = null;
        }

        private void MainListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = GetSelected();
            if (selected != null)
            {
                MainFrame.Content = new WorkTimeRangesView(_workTimeRangesController, selected.Id);
            }
        }

        private WorkTaskDto GetSelected()
        {
            return MainListView.SelectedItem != null ? (WorkTaskDto)MainListView.SelectedItem : null;
        }
    }
}

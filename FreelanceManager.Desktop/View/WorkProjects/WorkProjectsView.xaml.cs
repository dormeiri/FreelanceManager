using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using FreelanceManager.Desktop.View.WorkTasks;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.WorkProjects
{
    /// <summary>
    /// Interaction logic for WorkProjects.xaml
    /// </summary>
    public partial class WorkProjectsView : UserControl
    {
        public Action Done;
        private readonly WorkProjectsController _projectsController;
        private readonly WorkTasksController _tasksController;
        private readonly WorkTimeRangesController _workTimeRangesController;

        public WorkProjectsView()
        {
            InitializeComponent();

        }

        public WorkProjectsView(
            WorkProjectsController projectsController, 
            WorkTasksController tasksController, 
            WorkTimeRangesController workTimeRangesController) : this()
        {
            _projectsController = projectsController;
            _tasksController = tasksController;
            _workTimeRangesController = workTimeRangesController;

            _projectsController.ListChanged += ListChanged;

            ListChanged();
        }

        private void ListChanged()
        {
            MainListView.ItemsSource = _projectsController.Get();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var uc = new AddWorkProject(_projectsController);
            uc.Done += ReleaseFrame;

            MainFrame.Content = uc;
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            _projectsController.Remove(GetSelected().Id);
        }

        private void MainListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowTasksView();
        }

        private void ReleaseFrame()
        {
            MainFrame.Content = null;
        }

        private void ShowTasksView()
        {
            var selected = GetSelected();

            MainFrame.Content = selected != null
                ? new WorkTasksView(_tasksController, _workTimeRangesController, selected.Id)
                : null;
        }

        private WorkProjectDto GetSelected()
        {
            return (WorkProjectDto)MainListView.SelectedItem;
        }
    }
}

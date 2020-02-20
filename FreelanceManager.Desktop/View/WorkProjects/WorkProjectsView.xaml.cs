using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
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
        private readonly WorkProjectsController _controller;

        public WorkProjectsView()
        {
            InitializeComponent();

        }

        public WorkProjectsView(
            WorkProjectsController workProjectsController) : this()
        {
            _controller = workProjectsController;

            _controller.ListChanged += ListChanged;
            _controller.RelatedListChanged += RelatedListChanged;

            ListChanged();
        }

        private void ListChanged()
        {
            MainListView.ItemsSource = _controller.Get();
            RelatedListChanged();
        }

        private void RelatedListChanged()
        {
            LabelHours.Content = _controller.GetTotalHours();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var uc = _controller.GetAddView();
            uc.Done += SetMainFrame;

            MainFrame.Content = uc;
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            _controller.Remove(GetSelected().Id);
        }

        private void MainListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetMainFrame();
        }

        private void SetMainFrame()
        {
            var selected = GetSelected();

            MainFrame.Content = selected != null
                ? _controller.GetWorkTasksView(selected.Id)
                : null;
        }

        private WorkProjectDto GetSelected()
        {
            return (WorkProjectDto)MainListView.SelectedItem;
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            _controller.ExportReport();
        }
    }
}

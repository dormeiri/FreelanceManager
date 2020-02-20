using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.WorkTasks
{
    /// <summary>
    /// Interaction logic for WorkProjects.xaml
    /// </summary>
    public partial class WorkTasksView : UserControl
    {
        public Action Done;

        private readonly WorkTasksController _controller;

        public WorkTasksView()
        {
            InitializeComponent();
        }

        public WorkTasksView(WorkTasksController workTasksController) : this()
        {
            _controller = workTasksController;

            _controller.ListChanged += ListChanged;
            _controller.RelatedListChanged += RelatedListChanged;

            ListChanged();

            TbFilter.Focus();
        }

        private void ListChanged()
        {
            MainListView.ItemsSource = _controller.Get();
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
            var selected = GetSelected();

            if (selected != null)
            {
                _controller.Remove(selected.Id);
            }
        }

        private void MainListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetMainFrame();
        }

        private void SetMainFrame()
        {
            var selected = GetSelected();

            MainFrame.Content = selected == null
                ? null
                : _controller.GetWorkTimeRangesView(selected.Id);
        }

        private WorkTaskDto GetSelected()
        {
            return MainListView.SelectedItem != null ? (WorkTaskDto)MainListView.SelectedItem : null;
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            _controller.ExportReport();
        }

        private void TbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainListView.Items.Filter = IsMatchFilter;
        }

        private bool IsMatchFilter(object x)
        {
            return string.IsNullOrWhiteSpace(TbFilter.Text) 
                || ((WorkTaskDto)x).Name.ToLower().StartsWith(TbFilter.Text.ToLower());
        }
    }
}

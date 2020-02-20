using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using System;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.WorkTimeRanges
{
    /// <summary>
    /// Interaction logic for WorkProjects.xaml
    /// </summary>
    public partial class WorkTimeRangesView : UserControl
    {
        public Action Done;
        private readonly WorkTimeRangesController _controller;

        public WorkTimeRangesView()
        {
            InitializeComponent();
        }

        public WorkTimeRangesView(WorkTimeRangesController workTimeRangesController) : this()
        {
            _controller = workTimeRangesController;

            LabelTask.Content = _controller.GetWorkTaskContext().Name;

            _controller.ListChanged += ListChanged;

            ListChanged();
        }

        private void ListChanged()
        {
            MainListView.ItemsSource = _controller.Get();
            LabelHours.Content = _controller.GetTotalHours();
        }

        private void BtnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var uc = _controller.GetAddView();
            uc.Done += ReleaseFrame;

            MainFrame.Content = uc;
        }

        private void BtnRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = GetSelected();
            if (selected != null)
            {
                _controller.Remove(selected.Id);
            }
        }

        private WorkTimeRangeDto GetSelected()
        {
            return MainListView.SelectedItem != null ? (WorkTimeRangeDto)MainListView.SelectedItem : null;
        }

        private void ReleaseFrame()
        {
            MainFrame.Content = null;
        }
    }
}

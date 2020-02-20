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
        private readonly WorkTimeRangesController _workTimeRangesController;

        public WorkTimeRangesView()
        {
            InitializeComponent();
        }

        public WorkTimeRangesView(WorkTimeRangesController workTimeRangesController) : this()
        {
            _workTimeRangesController = workTimeRangesController;

            _workTimeRangesController.ListChanged += ListChanged;

            ListChanged();
        }

        private void ListChanged()
        {
            MainListView.ItemsSource = _workTimeRangesController.Get();
            LabelHours.Content = _workTimeRangesController.GetTotalHours();
        }

        private void BtnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var uc = _workTimeRangesController.GetAddView();
            uc.Done += ReleaseFrame;

            MainFrame.Content = uc;
        }

        private void BtnRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = GetSelected();
            if (selected != null)
            {
                _workTimeRangesController.Remove(selected.Id);
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

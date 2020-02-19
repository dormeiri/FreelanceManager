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
        private readonly int _workTaskId;
        private readonly WorkTimeRangesController _controller;

        public WorkTimeRangesView()
        {
            InitializeComponent();
        }

        public WorkTimeRangesView(WorkTimeRangesController controller, int workTaskId) : this()
        {
            _workTaskId = workTaskId;
            _controller = controller;

            _controller.ListChanged += ListChanged;

            ListChanged();
        }

        private void ListChanged()
        {
            MainListView.ItemsSource = _controller.Get(_workTaskId);
        }

        private void BtnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var uc = new WorkTimeRangeAddView(_controller, _workTaskId);
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

        private BillDto GetSelected()
        {
            return MainListView.SelectedItem != null ? (BillDto)MainListView.SelectedItem : null;
        }

        private void ReleaseFrame()
        {
            MainFrame.Content = null;
        }
    }
}

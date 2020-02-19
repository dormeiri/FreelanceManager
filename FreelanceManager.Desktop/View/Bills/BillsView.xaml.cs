using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using System;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.Bills
{
    /// <summary>
    /// Interaction logic for WorkProjects.xaml
    /// </summary>
    public partial class BillsView : UserControl
    {
        public Action Done;

        private readonly BillsController _controller;

        public BillsView()
        {
            InitializeComponent();
        }

        public BillsView(BillsController controller) : this()
        {
            _controller = controller;

            _controller.ListChanged += ListChanged;

            ListChanged();
        }

        private void ListChanged()
        {
            MainListView.ItemsSource = _controller.Get();
        }

        private void BtnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var uc = new BillAddView(_controller);
            uc.Done += ReleaseFrame;

            MainFrame.Content = uc;
        }

        private void BtnRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _controller.Remove(GetSelected().Id);
        }


        private void BtnLoad_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _controller.LoadBill(GetSelected().Id);
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

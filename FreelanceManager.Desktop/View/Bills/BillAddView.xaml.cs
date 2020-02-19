using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.Bills
{
    /// <summary>
    /// Interaction logic for AddProject.xaml
    /// </summary>
    public partial class BillAddView : UserControl
    {
        public Action Done;
        private readonly BillsController _controller;
        public BillDto Entity { get; set; }

        public BillAddView()
        {
            InitializeComponent();


        }

        public BillAddView(BillsController controller) : this()
        {
            var now = DateTime.Now;

            var start = new DateTime(now.Year, now.Month, 1);
            var end = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            Entity = new BillDto()
            {
                Start = start,
                End = end
            };

            _controller = controller;

            MainStackPanel.DataContext = Entity;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _controller.Add(Entity);

            Done.Invoke();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Done.Invoke();
        }
    }
}

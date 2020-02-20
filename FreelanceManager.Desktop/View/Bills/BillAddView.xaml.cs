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

        public BillAddView(BillsController controller, BillDto entity) : this()
        {
            Entity = entity;

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

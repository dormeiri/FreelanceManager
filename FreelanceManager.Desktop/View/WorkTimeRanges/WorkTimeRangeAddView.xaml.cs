using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.WorkTimeRanges
{
    /// <summary>
    /// Interaction logic for AddProject.xaml
    /// </summary>
    public partial class WorkTimeRangeAddView : UserControl
    {
        public Action Done;
        private readonly WorkTimeRangesController _controller;
        public WorkTimeRangeDto Entity { get; set; }

        public WorkTimeRangeAddView()
        {
            InitializeComponent();

            DpStart.Focus();
        }

        public WorkTimeRangeAddView(WorkTimeRangesController controller, WorkTimeRangeDto entity) : this()
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

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(c => char.IsDigit(c));
        }
    }
}

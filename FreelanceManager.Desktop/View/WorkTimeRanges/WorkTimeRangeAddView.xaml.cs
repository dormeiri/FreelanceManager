using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using System;
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


        }

        public WorkTimeRangeAddView(WorkTimeRangesController controller, int workTaskId) : this()
        {
            var now = DateTime.Now;
            now = now.Date.AddHours(now.Hour);

            var start = now.AddHours(-1);
            var end = now;

            Entity = new WorkTimeRangeDto()
            {
                Start = start,
                End = end,
                WorkTaskId = workTaskId
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

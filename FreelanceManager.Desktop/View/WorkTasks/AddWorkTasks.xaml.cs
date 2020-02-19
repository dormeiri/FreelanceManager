using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.WorkTasks
{
    /// <summary>
    /// Interaction logic for AddProject.xaml
    /// </summary>
    public partial class AddWorkTask : UserControl
    {
        public Action Done;
        private readonly WorkTasksController _controller;
        public WorkTaskDto Entity { get; set; }

        public AddWorkTask()
        {
            InitializeComponent();
        }

        public AddWorkTask(WorkTasksController controller, int projectId) : this()
        {
            Entity = new WorkTaskDto()
            {
                WorkProjectId = projectId
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

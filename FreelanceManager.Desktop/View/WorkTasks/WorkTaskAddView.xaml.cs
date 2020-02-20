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
    public partial class WorkTaskAddView : UserControl
    {
        public Action Done;
        private readonly WorkTasksController _controller;

        public WorkTaskDto Entity { get; set; }

        public WorkTaskAddView()
        {
            InitializeComponent();

            TbName.Focus();
        }

        public WorkTaskAddView(WorkTasksController controller, WorkTaskDto entity) : this()
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

using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.WorkProjects
{
    /// <summary>
    /// Interaction logic for AddProject.xaml
    /// </summary>
    public partial class AddWorkProject : UserControl
    {
        public Action Done;

        private readonly WorkProjectsController _controller;

        public WorkProjectDto Entity { get; set; }

        public AddWorkProject()
        {
            InitializeComponent();
        }

        public AddWorkProject(WorkProjectsController controller) : this()
        {
            Entity = new WorkProjectDto();
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

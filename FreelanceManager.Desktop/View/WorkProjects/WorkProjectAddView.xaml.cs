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
    public partial class WorkProjectAddView : UserControl
    {
        public Action Done;

        private readonly WorkProjectsController _controller;

        public WorkProjectDto Entity { get; set; }

        public WorkProjectAddView()
        {
            InitializeComponent();

            TbName.Focus();
        }

        public WorkProjectAddView(WorkProjectsController controller, WorkProjectDto entity) : this()
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

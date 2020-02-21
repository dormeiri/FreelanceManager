using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using FreelanceManager.Desktop.View.WorkTasks;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FreelanceManager.Desktop.View.WorkProjects
{
    /// <summary>
    /// Interaction logic for WorkProjects.xaml
    /// </summary>
    public partial class WorkProjectsView : UserControl
    {
        public Action Done;
        private readonly WorkProjectsController _controller;

        public WorkProjectsView()
        {
            InitializeComponent();

        }

        public WorkProjectsView(WorkProjectsController controller) : this()
        {
            _controller = controller;

            _controller.ListChanged += ListChanged;
            _controller.RelatedListChanged += RelatedListChanged;
            MainFrame.ContentRendered += (s, o) => _controller.TriggerBlazeAddedEvent();

            ListChanged();
        }

        private void ListChanged()
        {
            MainListView.ItemsSource = _controller.Get();
            RelatedListChanged();
        }

        private void RelatedListChanged()
        {
            LabelHours.Content = _controller.GetTotalHours();
            if (MainListView.ItemsSource is IEnumerable<WorkProjectDto> s)
            {
                _controller.UpdateStatistics(s);
            }

            CollectionViewSource.GetDefaultView(MainListView.ItemsSource).Refresh();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var uc = _controller.GetAddView();
            uc.Done += SetMainFrame;

            MainFrame.Content = uc;
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var selected = GetSelected();
            if (selected != null)
            {
                _controller.ShowRemoveDialog(_controller, selected.Id, selected.Name);
            }
        }

        private void MainListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetMainFrame();
        }

        private void SetMainFrame()
        {
            var selected = GetSelected();

            WorkTasksView uc;

            if (selected != null)
            {
                uc = _controller.GetWorkTasksView(selected.Id);
                uc.Done += () => MainListView.SelectedIndex = -1;
            }
            else
            {
                uc = null;
            }

            MainFrame.Content = uc;
        }

        private WorkProjectDto GetSelected()
        {
            return (WorkProjectDto)MainListView.SelectedItem;
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            _controller.ExportReport();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Done?.Invoke();
        }
    }
}

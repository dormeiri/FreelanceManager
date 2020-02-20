﻿using FreelanceManager.Desktop.Controllers;
using FreelanceManager.Desktop.Models;
using FreelanceManager.Desktop.View.WorkTimeRanges;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.WorkTasks
{
    /// <summary>
    /// Interaction logic for WorkProjects.xaml
    /// </summary>
    public partial class WorkTasksView : UserControl
    {
        public Action Done;

        private readonly WorkTasksController _controller;

        public WorkTasksView()
        {
            InitializeComponent();
        }

        public WorkTasksView(WorkTasksController controller) : this()
        {
            _controller = controller;

            LabelProject.Content = _controller.GetWorkProjectContext().Name;

            _controller.ListChanged += ListChanged;
            _controller.RelatedListChanged += RelatedListChanged;
            MainFrame.ContentRendered += (s, o) => _controller.TriggerBlazeAddedEvent();

            ListChanged();

            TbFilter.Focus();
        }

        private void ListChanged()
        {
            MainListView.ItemsSource = _controller.Get();
        }

        private void RelatedListChanged()
        {
            LabelHours.Content = _controller.GetTotalHours();
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
                _controller.Remove(selected.Id);
            }
        }

        private void MainListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetMainFrame();
        }

        private void SetMainFrame()
        {
            var selected = GetSelected();

            WorkTimeRangesView uc;

            if (selected != null)
            {
                uc = _controller.GetWorkTimeRangesView(selected.Id);
                uc.Done += () => MainListView.SelectedIndex = -1;
            }
            else
            {
                uc = null;
            }

            MainFrame.Content = uc;
        }

        private WorkTaskDto GetSelected()
        {
            return MainListView.SelectedItem != null ? (WorkTaskDto)MainListView.SelectedItem : null;
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            _controller.ExportReport();
        }

        private void TbFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainListView.Items.Filter = IsMatchFilter;
        }

        private bool IsMatchFilter(object x)
        {
            return string.IsNullOrWhiteSpace(TbFilter.Text) 
                || ((WorkTaskDto)x).Name.ToLower().StartsWith(TbFilter.Text.ToLower());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Done?.Invoke();
        }
    }
}

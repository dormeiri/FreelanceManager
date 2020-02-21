using System;
using System.Windows;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.UserControls
{
    /// <summary>
    /// Interaction logic for RemoveDialog.xaml
    /// </summary>
    public partial class RemoveDialog : UserControl
    {
        public string Item { get; set; }

        private readonly Action _yesAction;
        private readonly Action _noAction;

        public RemoveDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        public RemoveDialog(string item, Action yesAction, Action noAction) : this()
        {
            Item = item;
            _yesAction = yesAction;
            _noAction = noAction;
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            _yesAction?.Invoke();
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            _noAction?.Invoke();
        }
    }
}

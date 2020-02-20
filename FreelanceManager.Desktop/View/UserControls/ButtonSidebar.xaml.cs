using System;
using System.Windows;
using System.Windows.Controls;

namespace FreelanceManager.Desktop.View.UserControls
{
    /// <summary>
    /// Interaction logic for ButtonSidebar.xaml
    /// </summary>
    public partial class ButtonSidebar : UserControl
    {
        public Action Click;

        public static readonly DependencyProperty TextVisibilityProperty = DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(ButtonSidebar), new PropertyMetadata(Visibility.Visible));
        public object TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ButtonSidebar), new PropertyMetadata(""));
        public object Text
        {
            get { return GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }


        public ButtonSidebar()
        {
            InitializeComponent();
            DataContext = this;

            Toggle(false);
        }

        public void Toggle(bool value)
        {
            btn.Style = (Style)(value is bool b && b ? App.Current.Resources["SidebarButtonIn"] : App.Current.Resources["SidebarButton"]);
        }


        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Click?.Invoke();

            Toggle(true);
        }
    }
}

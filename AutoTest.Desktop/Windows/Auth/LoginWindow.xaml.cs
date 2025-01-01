using AutoTest.Desktop.Pages.AuthForPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoTest.Desktop.Windows.Auth
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown();

        private void ShowLogin()
        {
            LoginPage page = new LoginPage();
            LoginPageNavigator.Content = page;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowLogin();
        }
    }
}

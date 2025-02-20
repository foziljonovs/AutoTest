using AutoTest.Desktop.Pages.AuthForPage;
using System.Windows;

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

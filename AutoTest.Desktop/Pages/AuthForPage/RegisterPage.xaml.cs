using AutoTest.Desktop.Windows.Auth;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoTest.Desktop.Pages.AuthForPage
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Register");    
        }

        private void EyeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DisEyeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Application.Current.MainWindow is LoginWindow window)
            {
                window.LoginPageNavigator.Content = new LoginPage();
            }
            else
            {
                MessageBox.Show("Login oynasini yuklashda xatolik yuz berdi!");
            }
        }
    }
}

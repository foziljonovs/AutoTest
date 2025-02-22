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

namespace AutoTest.Desktop.Windows.ProfileForWindows
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();

        private void OldDisEyeButton_Click(object sender, RoutedEventArgs e)
        {
            OldPasswordTxt.Text = OldPasswordPwd.Password; // PasswordBox dan TextBox ga parolni ko'chirish
            OldPasswordPwd.Visibility = Visibility.Collapsed; // PasswordBox ni yashirish
            OldPasswordTxt.Visibility = Visibility.Visible;  // TextBox ni ko'rsatish
            OldEyeButton.Visibility = Visibility.Visible;    // EyeButton ni ko'rsatish
            OldDisEyeButton.Visibility = Visibility.Collapsed; // DisEyeButton ni yashirish
        }

        private void OldEyeButton_Click(object sender, RoutedEventArgs e)
        {
            OldPasswordPwd.Password = OldPasswordTxt.Text; // TextBox dan PasswordBox ga parolni ko'chirish
            OldPasswordTxt.Visibility = Visibility.Collapsed; // TextBox ni yashirish
            OldPasswordPwd.Visibility = Visibility.Visible;  // PasswordBox ni ko'rsatish
            OldEyeButton.Visibility = Visibility.Collapsed;  // EyeButton ni yashirish
            OldDisEyeButton.Visibility = Visibility.Visible; // DisEyeButton ni ko'rsatish
        }

        private void NewEyeButton_Click(object sender, RoutedEventArgs e)
        {
            NewPasswordPwd.Password = NewPasswordTxt.Text; // TextBox dan PasswordBox ga parolni ko'chirish
            NewPasswordTxt.Visibility = Visibility.Collapsed; // TextBox ni yashirish
            NewPasswordPwd.Visibility = Visibility.Visible;  // PasswordBox ni ko'rsatish
            NewEyeButton.Visibility = Visibility.Collapsed;  // EyeButton ni yashirish
            NewDisEyeButton.Visibility = Visibility.Visible; // DisEyeButton ni ko'rsatish
        }

        private void NewDisEyeButton_Click(object sender, RoutedEventArgs e)
        {
            NewPasswordTxt.Text = NewPasswordPwd.Password; // PasswordBox dan TextBox ga parolni ko'chirish
            NewPasswordPwd.Visibility = Visibility.Collapsed; // PasswordBox ni yashirish
            NewPasswordTxt.Visibility = Visibility.Visible;  // TextBox ni ko'rsatish
            NewEyeButton.Visibility = Visibility.Visible;    // EyeButton ni ko'rsatish
            NewDisEyeButton.Visibility = Visibility.Collapsed; // DisEyeButton ni yashirish
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OldPasswordPwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            OldPasswordTxt.Text = OldPasswordPwd.Password;
        }

        private void NewPasswordPwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            NewPasswordTxt.Text = NewPasswordPwd.Password;
        }
    }
}

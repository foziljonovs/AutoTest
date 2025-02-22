using AutoTest.BLL.DTOs.User;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Services.User;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Windows.ProfileForWindows
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public readonly IUserService _service;
        public long UserId { get; set; }
        public ChangePasswordWindow()
        {
            InitializeComponent();
            this._service = new UserService();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 200;
            cfg.DisplayOptions.TopMost = true;
        });

        Notifier notifierThis = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 200;
            cfg.DisplayOptions.TopMost = true;
        });

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.UserId = IdentitySingelton.GetInstance().Id;
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

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveBtn.Visibility = Visibility.Collapsed;
                ChangeLoader.Visibility = Visibility.Visible;

                if(!string.IsNullOrEmpty(OldPasswordTxt.Text) ||
                    !string.IsNullOrEmpty(NewPasswordTxt.Text))
                {
                    var phoneNumber = IdentitySingelton.GetInstance().PhoneNumber;

                    if(string.IsNullOrEmpty(phoneNumber))
                    {
                        notifierThis.ShowWarning("Foydalanuvchi ma'lumotlari topilmadi!");
                        ChangeLoader.Visibility = Visibility.Collapsed;
                        SaveBtn.Visibility = Visibility.Visible;
                        return;
                    }

                    UserChangePasswordDto dto = new UserChangePasswordDto
                    {
                        PhoneNumber = phoneNumber,
                        CurrentPassword = OldPasswordTxt.Text,
                        NewPassword = NewPasswordTxt.Text
                    };

                    var res = await _service.ChangePasswordAsync(UserId, dto);

                    if (res)
                    {
                        notifier.ShowSuccess("Parol muvaffaqiyatli o'zgartirildi!");
                        this.Close();
                    }
                    else
                    {
                        notifierThis.ShowError("Parol o'zgartirishda xatolik yuz berdi!");
                        ChangeLoader.Visibility = Visibility.Collapsed;
                        SaveBtn.Visibility = Visibility.Visible;
                        return;
                    }
                }
                else
                {
                    notifierThis.ShowWarning("Parollar bo'sh bo'lmasligi kerak!");
                    ChangeLoader.Visibility = Visibility.Collapsed;
                    SaveBtn.Visibility = Visibility.Visible;
                    return;
                }
            }
            catch(Exception ex)
            {
                notifierThis.ShowError("Xatolik yuz berdi!");
            }
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

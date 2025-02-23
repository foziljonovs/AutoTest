using AutoTest.BLL.DTOs.Users.User;
using AutoTest.Desktop.Integrated.Servers.Repositories.Auth;
using AutoTest.Desktop.Integrated.Services.Auth;
using AutoTest.Desktop.Windows.Auth;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Pages.AuthForPage
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private readonly IAuthService _authService;
        public RegisterPage()
        {
            InitializeComponent();
            this._authService = new AuthService(new AuthServer());
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

        private async void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegisterBtn.Visibility = Visibility.Collapsed;
                Loader.Visibility = Visibility.Visible;

                if (IsInternetAvailable())
                {
                    if (!string.IsNullOrEmpty(FullnameTxt.Text) ||
                        !string.IsNullOrEmpty(PasswordTxt.Text) ||
                        !string.IsNullOrEmpty(UsernameTxt.Text))
                    {
                        string[] fullname = FullnameTxt.Text.Split(' ');

                        RegisterDto register = new RegisterDto();
                        register.Firstname = fullname[0];
                        register.Lastname = fullname[1];
                        register.PhoneNumber = UsernameTxt.Text;
                        register.Password = PasswordTxt.Text;

                        var res = await _authService.RegisterAsync(register);
                        if (res)
                        {
                            notifier.ShowSuccess("Siz muvaffaqiyatli ro'yxatdan o'tdingiz!");
                            if (Application.Current.MainWindow is LoginWindow window)
                            {
                                window.LoginPageNavigator.Content = new LoginPage();
                            }
                        }
                        else
                        {
                            notifier.ShowError("Ro'yxatdan o'tishda xatolik yuz berdi!");
                            Loader.Visibility = Visibility.Collapsed;
                            RegisterBtn.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        notifier.ShowWarning("Malumotlar to'liq kiritilmagan!");
                        Loader.Visibility = Visibility.Collapsed;
                        RegisterBtn.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    notifier.ShowWarning("Internet aloqasi yo'q!");
                    Loader.Visibility = Visibility.Collapsed;
                    RegisterBtn.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                notifier.ShowError($"Xatolik yuz berdi!");
                Loader.Visibility = Visibility.Collapsed;
                RegisterBtn.Visibility = Visibility.Visible;
            }
        }

        private bool IsInternetAvailable()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send("www.google.com");
                    return reply.Status == IPStatus.Success;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void EyeButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordTxt.Text = PasswordPwb.Password;
            PasswordPwb.Visibility = Visibility.Collapsed;
            PasswordTxt.Visibility = Visibility.Visible;
            EyeButton.Visibility = Visibility.Collapsed;
            DisEyeButton.Visibility = Visibility.Visible;
        }

        private void DisEyeButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordPwb.Password = PasswordTxt.Text;
            PasswordTxt.Visibility = Visibility.Collapsed;
            PasswordPwb.Visibility = Visibility.Visible;
            DisEyeButton.Visibility = Visibility.Collapsed;
            EyeButton.Visibility = Visibility.Visible;
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

        private void PasswordPwb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordTxt.Text = PasswordPwb.Password;
        }
    }
}

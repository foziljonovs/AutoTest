using AutoTest.BLL.DTOs.User;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Repositories.Auth;
using AutoTest.Desktop.Integrated.Services.Auth;
using AutoTest.Desktop.Windows;
using AutoTest.Desktop.Windows.Auth;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly IAuthService _authService;
        public LoginPage()
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

        private void EyeButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordTxt.Text = PasswordPwd.Password;
            PasswordPwd.Visibility = Visibility.Collapsed;
            PasswordTxt.Visibility = Visibility.Visible;
            EyeButton.Visibility = Visibility.Collapsed;
            DisEyeButton.Visibility = Visibility.Visible;
        }

        private void DisEyeButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordPwd.Password = PasswordTxt.Text;
            PasswordTxt.Visibility = Visibility.Collapsed;
            PasswordPwd.Visibility = Visibility.Visible;
            DisEyeButton.Visibility = Visibility.Collapsed;
            EyeButton.Visibility = Visibility.Visible;
        }

        private void SetIdentityToken(string token)
        {
            var tkn = TokenHandler.ParseToken(token);
            var identity = IdentitySingelton.GetInstance();
            identity.Token = token;
            identity.Id = tkn.Id;
            identity.PhoneNumber = tkn.PhoneNumber;
            //identity.RoleName = tkn.RoleName;
            identity.Name = tkn.Name;
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginBtn.Visibility = Visibility.Collapsed;
            Loader.Visibility = Visibility.Visible;

            try
            {
                if (IsInternetAvailable())
                {
                    if (!string.IsNullOrEmpty(PhoneNumberTxt.Text) ||
                        !string.IsNullOrEmpty(PasswordTxt.Text))
                    {
                        LoginDto login = new LoginDto();

                        login.PhoneNumber = PhoneNumberTxt.Text;
                        login.Password = PasswordPwd.Password.ToString();

                        (bool result, string token) res = await _authService.LoginAsync(login);

                        if (res.result)
                        {
                            TokenHandler.ParseToken(res.token);
                            SetIdentityToken(res.token);

                            var currentWindow = Window.GetWindow(this);

                            MainWindow window = new MainWindow();
                            window.Show();

                            currentWindow.Close();
                        }
                        else
                        {
                            notifier.ShowError("Login yoki parol noto'g'ri!");
                            Loader.Visibility = Visibility.Collapsed;
                            LoginBtn.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        notifier.ShowWarning("Login yoki parol kiritilmagan!");
                        Loader.Visibility = Visibility.Collapsed;
                        LoginBtn.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    notifier.ShowWarning("Internet aloqasi yo'q!");
                    Loader.Visibility = Visibility.Collapsed;
                    LoginBtn.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                notifier.ShowError("Xatolik yuz berdi!");
                Loader.Visibility = Visibility.Collapsed;
                LoginBtn.Visibility = Visibility.Visible;
            }
        }

        private bool IsInternetAvailable()
        {
            try
            {
                return true;
                //using(Ping ping = new Ping())
                //{
                //    PingReply reply = ping.Send("www.google.com");
                //    return reply.Status == IPStatus.Success;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Application.Current.MainWindow is LoginWindow window)
            {
                window.LoginPageNavigator.Content = new RegisterPage();
            }
            else
            {
                MessageBox.Show("Login oynasini yuklashda xatolik yuz berdi!");
            }
        }

        private void PasswordPwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordTxt.Text = PasswordPwd.Password;
        }
    }
}

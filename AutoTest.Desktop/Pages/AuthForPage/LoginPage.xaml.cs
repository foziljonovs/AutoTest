using AutoTest.BLL.DTOs.User;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Servers.Repositories.Auth;
using AutoTest.Desktop.Integrated.Services.Auth;
using AutoTest.Desktop.Windows;
using AutoTest.Desktop.Windows.Auth;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void EyeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DisEyeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SetIdentityToken(string token)
        {
            var tkn = TokenHandler.ParseToken(token);
            var identity = IdentitySingelton.GetInstance();
            identity.Token = token;
            identity.Id = tkn.Id;
            identity.PhoneNumber = tkn.PhoneNumber;
            identity.RoleName = tkn.RoleName;
            identity.Firstname = tkn.Firstname;
            identity.Lastname = tkn.Lastname;

        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(IsInternetAvailable())
                {
                    if(!string.IsNullOrEmpty(PhoneNumberTxt.Text) ||
                        !string.IsNullOrEmpty(PasswordPwd.Password.ToString()))
                    {
                        LoginDto login = new LoginDto();

                        login.PhoneNumber = PhoneNumberTxt.Text;
                        login.Password = PasswordPwd.Password.ToString();
                            
                        (bool result, string token) res = await _authService.LoginAsync(login);

                        if(res.result)
                        {
                            TokenHandler.ParseToken(res.token);
                            SetIdentityToken(res.token);

                            string role = IdentitySingelton.GetInstance().RoleName;
                            
                            if(role == "User")
                            {
                                var currentWindow = Window.GetWindow(this);

                                MainWindow window = new MainWindow();
                                window.Show();

                                currentWindow.Close();
                            }
                            else
                            {
                                MessageBox.Show("Sizga ruxsat yo'q!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Login yoki parol noto'g'ri!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Login yoki parol kiritilmagan!");
                    }
                }
                else
                {
                    MessageBox.Show("Internet aloqasi yo'q!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsInternetAvailable()
        {
            try
            {
                using(Ping ping = new Ping())
                {
                    PingReply reply = ping.Send("www.google.com");
                    return reply.Status == IPStatus.Success;
                }
            }
            catch(Exception ex)
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
    }
}

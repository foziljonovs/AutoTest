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
    /// Interaction logic for VerifyPasswordWindow.xaml
    /// </summary>
    public partial class VerifyPasswordWindow : Window
    {
        private readonly IUserService _service;
        public long UserId { get; set; }
        public VerifyPasswordWindow()
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

        private async void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SubmitBtn.Visibility = Visibility.Collapsed;
                Loader.Visibility = Visibility.Visible;

                if(!string.IsNullOrEmpty(PasswordTxt.Text))
                {
                    var res = await _service.VerifyPasswordAsync(this.UserId, PasswordTxt.Text);

                    if(res)
                    {
                        notifier.ShowSuccess("Parol to'g'ri.");
                        this.Close();
                    }
                    else
                    {
                        notifierThis.ShowWarning("Parol noto'g'ri!");
                        Loader.Visibility = Visibility.Collapsed;
                        SubmitBtn.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    notifierThis.ShowWarning("Parolni kiriting!");
                    Loader.Visibility = Visibility.Collapsed;
                    SubmitBtn.Visibility = Visibility.Visible;
                }

            }
            catch(Exception ex)
            {
                notifierThis.ShowWarning("Xatolik yuz berdi!");
                Loader.Visibility = Visibility.Collapsed;
                SubmitBtn.Visibility = Visibility.Visible;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();
    }
}

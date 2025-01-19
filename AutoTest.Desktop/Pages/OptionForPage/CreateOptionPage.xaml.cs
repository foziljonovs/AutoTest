using AutoTest.BLL.DTOs.Tests.Option;
using AutoTest.Desktop.Components.OptionForComponents;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Pages.OptionForPage
{
    /// <summary>
    /// Interaction logic for CreateOptionPage.xaml
    /// </summary>
    public partial class CreateOptionPage : Page
    {
        private readonly List<OptionDto> options;
        public CreateOptionPage()
        {
            InitializeComponent();
        }

        Notifier notifierThis = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.TopRight,
            offsetX: 50,
            offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 200;
            cfg.DisplayOptions.TopMost = true;
        });

        public List<OptionDto> GetOptions()
        {
            if (options.Any())
                return options;
            else
                return new List<OptionDto>();
        }

        private void AddOptionBtn_Click(object sender, RoutedEventArgs e)
        {
            OptionDto option = new OptionDto();

            if(!string.IsNullOrEmpty(txtName.Text))
            {
                option.Text = txtName.Text;

                if(rbIsCorrect.IsChecked == true)
                    option.IsCorrect = true;
                else
                    option.IsCorrect = false;

                CreateOptionComponent component = new CreateOptionComponent();
                component.SetValues(option);
                stOptions.Children.Add(component);
                options.Add(option);
            }
            else
            {
                notifierThis.ShowWarning("Javobni kiriting!");
            }
        }
    }
}

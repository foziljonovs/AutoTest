using AutoTest.BLL.DTOs.Tests.Option;
using AutoTest.Desktop.Components.OptionForComponents;
using System.Windows;
using System.Windows.Controls;
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
        private readonly List<OptionDto> options = new List<OptionDto>();
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

        private void Clear()
        {
            txtName.Clear();
            rbIsCorrect.IsChecked = false;
        }

        public void AddOptions(List<OptionDto> questionOptions)
        {
            if (questionOptions.Any())
            {
                EmptyData.Visibility = Visibility.Collapsed;
                foreach (var option in questionOptions)
                {
                    options.Add(option);

                    CreateOptionComponent component = new CreateOptionComponent();
                    component.SetValues(option);
                    stOptions.Children.Add(component);
                }
            }
            else
            {
                notifierThis.ShowWarning("Javoblarni olishda muammo yuz berdi!");
            }
        }

        private void AddOptionBtn_Click(object sender, RoutedEventArgs e)
        {
            EmptyData.Visibility = Visibility.Collapsed;
            OptionDto option = new OptionDto();

            if(!string.IsNullOrEmpty(txtName.Text))
            {
                option.Text = txtName.Text;
                option.IsChange = false;

                if(rbIsCorrect.IsChecked == true)
                    option.IsCorrect = true;
                else
                    option.IsCorrect = false;

                CreateOptionComponent component = new CreateOptionComponent();
                component.SetValues(option);
                stOptions.Children.Add(component);
                options.Add(option);
                Clear();
            }
            else
            {
                notifierThis.ShowWarning("Javobni kiriting!");
            }
        }
    }
}

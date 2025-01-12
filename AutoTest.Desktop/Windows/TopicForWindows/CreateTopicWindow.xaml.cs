using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Integrated.Services.Topic;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Windows.TopicForWindows
{
    /// <summary>
    /// Interaction logic for CreateTopicWindow.xaml
    /// </summary>
    public partial class CreateTopicWindow : Window
    {
        public Func<Task> CreateTopicTask { get; set; }
        private readonly ITopicService _topicService;
        public CreateTopicWindow()
        {
            InitializeComponent();
            this._topicService = new TopicService();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 200; 
            cfg.DisplayOptions.TopMost = true;
        });


        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveBtn.Visibility = Visibility.Collapsed;
            BtnLoader.Visibility = Visibility.Visible;

            CreateTopicDto dto = new CreateTopicDto();
            if (!string.IsNullOrEmpty(txtName.Text) &&
                !string.IsNullOrEmpty(txtDescription.Text))
            {
                dto.Name = txtName.Text;
                dto.Description = txtDescription.Text;

                var result = await _topicService.AddAsync(dto);

                if (result)
                {
                    notifier.ShowSuccess(dto.Name + " muvaffaqiyatli yaratildi.");
                    CreateTopicTask?.Invoke();
                    this.Close();
                }
                else
                {
                    BtnLoader.Visibility = Visibility.Collapsed;
                    SaveBtn.Visibility = Visibility.Visible;
                    notifier.ShowError(dto.Name + " yaratishda xatolik yuz berdi!");
                }
            }
            else
            {
                BtnLoader.Visibility = Visibility.Collapsed;
                SaveBtn.Visibility = Visibility.Visible;
                notifier.ShowWarning("Malumotlar to'liq emas!");
            }
        }
    }
}

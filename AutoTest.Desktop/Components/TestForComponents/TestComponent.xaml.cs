using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.Windows.TestForWindows;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Components.TestForComponents
{
    /// <summary>
    /// Interaction logic for TestComponent.xaml
    /// </summary>
    public partial class TestComponent : UserControl
    {
        private readonly ITestService _testService;
        public Func<Task> TestDeleted {  get; set; }
        private TestDto Test { get ; set; }
        public TestComponent()
        {
            InitializeComponent();
            this._testService = new TestService();
        }

        private long Id { get; set; }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: System.Windows.Application.Current.MainWindow,
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

        public void SetValues(TestDto test, long number)
        {
            Id = test.Id;
            tbNumber.Text = number.ToString();
            tbTitle.Text = test.Title;
            tbLevel.Text = test.Level.ToString();
            tbStatus.Text = test.Status.ToString();
            tbTopic.Text = test.Topics.FirstOrDefault()?.Name ?? "?";
            Test = test;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateTestWindow window = new UpdateTestWindow();
            window.SelectTest(Test);
            window.ShowDialog();
            TestDeleted?.Invoke();
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Id != 0)
            {
                var result = MessageBox.Show($"Testni o'chirishni istaysizmi?", "Tasdiqlash", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bool isDeleted = await _testService.DeleteAsync(Id);
                    if (isDeleted)
                    {
                        TestDeleted?.Invoke();
                        notifier.ShowSuccess("Test muvaffaqiyatli o'chirildi.");
                    }
                    else
                    {
                        notifier.ShowError($"Testni o'chirishda hatolik yuz berdi!");
                    }
                }
            }
            else
            {
                notifier.ShowWarning("Test tanlanmagan qayta urinib ko'ring.");
            }
        }
    }
}

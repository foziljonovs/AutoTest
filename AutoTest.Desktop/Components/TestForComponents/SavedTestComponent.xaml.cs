using AutoTest.Desktop.Integrated.Services.User;
using AutoTest.Desktop.Windows.TestForWindows;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Components.TestForComponents;

/// <summary>
/// Interaction logic for SavedTestComponent.xaml
/// </summary>
public partial class SavedTestComponent : UserControl
{
    private readonly ISavedTestService _service;
    public long SavedTestId { get; set; }
    public long TestId { get; set; }
    public Func<Task> SavedTestDelegate { get; set; }
    public SavedTestComponent()
    {
        InitializeComponent();
        this._service = new SavedTestService();
    }

    Notifier notifier = new Notifier(cfg =>
    {
        cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive) ?? Application.Current.MainWindow,
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

    public void SetValues(int number, long id, string title, string author, string level, long testId)
    {
        this.SavedTestId = id;
        this.TestId = testId;
        tbNumber.Text = number.ToString();
        tbTitle.Text = title;
        tbAuthor.Text = author;
        tbLevel.Text = level;
    }

    private async void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        try
        {
            if(SavedTestId > 0)
            {
                var message = MessageBox.Show("O'chirilsinmi?", "Tasdiqlash", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if(message == MessageBoxResult.Yes)
                {
                    var res = await _service.DeleteAsync(SavedTestId);

                    if (res)
                    {
                        notifier.ShowSuccess("Muvaffaqiyatli o'chirildi!");
                        SavedTestDelegate?.Invoke();
                    }
                    else
                    {
                        notifier.ShowWarning("O'chirib bo'madi!");
                    }
                }
            }
            else
            {
                notifier.ShowWarning("Qayta urining");
            }
        }
        catch(Exception ex)
        {
            notifier.ShowError("Xatolik yuz berdi!");
        }
    }

    private void ViewButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        TestViewWindow window = new TestViewWindow();
        window.SetTestId(TestId);
        window.Show();
    }
}

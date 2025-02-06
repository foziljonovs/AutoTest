using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.Desktop.Integrated.Services.Question;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Components.QuestionForComponents
{
    /// <summary>
    /// Interaction logic for QuestionComponent.xaml
    /// </summary>
    public partial class QuestionComponent : UserControl
    {
        private readonly IQuestionService _service;
        public Func<Task> IsDeleted { get; set; }
        public QuestionComponent()
        {
            InitializeComponent();
            this._service = new QuestionService();
        }
        private QuestionDto Question { get; set; }
        private long QuestionId { get; set; }

        Notifier notifierThis = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive) ?? Application.Current.MainWindow,
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

        public void SetValues(QuestionDto question, int number)
        {
            this.Question = question;
            this.QuestionId = question.Id;
            tbNumber.Text = number.ToString();
            tbProblem.Text = question.Problem;
            tbType.Text = question.Type.ToString();
            tbTestName.Text = question.Test.Title.ToString();
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(this.QuestionId > 0)
                {
                    var message = MessageBox.Show("Savolni o'chirishni istaysizmi?", "Tasdiqlash", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(message == MessageBoxResult.Yes)
                    {
                        var result = await _service.DeleteAsync(QuestionId);
                        if (result)
                        {
                            IsDeleted?.Invoke();
                            notifierThis.ShowSuccess("Savol muvaffaqiyatli o'chirildi");
                        }
                        else
                        {
                            notifierThis.ShowWarning("Savolni o'chirishda xatolik yuz berdi!");
                        }
                    }
                }
                else
                {
                    notifierThis.ShowWarning("Savolni o'chirishga xatolik, qayta urinib ko'ring.");
                }
            }
            catch(Exception ex)
            {
                notifierThis.ShowError("Savolni o'chirishda xatolik yuz berdi!");
            }
        }
    }
}

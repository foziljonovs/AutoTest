using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.Desktop.Pages.OptionForPage;
using AutoTest.Domain.Enums;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Windows.QuestionForWIndows
{
    /// <summary>
    /// Interaction logic for UpdateQuestionWindow.xaml
    /// </summary>
    public partial class UpdateQuestionWindow : Window
    {
        public UpdateQuestionWindow()
        {
            InitializeComponent();
        }
        public QuestionDto Question { get; set; }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: System.Windows.Application.Current.MainWindow,
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

        public void GetQuestion(QuestionDto dto)
        {
            if(dto is not null)
            {
                Question = dto;
                PageNavigator();
                Get(dto.Problem, dto.Type);
            }
            else
            {
                notifierThis.ShowWarning("Savol ma'lumotlarini olishda xatolik yuz berdi!");
                this.Hide();
            }
        }

        private void PageNavigator()
        {
            CreateOptionPage page = new CreateOptionPage();
            page.AddOptions(Question.Options);
            OptionPageNavigator.Content = page;
        }

        private void Get(string problem, QuestionType type)
        {
            if (!string.IsNullOrEmpty(problem))
            {
                txtProblem.Text = problem;

                var questionType = type switch
                {
                    QuestionType.Multiple => rbMultiple.IsChecked = true,
                    QuestionType.FIllInTheBlank => rbFillInTheBlank.IsChecked = true,
                    QuestionType.TrueFalse => rbTrueOrFalse.IsChecked = true,
                    _ => false
                };

                if (questionType == false)
                    notifierThis.ShowWarning("Savol turi tanlanmagan!");
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();
    }
}

using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Desktop.Components.OptionForComponents;
using AutoTest.Desktop.Integrated.Services.Question;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.Integrated.Services.User;
using AutoTest.Domain.Entities.Tests;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Pages.TestForPage;

/// <summary>
/// Interaction logic for SolutionPage.xaml
/// </summary>
public partial class SolutionPage : Page
{
    private readonly ITestSolutionService _testSolutionService;
    private readonly IQuestionSolutionService _questionSolutionService;
    private readonly IUserTestSolutionService _userTestSolutionService;
    private readonly ITestService _testService;
    private TestDto Test { get; set; }
    private long TestId { get; set; }
    private Dictionary<int, Question> Questions { get; set; } = new Dictionary<int, Question>();
    private char[] Characters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J' };
    private int maxQuestionCount = 0;
    private int nextQuestion = 1;
    public SolutionPage()
    {
        InitializeComponent();
        this._testSolutionService = new TestSolutionService();
        this._questionSolutionService = new QuestionSolutionService();
        this._userTestSolutionService = new UserTestSolutionService();
        this._testService = new TestService();
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

    public void SetTestId(long testId)
        => this.TestId = testId;

    private async Task GetTestAsync()
    {
        var test = await Task.Run(async () => await _testService.GetByIdAsync(TestId));
        if (test == null)
            notifierThis.ShowInformation("Test yuklashda xatolik yuz berdi!");

        GetValues(test);
    }

    private void GetValues(TestDto test)
    {
        int count = 1;

        if(test is not null)
        {
            this.Test = test;

            foreach(var question in test.Question)
            {
                AddQuestion(count, question);
                count++;
            }

            maxQuestionCount = count;
            tbProblem.Text = test.Title;
            ShowQuestion();
        }
    }

    private void ShowQuestion()
    {
        if(nextQuestion > 0)
        {
            if(Questions.TryGetValue(nextQuestion, out Question question))
            {
                if (question != null)
                {
                    stOptions.Children.Clear();

                    for (int i = 0; i < question.Options.Count(); i++)
                    {
                        SolutionOptionComponent component = new SolutionOptionComponent();
                        component.SetOption(
                            Characters[i],
                            question.Options[i].Id,
                            question.Id,
                            question.Options[i].Text,
                            question.Options[i].IsCorrect);

                        stOptions.Children.Add(component);
                    }
                }
                else
                {
                    notifierThis.ShowWarning("Savolni yuklashda xatolik yuz berdi!");
                }
            }
            else
            {
                notifierThis.ShowWarning("Xatolik yuz berdi!");
            }
        }
        else
        {
            notifierThis.ShowWarning("Savollar mavjud emas!");
        }
    }

    private bool AddQuestion(int number, Question question)
    {
        if (question is not null)
        {
            Questions.Add(number, question);
            return true;
        }
        else
            return false;
    }

    private SolutionOptionComponent solutionOptionComponent = null!;
    public async void AddSolutionOption(SolutionOptionComponent component)
    {
        if(solutionOptionComponent != null)
            solutionOptionComponent.st_Border.Background = Brushes.White;

        component.st_Border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6"));
        solutionOptionComponent = component;
    }
    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await GetTestAsync();
    }
}

using AutoTest.BLL.DTOs.Tests.QuestionSolution;
using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.DTOs.Tests.TestSolution;
using AutoTest.BLL.DTOs.Users.UserTestSolution;
using AutoTest.Desktop.Components.OptionForComponents;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Services.Question;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.Integrated.Services.User;
using AutoTest.Desktop.Windows.TestForWindows;
using AutoTest.Domain.Entities.Tests;
using System.Threading.Tasks;
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
    private long TestSolutionId { get; set; }
    private Dictionary<int, Question> Questions { get; set; } = new Dictionary<int, Question>();
    private char[] Characters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J' };
    private int maxQuestionCount = 0;
    private int nextQuestion = 1;
    private Question? currentQuestion { get; set; }
    private long SelectedOptionId { get; set; }
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
            ShowQuestion();
        }
    }

    private void Clear()
    {
        tbProblem.Text = "";
        stOptions.Children.Clear();
    }

    private void ShowQuestion()
    {
        if(nextQuestion > 0)
        {
            if(Questions.TryGetValue(nextQuestion, out Question question))
            {
                if (question != null)
                {
                    Clear();
                    tbProblem.Text = question.Problem;
                    currentQuestion = question;

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
        var selectedOptionId = component.GetOptionId();

        if (selectedOptionId > 0)
            SelectedOptionId = selectedOptionId;
    }

    private async Task CreateTestSolution()
    {
        if(TestId > 0)
        {
            CreateTestSolutionDto dto = new CreateTestSolutionDto();
            dto.TestId = TestId;
            dto.Score = 0;

            var res = await _testSolutionService.AddAsync(dto);
            if(res > 0)
            {
                TestSolutionId = res;
                notifierThis.ShowInformation("Test natijalari saqlanadi!");
            }
            else
                notifierThis.ShowSuccess("Xatolik yuz berdi, sahifani qayta yuklang!");
        }
        else
            notifierThis.ShowInformation("Xatolik yuz berdi!");
    }

    private async Task CreateUserTestSolution()
    {
        if(TestSolutionId > 0)
        {
            CreateUserTestSolutionDto dto = new CreateUserTestSolutionDto();
            var userId = IdentitySingelton.GetInstance().Id;

            if(userId > 0)
            {
                dto.TestSolutionId = TestSolutionId;
                dto.UserId = userId;

                var res = await _userTestSolutionService.AddAsync(dto);
                if (res > 0)
                    notifierThis.ShowSuccess("Test natijalari bog'landi");
                else
                    notifierThis.ShowError("Test natijalarini sizga bog'lashda xatolik yuz berdi!");
            }
            else
                notifierThis.ShowWarning("Shaxsiy ma'lumotlaringiz bilan ishlashda xatolik yuz berdi!");
        }
        else
            notifierThis.ShowWarning("Xatolik yuz berdi!");
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await GetTestAsync();
        await CreateTestSolution();
        await CreateUserTestSolution();
    }

    private void NextQuestion()
    {
        nextQuestion++;

        if (nextQuestion <= maxQuestionCount && Questions.ContainsKey(nextQuestion))
            ShowQuestion();
        else if (nextQuestion == maxQuestionCount)
        {
            var parentWindow = Window.GetWindow(this);
            if (parentWindow is TestSolutionWindow window)
            {
                CompletedTestSolution page = new CompletedTestSolution();
                page.SetTestSolutionId(TestSolutionId);
                window.PageNavigator.Content = page;
            }

            notifierThis.ShowInformation("Test savollari tugadi");
        }
        else
            notifierThis.ShowWarning("Xatolik yuz bermoqda!");
    }

    private async void IntakeBtn_Click(object sender, RoutedEventArgs e)
    {
        if(TestSolutionId > 0)
        {
            CreateQuestionSolutionDto dto = new CreateQuestionSolutionDto();

            if(currentQuestion is not null)
                dto.QuestionId = currentQuestion.Id;
            else
            {
                notifierThis.ShowWarning("Savol aniqlanmadi!");
                return;
            }    
            
            dto.TestSolutionId = TestSolutionId;

            if(SelectedOptionId > 0)
                dto.SelectedOptionId = SelectedOptionId;
            else
            {
                notifierThis.ShowWarning("Javob tanlanmagan!");
                return;
            }
                
            dto.IsCorrect = solutionOptionComponent.ChooseOptionResult();
            if (dto.IsCorrect)
                dto.Score = 5;
            else
                dto.Score = 0;

            var res = await _questionSolutionService.AddAsync(dto);
            if (res)
            {
                NextQuestion();
            }
            else
                notifierThis.ShowError("Xatolik yuz berdi! qayta urining");
        }
        else
        {
            notifierThis.ShowInformation("Xatolik yuz berdi!");
        }
    }

    private void NextBtn_Click(object sender, RoutedEventArgs e)
    {
        NextQuestion();
    }
}

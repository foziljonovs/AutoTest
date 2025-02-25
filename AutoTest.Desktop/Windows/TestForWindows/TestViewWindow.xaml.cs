using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.DTOs.Users.SavedTest;
using AutoTest.Desktop.Components.MainForComponents;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.Integrated.Services.User;
using AutoTest.Domain.Entities.Tests;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Windows.TestForWindows;

/// <summary>
/// Interaction logic for TestViewWindow.xaml
/// </summary>
public partial class TestViewWindow : Window
{
    private readonly ITestService _testService;
    private readonly ISavedTestService _savedtestService;
    private TestDto Test { get; set; }
    private long TestId { get; set; }
    public List<SavedTestDto> SavedTests { get; set; }
    public TestViewWindow()
    {
        InitializeComponent();
        this._testService = new TestService();
        this._savedtestService = new SavedTestService();
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
    private async Task GetAllSavedTestByTestId()
    {
        var savedTests = await Task.Run(async () => await _savedtestService.GetAllByTestIdAsync(TestId));
        if (savedTests != null)
        {
            SavedTests = savedTests.ToList();
            tbSavedTestCount.Text = savedTests.Count().ToString();
        }
        else
        {
            savedTests = new List<SavedTestDto>();
            tbSavedTestCount.Text = "0";
        }
    }
    private async Task GetByTestId()
    {
        var test = await Task.Run(async () => await _testService.GetByIdAsync(TestId));
        if(test != null)
        {
            Test = test;
            ShowTestInformation(test);
            await GetAllSavedTestByTestId();
            ShowQuestions(test.Question);
        }
        else
        {
            notifierThis.ShowError("Test topilmadi!");
        }
    }

    public void SetTestId(long testId)
    {
        TestId = testId;
    }
    private void ShowQuestions(List<Question> questions)
    {
        QuestionsLoader.Visibility = Visibility.Visible;
        int count = 1;

        if (questions.Any())
        {
            QuestionsLoader.Visibility = Visibility.Collapsed;
            EmptyQuestionLbl.Visibility = Visibility.Collapsed;

            foreach (var question in questions)
            {
                MainQuestionComponent component = new MainQuestionComponent();
                component.SetValues(count, question.Id, question.Problem, question.Type.ToString());
                stQuestions.Children.Add(component);
                count++;
            }
        }
        else
        {
            QuestionsLoader.Visibility = Visibility.Collapsed;
            EmptyQuestionLbl.Visibility = Visibility.Visible;
            notifierThis.ShowInformation("Savollar mavjud emas!");
        }
    }
    private void ShowTestInformation(TestDto test)
    {
        tbTitle.Text = test.Title;
        tbAuthor.Text = test.User.Firstname + " " + test.User.Lastname;
        tbLevel.Text = test.Level.ToString();
    }

    private void CloseBtn_Click(object sender, RoutedEventArgs e)
        => this.Close();

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        GetByTestId();
    }

    private async void SavedTestBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            long userId = IdentitySingelton.GetInstance().Id;

            CreatedSavedTestDto dto = new CreatedSavedTestDto
            {
                TestId = TestId,
                UserId = userId
            };

            var res = await _savedtestService.AddAsync(dto);
            if (res)
            {
                notifierThis.ShowSuccess("Test muvaffaqiyatli saqlandi!");
                await GetAllSavedTestByTestId();
            }
            else
            {
                notifierThis.ShowWarning("Testni saqlanmado, qayta urining!");
            }
        }
        catch (Exception ex)
        {
            notifierThis.ShowError("Xatolik yuz berdi!");
        }
    }
}

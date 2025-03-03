using AutoTest.BLL.DTOs.Tests.TestSolution;
using AutoTest.Desktop.Integrated.Services.Test;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Pages.TestForPage;

/// <summary>
/// Interaction logic for CompletedTestSolution.xaml
/// </summary>
public partial class CompletedTestSolution : Page
{
    private long TestSolutionId { get; set; }
    private readonly ITestSolutionService _service;
    public CompletedTestSolution()
    {
        InitializeComponent();
        this._service = new TestSolutionService();
    }

    Notifier notifierThis = new(cfg =>
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

    public void SetTestSolutionId(long testSolutionId)
        => this.TestSolutionId = testSolutionId;

    private async Task GetTestSolution()
    {
        var testSolution = await Task.Run(async () => await _service.GetByIdAsync(TestSolutionId));
        if (testSolution == null)
            notifierThis.ShowWarning("Test natijalari yuklashda xatolik yuz berdi!");

        ShowTestSolution(testSolution);
    }

    private void ShowTestSolution(TestSolutionDto dto)
    {
        if(dto != null)
        {
            var isCorrectTrue = dto.QuestionSolutions.Count(x => x.IsCorrect == true);
            var allScore = dto.QuestionSolutions.Sum(x => x.Score); 

            tbIsCorrectTrue.Text = isCorrectTrue.ToString();
            tbAllScore.Text = allScore.ToString();
        }
        else
        {
            tbIsCorrectTrue.Text = "0";
            tbAllScore.Text = "0";
        }
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await GetTestSolution();
    }
}

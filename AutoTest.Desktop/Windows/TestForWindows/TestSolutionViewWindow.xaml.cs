using AutoTest.BLL.DTOs.Tests.TestSolution;
using AutoTest.Desktop.Components.TestForComponents;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.Integrated.Services.User;
using System.Windows;

namespace AutoTest.Desktop.Windows.TestForWindows;

/// <summary>
/// Interaction logic for TestSolutionViewWindow.xaml
/// </summary>
public partial class TestSolutionViewWindow : Window
{
    private readonly IUserTestSolutionService _userTestSolutionService;
    private readonly ITestSolutionService _testSolutionService;
    public TestSolutionViewWindow()
    {
        InitializeComponent();
        this._userTestSolutionService = new UserTestSolutionService();
        this._testSolutionService = new TestSolutionService();
    }

    private async Task GetAllTestSolution()
    {
        var testSolutions = await Task.Run(async () => await _testSolutionService.GetAllAsync());
        
        if (!testSolutions.Any())
        {
            TestSolutionLoader.Visibility = Visibility.Collapsed;
            EmptyData.Visibility = Visibility.Visible;
            return;
        }

        var userId = IdentitySingelton.GetInstance().Id;
        var userTestSolutions = await Task.Run(async () => await _userTestSolutionService.GetAllByUserIdAsync(userId));
        if (!userTestSolutions.Any())
        {
            TestSolutionLoader.Visibility = Visibility.Collapsed;
            EmptyData.Visibility = Visibility.Visible;
            return;
        }
        else
        {
            var solutions = testSolutions.Where(x => userTestSolutions.Any(uts => uts.TestSolutionId == x.Id)).ToList();
            ShowTestSolution(solutions);
        }
    }

    private void ShowTestSolution(List<TestSolutionDto> testSolutions)
    {
        int count = 1;
        if (testSolutions.Any())
        {
            TestSolutionLoader.Visibility = Visibility.Collapsed;
            EmptyData.Visibility = Visibility.Collapsed;

            foreach(var testSolution in testSolutions)
            {
                var isCorrectTrueOption = testSolution.QuestionSolutions.Where(x => x.IsCorrect == true).Count();
                var score = testSolution.QuestionSolutions.Sum(x => x.Score);

                TestSolutionComponent component = new TestSolutionComponent();
                component.SetValues(count, testSolution.Id, testSolution.Test.Title, isCorrectTrueOption, score);
                stTestSolutions.Children.Add(component);
                count++;
            }
        }
        else
        {
            TestSolutionLoader.Visibility = Visibility.Collapsed;
            EmptyData.Visibility = Visibility.Visible;
        }
    }

    private void CloseBtn_Click(object sender, RoutedEventArgs e)
        => this.Close();

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await GetAllTestSolution();
    }
}

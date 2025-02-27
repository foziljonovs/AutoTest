using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.BLL.DTOs.Tests.Test;
using System.Windows.Controls;

namespace AutoTest.Desktop.Pages.TestForPage;

/// <summary>
/// Interaction logic for SolutionPage.xaml
/// </summary>
public partial class SolutionPage : Page
{
    public TestDto Test { get; set; }
    public long TestId { get; set; }
    public Dictionary<int, QuestionDto> Questions { get; set; }
    public SolutionPage()
    {
        InitializeComponent();
    }

    public void SetTestId(long testId)
        => this.TestId = testId;
}

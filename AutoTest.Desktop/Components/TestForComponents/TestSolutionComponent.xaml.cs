using System.Windows.Controls;

namespace AutoTest.Desktop.Components.TestForComponents;

/// <summary>
/// Interaction logic for TestSolutionComponent.xaml
/// </summary>
public partial class TestSolutionComponent : UserControl
{
    private long Id { get; set; }
    public TestSolutionComponent()
    {
        InitializeComponent();
    }

    public void SetValues(int number, long id, string title, int testSolutionQuestionCount, int score)
    {
        this.Id = id;
        tbNumber.Text = number.ToString();
        tbTitle.Text = title;
        tbScore.Text = score.ToString();
        tbScore.Text = score.ToString();
    }
}

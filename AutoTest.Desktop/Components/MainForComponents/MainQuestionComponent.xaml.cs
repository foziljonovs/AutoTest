using AutoTest.BLL.DTOs.Tests.Question;
using System.Windows.Controls;

namespace AutoTest.Desktop.Components.MainForComponents;

/// <summary>
/// Interaction logic for MainQuestionComponent.xaml
/// </summary>
public partial class MainQuestionComponent : UserControl
{
    public long QuestionId { get; set; }
    public MainQuestionComponent()
    {
        InitializeComponent();
    }

    public void SetValues(int number, long id, string problem, string type)
    {
        QuestionId = id;
        tbNumber.Text = number.ToString();
        tbProblem.Text = problem;
        tbType.Text = type;
    }
}

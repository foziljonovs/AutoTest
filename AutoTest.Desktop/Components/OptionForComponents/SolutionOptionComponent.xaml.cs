using System.Windows.Controls;
using System.Windows.Input;

namespace AutoTest.Desktop.Components.OptionForComponents;

/// <summary>
/// Interaction logic for SolutionOptionComponent.xaml
/// </summary>
public partial class SolutionOptionComponent : UserControl
{
    private long OptionId { get; set; }
    private long QuestionId { get; set; }
    private bool IsCorrect { get; set; }
    public SolutionOptionComponent()
    {
        InitializeComponent();
    }

    public void SetOption(char character, long optionId, long questionId, string option, bool isCorrect)
    {
        this.OptionId = optionId;
        this.QuestionId = questionId;
        this.IsCorrect = isCorrect;
        tbCharacter.Text = character.ToString();
        tbOption.Text = option;
    }

    public bool ChooseOptionResult()
        => this.IsCorrect;

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {

    }
}

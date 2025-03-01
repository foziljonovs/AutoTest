using AutoTest.Desktop.Pages.TestForPage;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        var page = FindParentPage(this);

        if(page is SolutionPage solutionPage)
            solutionPage.AddSolutionOption(this);
    }

    private Page FindParentPage(DependencyObject child)
    {
        DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        if (parentObject is Page page)
            return page;
        else if(parentObject != null)
            return FindParentPage(parentObject);

        return null!;
    }
}

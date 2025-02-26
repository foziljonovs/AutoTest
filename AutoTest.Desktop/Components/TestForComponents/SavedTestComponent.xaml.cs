using System.Windows.Controls;

namespace AutoTest.Desktop.Components.TestForComponents;

/// <summary>
/// Interaction logic for SavedTestComponent.xaml
/// </summary>
public partial class SavedTestComponent : UserControl
{
    public long SavedTestId { get; set; }
    public SavedTestComponent()
    {
        InitializeComponent();
    }

    public void SetValues(int number, long id, string title, string author, string level)
    {
        this.SavedTestId = id;
        tbNumber.Text = number.ToString();
        tbTitle.Text = title;
        tbAuthor.Text = author;
        tbLevel.Text = level;
    }
}

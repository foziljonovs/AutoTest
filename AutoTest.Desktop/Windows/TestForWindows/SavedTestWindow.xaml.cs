using AutoTest.Desktop.Components.TestForComponents;
using AutoTest.Desktop.Integrated.Services.User;
using System.Windows;

namespace AutoTest.Desktop.Windows.TestForWindows;

/// <summary>
/// Interaction logic for SavedTestWindow.xaml
/// </summary>
public partial class SavedTestWindow : Window
{
    private readonly ISavedTestService _service;
    public long UserId { get; set; }
    public SavedTestWindow()
    {
        InitializeComponent();
        this._service = new SavedTestService();
    }

    private void CloseWindow_Click(object sender, RoutedEventArgs e)
        => this.Close();

    private async Task GetAllSavedTests()
    {
        stSavedTest.Children.Clear();
        int count = 1;

        var savedTests = await _service.GetAllByUserIdAsync(UserId);
        if (savedTests.Any())
        {
            SavedTestLoader.Visibility = Visibility.Collapsed;

            foreach(var savedTest in savedTests)
            {
                SavedTestComponent component = new SavedTestComponent();
                component.Tag = savedTest;
                component.SetValues(
                    count,
                    savedTest.Id,
                    savedTest.Test.Title ?? "Nomalum",
                    savedTest.User.Firstname ?? "Nomalum",
                    savedTest.Test.Level.ToString(),
                    savedTest.TestId);

                component.SavedTestDelegate = GetAllSavedTests;
                stSavedTest.Children.Add(component);
                count++;
            }
        }
        else
        {
            SavedTestLoader.Visibility = Visibility.Collapsed;
            EmptyData.Visibility = Visibility.Visible;
        }
    }

    public void SetUserId(long userId)
    {
        this.UserId = userId;
        GetAllSavedTests();
    }
}

using AutoTest.Desktop.Pages.TestForPage;
using System.Windows;

namespace AutoTest.Desktop.Windows.TestForWindows
{
    /// <summary>
    /// Interaction logic for TestSolutionWindow.xaml
    /// </summary>
    public partial class TestSolutionWindow : Window
    {
        public TestSolutionWindow()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            StartBtn.Visibility = Visibility.Collapsed;
            StopBtn.Visibility = Visibility.Visible;
            CloseBtn.Visibility = Visibility.Collapsed;

            PageNavigator.Navigate(new SolutionPage());
        }
    }
}

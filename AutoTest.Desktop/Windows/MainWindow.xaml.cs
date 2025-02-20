using AutoTest.Desktop.Pages.MainForPage;
using AutoTest.Desktop.Pages.ProfileForPage;
using AutoTest.Desktop.Pages.TestForPage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace AutoTest.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage page = new MainPage();
            PageNavigator.Content = page;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainPage page = new MainPage();
            PageNavigator.Content = page;
        }

        private void TestBtn_Click(object sender, RoutedEventArgs e)
        {
            TestPage page = new TestPage();
            PageNavigator.Content = page;
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage page = new ProfilePage();
            PageNavigator.Content = page;
        }

        private void ButtonMouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;

            var heightAnimation = new DoubleAnimation()
            {
                To = 50,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            button.BeginAnimation(Button.HeightProperty, heightAnimation);
        }

        private void ButtonMouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;

            var heightAnimation = new DoubleAnimation()
            {
                To = 40,
                Duration = TimeSpan.FromSeconds(0.2)
            };

            button.BeginAnimation(Button.HeightProperty, heightAnimation);
        }
    }
}

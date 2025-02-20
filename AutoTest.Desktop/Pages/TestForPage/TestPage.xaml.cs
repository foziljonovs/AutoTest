using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Desktop.Components.TestForComponents;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.Windows.TestForWindows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutoTest.Desktop.Pages.TestForPage
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        private readonly ITestService _testService;
        public TestPage()
        {
            InitializeComponent();
            this._testService = new TestService();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllTest();
        }

        private async Task GetAllTest()
        {
            st_tests.Children.Clear();
            TestLoader.Visibility = Visibility.Visible;

            long userId = IdentitySingelton.GetInstance().Id;

            var tests = await Task.Run(async () => await _testService.GetAllByUserIdAsync(userId));

            ShowTests(tests);
        }

        private void ShowTests(List<TestDto> tests)
        {
            int count = 1;
            if (tests.Any())
            {
                TestLoader.Visibility = Visibility.Collapsed;
                TestEmptyData.Visibility = Visibility.Collapsed;

                foreach (var test in tests)
                {
                    TestComponent component = new TestComponent();
                    component.Tag = test;
                    component.TestDeleted = GetAllTest;
                    component.SetValues(test, count);
                    st_tests.Children.Add(component);
                    count++;
                }
            }
            else
            {
                TestLoader.Visibility = Visibility.Collapsed;
                TestEmptyData.Visibility = Visibility.Visible;
            }
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CreateTestWindow window = new CreateTestWindow();
            window.ShowDialog();
            GetAllTest();
        }
    }
}

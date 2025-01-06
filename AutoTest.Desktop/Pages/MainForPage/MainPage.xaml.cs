using AutoTest.Desktop.Components.MainForComponents;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Test;
using AutoTest.Desktop.Integrated.Servers.Repositories.Test;
using AutoTest.Desktop.Integrated.Services.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoTest.Desktop.Pages.MainForPage
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly ITestService _testService;
        public MainPage()
        {
            InitializeComponent();
            this._testService = new TestService(new TestServer());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllTest();
            ShowTopics();
        }

        private async Task GetAllTest()
        {
            st_tests.Children.Clear();
            TestLoader.Visibility = Visibility.Visible;

            var tests = await Task.Run(async () => await _testService.GetAllAsync());
            
            int count = 1;

            if(tests.Count > 0)
            {
                TestLoader.Visibility = Visibility.Collapsed;
                TestEmptyData.Visibility = Visibility.Collapsed;

                foreach(var test in tests)
                {
                    MainTestComponent component = new MainTestComponent();
                    component.Tag = test;
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

        private void ShowTests()
        {
            st_tests.Children.Clear();

            for(int i = 0; i < 10; i++)
            {
                MainTestComponent component = new MainTestComponent();
                st_tests.Children.Add(component);
            }
        }

        private List<string> topics = new List<string>
        {
            "C#",
            "java",
            "Ruby",
            "PHP",
            "Go",
            "Java Script",
            "HTML",
            "Css"
        };

        private void ShowTopics()
        {
            st_topics.Children.Clear();

            foreach(var item in topics)
            {
                MainTopicComponents component  = new MainTopicComponents();
                component.SetValues(item);
                st_topics.Children.Add(component);
            }
        }
    }
}

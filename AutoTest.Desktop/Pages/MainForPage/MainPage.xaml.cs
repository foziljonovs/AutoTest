using AutoTest.Desktop.Components.MainForComponents;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Test;
using AutoTest.Desktop.Integrated.Servers.Repositories.Test;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.Integrated.Services.Topic;
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
        private readonly ITopicService _topicService;
        public MainPage()
        {
            InitializeComponent();
            this._testService = new TestService();
            this._topicService = new TopicService();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GetAllTest();
            await GetAllTopic();
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

        private async Task GetAllTopic()
        {
            st_topics.Children.Clear();
            TopicLoader.Visibility = Visibility.Visible;

            var topics = await Task.Run(async () => await _topicService.GetAllAsync());

            int allTopicCount = 0;

            if(topics.Count > 0)
            {
                TopicLoader.Visibility = Visibility.Collapsed;
                TopicEmptyData.Visibility = Visibility.Collapsed;

                foreach(var topic in topics)
                {
                    MainTopicComponents component = new MainTopicComponents();
                    component.Tag = topic;
                    component.SetValues(topic);
                    st_topics.Children.Add(component);
                    allTopicCount++;
                }
            }
            else
            {
                TopicLoader.Visibility = Visibility.Collapsed;
                TopicEmptyData.Visibility = Visibility.Visible;
            }
        }
    }
}

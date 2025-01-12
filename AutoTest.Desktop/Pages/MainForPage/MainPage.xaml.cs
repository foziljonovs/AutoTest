using AutoTest.Desktop.Components.MainForComponents;
using AutoTest.Desktop.Integrated.Servers.Interfaces.Test;
using AutoTest.Desktop.Integrated.Servers.Repositories.Test;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.Integrated.Services.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Pages.MainForPage
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly ITestService _testService;
        private readonly ITopicService _topicService;
        private long selectedTopicId {  get; set; }
        public MainPage()
        {
            InitializeComponent();
            this._testService = new TestService();
            this._topicService = new TopicService();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 200;
            cfg.DisplayOptions.TopMost = true;
        });

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllTest();
            GetAllTopic();
        }

        private async Task GetAllTest()
        {
            st_tests.Children.Clear();
            TestLoader.Visibility = Visibility.Visible;

            if(IsInternetAvailable())
            {
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
            else
            {
                TestLoader.Visibility = Visibility.Collapsed;
                TestEmptyData.Visibility = Visibility.Visible;
                notifier.ShowWarning("Internetni tekshiring!");
            }
        }

        private async Task GetAllTopic()
        {
            st_topics.Children.Clear();
            TopicLoader.Visibility = Visibility.Visible;
            if(IsInternetAvailable())
            {
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
            else
            {
                TopicLoader.Visibility = Visibility.Collapsed;
                TopicEmptyData.Visibility = Visibility.Visible;
                notifier.ShowWarning("Internetni tekshiring!");
            }
        }

        private MainTopicComponents selectedComponent = null!;
        public void AddTopic(MainTopicComponents component)
        {
            if (selectedComponent != null)
                selectedComponent.st_border.Background = Brushes.White;

            component.st_border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6B6B6"));
            selectedComponent = component;

            if(selectedTopicId != component.GetId())
                selectedTopicId = component.GetId();
        }

        private bool IsInternetAvailable()
        {
            try
            {
                return true;
                //using(Ping ping = new Ping())
                //{
                //    PingReply reply = ping.Send("www.google.com");
                //    return (reply.Status == IPStatus.Success);
                //}
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}

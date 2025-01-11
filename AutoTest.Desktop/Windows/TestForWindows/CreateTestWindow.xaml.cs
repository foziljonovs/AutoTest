using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Components.MainForComponents;
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
using System.Windows.Shapes;

namespace AutoTest.Desktop.Windows.TestForWindows
{
    /// <summary>
    /// Interaction logic for CreateTestWindow.xaml
    /// </summary>
    public partial class CreateTestWindow : Window
    {
        private readonly ITopicService _topicService;
        public CreateTestWindow()
        {
            InitializeComponent();
            this._topicService = new TopicService();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Clear();
            txtDesctiption.Clear();
            txtTopicFilter.Clear();
            LevelComboBox.SelectedItem = -1;
        }

        private async Task GetAllTopic()
        {
            st_AllTopic.Children.Clear();
            AllTopicLoader.Visibility = Visibility.Visible;

            var topics = await Task.Run(async () => await _topicService.GetAllAsync());
            ShowTopics(topics);
        }

        private void ShowTopics(List<TopicDto> topics)
        {
            AllTopicLoader.Visibility = Visibility.Collapsed;
            if (topics.Any())
            {
                TopicEmptyData.Visibility = Visibility.Collapsed;

                foreach (var topic in topics)
                {
                    MainTopicComponents component = new MainTopicComponents();
                    component.Tag = topic;
                    component.SetValues(topic);
                    st_AllTopic.Children.Add(component);
                }
            }
            else
            {
                TopicEmptyData.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllTopic();
        }
    }
}

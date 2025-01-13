using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Components.MainForComponents;
using AutoTest.Desktop.Integrated.Services.Topic;
using AutoTest.Domain.Entities.Tests;
using System.Windows;

namespace AutoTest.Desktop.Windows.TestForWindows
{
    /// <summary>
    /// Interaction logic for UpdateTestWindow.xaml
    /// </summary>
    public partial class UpdateTestWindow : Window
    {
        private readonly ITopicService _topicService;
        private TestDto test { get; set; }
        private List<Topic> selectedTopics { get; set; } = new List<Topic>();
        public UpdateTestWindow()
        {
            InitializeComponent();
            this._topicService = new TopicService();
        }

        private async Task GetAllTopic()
        {
            AllTopicLoader.Visibility = Visibility.Visible;

            var topics = await Task.Run(async () => await _topicService.GetAllAsync());

            ShowTopics(topics);

            ChosesTopics();
        }

        private void ShowTopics(List<TopicDto> topics)
        {
            if(topics.Any())
            {
                AllTopicLoader.Visibility = Visibility.Collapsed;
                TopicEmptyData.Visibility = Visibility.Collapsed;

                foreach(var topic in topics)
                {
                    MainTopicComponents component = new MainTopicComponents();
                    component.Tag = topic;
                    component.SetValues(topic);
                    st_AllTopic.Children.Add(component);
                }
            }
            else
            {
                AllTopicLoader.Visibility = Visibility.Collapsed;
                TopicEmptyData.Visibility = Visibility.Visible;
            }
        }

        public void SelectTest(TestDto test)
        {
            this.test = test;
            txtTitle.Text = test.Title;
            txtDesctiption.Text = test.Description;
            LevelComboBox.SelectedValue = test.Level;
            selectedTopics = test.Topics;
        }

        public void AddTopic(MainTopicComponents component, TopicDto topic)
        {
            var topicId = component.GetId();
            if (!selectedTopics.Any(x => x.Id == topicId))
            {
                selectedTopics.Add(new Topic { Id = topicId, Name = topic.Name, Description = topic.Description });
                component.SelectedState(true);
            }
            else
            {
                var deleted = selectedTopics.FirstOrDefault(x => x.Id == topicId);

                if(deleted != null) 
                    selectedTopics.Remove(deleted);

                component.SelectedState(false);
            }
        }

        private void ChosesTopics()
        {
            if(selectedTopics.Any())
            {
                foreach(var child in st_AllTopic.Children)
                {
                    if(child is MainTopicComponents component)
                    {
                        if (selectedTopics.Any(x => x.Id == component.GetId()))
                            component.SelectedState(true);
                    }
                }
            }
            else
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllTopic();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();
    }
}

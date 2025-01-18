using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Components.MainForComponents;
using AutoTest.Desktop.Windows.QuestionForWIndows;
using AutoTest.Domain.Entities.Tests;
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
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestDto Test { get; set; }
        public TestWindow()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();

        public void SetValues(TestDto test)
        {
            this.Test = test;
            tbTitle.Text = test.Title;
            tbDescription.Text = test.Description;
            tbLevel.Text = test.Level.ToString();
            tbStatus.Text = test.Status.ToString();
            tbTestCount.Text = test.Question.Count.ToString();
            SelectTopics(test.Topics);
        }

        private void SelectTopics(List<Topic> topics)
        {
            if(topics.Any())
            {
                TopicLoader.Visibility = Visibility.Collapsed;
                EmptyDataSelectTopic.Visibility = Visibility.Collapsed;

                foreach(var topic in topics)
                {
                    var dto = new TopicDto
                    {
                        Id = topic.Id,
                        Name = topic.Name,
                        Description = topic.Description
                    };

                    MainTopicComponents component = new MainTopicComponents();
                    component.Tag = topic;
                    component.SetValues(dto);
                    stTopics.Children.Add(component);
                }
            }
            else
            {
                TopicLoader.Visibility = Visibility.Collapsed;
                EmptyDataSelectTopic.Visibility = Visibility.Visible;
            }
        }

        private void ViewQuestionsBtn_Click(object sender, RoutedEventArgs e)
        {
            QuestionViewWindow window = new QuestionViewWindow();
            window.SelectTestId(Test.Id);
            window.ShowDialog();
        }
    }
}

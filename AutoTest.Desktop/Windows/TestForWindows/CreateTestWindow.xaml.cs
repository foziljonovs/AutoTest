using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Components.MainForComponents;
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
        private List<string> topics = new List<string>()
        {
            "C#",
            "Java",
            "Rust",
            "C++",
            "Go"
        };
        public CreateTestWindow()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            st_topics.Children.Clear();

            foreach(var item in topics)
            {
                TopicDto topic = new TopicDto
                {
                    Id = 1,
                    Name = item,
                    Description = "Birnimalarda e"
                };

                MainTopicComponents component = new MainTopicComponents();
                component.Tag = topic;
                component.SetValues(topic);
                st_topics.Children.Add(component);
            }
        }
    }
}

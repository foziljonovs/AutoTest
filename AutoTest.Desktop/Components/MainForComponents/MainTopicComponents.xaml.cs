using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Pages.MainForPage;
using AutoTest.Desktop.Windows.TestForWindows;
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

namespace AutoTest.Desktop.Components.MainForComponents
{
    /// <summary>
    /// Interaction logic for MainTopicComponents.xaml
    /// </summary>
    public partial class MainTopicComponents : UserControl
    {
        public MainTopicComponents()
        {
            InitializeComponent();
        }
        private long Id { get; set; }

        public void SetValues(TopicDto dto)
        {
            Id = dto.Id;
            tbTopic.Text = dto.Name;
        }

        private void st_border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);

            if(parentWindow is CreateTestWindow window)
            {
                window.AddTopic(Id);
            }
            else
            {
                var page = FindParentPage(this);

                if(page is MainPage mainPage)
                {
                    mainPage.AddTopic(Id);
                }
            }
        }

        private Page FindParentPage(DependencyObject child)
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject is Page page)
            {
                return page;
            }
            else if (parentObject != null)
            {
                return FindParentPage(parentObject);
            }

            return null!;
        }
    }
}

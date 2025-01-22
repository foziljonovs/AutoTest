using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Components.MainForComponents;
using AutoTest.Desktop.PDF;
using AutoTest.Desktop.Windows.QuestionForWIndows;
using AutoTest.Domain.Entities.Tests;
using QuestPDF.Fluent;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

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

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: System.Windows.Application.Current.MainWindow,
            corner: Corner.TopRight,
            offsetX: 50,
            offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 200;
            cfg.DisplayOptions.TopMost = true;
        });

        Notifier notifierThis = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.TopRight,
            offsetX: 50,
            offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 200;
            cfg.DisplayOptions.TopMost = true;
        });

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

        public string SanitizeFileName(string fileName)
        {
            foreach (char invalidChar in System.IO.Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidChar, '_');
            }
            return fileName;
        }

        private void UploadTestBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sanitizedTitle = SanitizeFileName(Test.Title);

                // Rabochiy stol papkasini aniqlash
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(desktopPath, $"Test_{sanitizedTitle}.pdf");

                // Faylni yaratish
                var document = new TestReportDocument(Test);
                document.GeneratePdf(filePath);
                notifierThis.ShowSuccess("Test muvaffaqiyatli yuklandi.");
            }
            catch (Exception ex)
            {
                notifierThis.ShowError("Testni yuklashda xatolik yuz berdi!");
            }
        }
    }
}

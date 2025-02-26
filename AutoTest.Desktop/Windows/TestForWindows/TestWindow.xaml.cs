using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Components.MainForComponents;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.PDF;
using AutoTest.Desktop.Windows.QuestionForWIndows;
using AutoTest.Domain.Entities.Tests;
using QuestPDF.Fluent;
using System.Windows;
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
        private readonly ITestService _service;
        public TestDto Test { get; set; }
        public TestWindow()
        {
            InitializeComponent();
            this._service = new TestService();
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
            TestLoader.Visibility = Visibility.Collapsed;
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
            stTopics.Children.Clear();

            if (topics.Any())
            {
                TopicLoader.Visibility = Visibility.Collapsed;
                EmptyDataSelectTopic.Visibility = Visibility.Collapsed;

                foreach (var topic in topics)
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

        private async void ViewQuestionsBtn_Click(object sender, RoutedEventArgs e)
        {
            QuestionViewWindow window = new QuestionViewWindow();
            window.SelectTestId(Test.Id);
            window.ShowDialog();
            await GetByIdAsync();
        }

        private async Task GetByIdAsync()
        {
            try
            {
                TestLoader.Visibility = Visibility.Visible;

                var test = await Task.Run(async () => await _service.GetByIdAsync(Test.Id));
                if (test != null)
                    SetValues(test);
                else
                {
                    notifier.ShowWarning("Istimos asosiy o'tib qayta kiring!");
                    TestLoader.Visibility = Visibility.Collapsed;
                    EmptyData.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                notifier.ShowWarning("Testni yuklashda xatolik yuz berdi!");
                TestLoader.Visibility = Visibility.Collapsed;
                EmptyData.Visibility = Visibility.Visible;
            }
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

        private void GenerateTestBtn_Click(object sender, RoutedEventArgs e)
        {
            notifierThis.ShowInformation("Test generatsiya qilish hali to'liq ishlab chiqarilmagan!");
        }
    }
}

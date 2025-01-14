using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Components.MainForComponents;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.Integrated.Services.Topic;
using AutoTest.Domain.Entities.Tests;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Windows.TestForWindows
{
    /// <summary>
    /// Interaction logic for UpdateTestWindow.xaml
    /// </summary>
    public partial class UpdateTestWindow : Window
    {
        private readonly ITopicService _topicService;
        private readonly ITestService _testService;
        private TestDto test { get; set; }
        private List<Topic> selectedTopics { get; set; } = new List<Topic>();
        public UpdateTestWindow()
        {
            InitializeComponent();
            this._topicService = new TopicService();
            this._testService = new TestService();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
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
            LevelComboBox.SelectedValue = test.Level.ToString();
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

                if (deleted != null)
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

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveBtn.Visibility = Visibility.Collapsed;
            BtnLoader.Visibility = Visibility.Visible;
            UpdateTestDto dto = new UpdateTestDto();

            if(!string.IsNullOrEmpty(txtTitle.Text) &&
                !string.IsNullOrEmpty(txtDesctiption.Text))
            {
                dto.Title = txtTitle.Text;
                dto.Description = txtDesctiption.Text;

                if (LevelComboBox.SelectedValue != null)
                {
                    string selectedLevel = (LevelComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                    if (selectedLevel == "Easy")
                        dto.Level = Domain.Enums.TestLevel.Easy;
                    else if (selectedLevel == "Medium")
                        dto.Level = Domain.Enums.TestLevel.Medium;
                    else
                        dto.Level = Domain.Enums.TestLevel.Hard;
                }
                else
                {
                    BtnLoader.Visibility = Visibility.Collapsed;
                    SaveBtn.Visibility = Visibility.Visible;
                    notifier.ShowWarning("Level tanlanmagan!");
                    return;
                }

                dto.Status = Domain.Enums.TestStatus.InProcess;
                dto.UserId = IdentitySingelton.GetInstance().Id;

                dto.Topics = selectedTopics.Select(x => x.Id).ToList();

                var result = await _testService.UpdateAsync(test.Id, dto);

                if(result)
                {
                    notifier.ShowSuccess($"Test muvaffaqiyatli yangilandi.");
                    this.Close();
                }
                else
                {
                    BtnLoader.Visibility = Visibility.Collapsed;
                    SaveBtn.Visibility = Visibility.Visible;
                    notifier.ShowError("Testni yangilashda xatolik yuzberdi!");
                }
            }
            else
            {
                BtnLoader.Visibility = Visibility.Collapsed;
                SaveBtn.Visibility = Visibility.Visible;
                notifier.ShowWarning("Malumotlar to'liq emas!");
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Clear();
            txtDesctiption.Clear();
            txtTopicFilter.Clear();
            LevelComboBox.SelectedItem = -1;

            foreach (var topic in selectedTopics.ToList())
            {
                var component = st_AllTopic.Children
                    .OfType<MainTopicComponents>()
                    .FirstOrDefault(x => x.GetId() == topic.Id);

                if (component != null)
                    component.SelectedState(false);

                selectedTopics.Clear();
            }
        }
    }
}

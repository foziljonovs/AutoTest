using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.DTOs.Tests.Topic;
using AutoTest.Desktop.Components.MainForComponents;
using AutoTest.Desktop.Integrated.Security;
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
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Windows.TestForWindows
{
    /// <summary>
    /// Interaction logic for CreateTestWindow.xaml
    /// </summary>
    public partial class CreateTestWindow : Window
    {
        private readonly ITestService _testService;
        private readonly ITopicService _topicService;
        private List<long> selectedTopics { get; set; } = new List<long>();
        public CreateTestWindow()
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

            cfg.Dispatcher = Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 200;
            cfg.DisplayOptions.TopMost = true;
        });

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

        public void AddTopic(MainTopicComponents component)
        {
            if (!selectedTopics.Contains(component.GetId()))
            {
                selectedTopics.Add(component.GetId());
                component.SelectedState(true);
            }
            else
            {
                selectedTopics.Remove(component.GetId());
                component.SelectedState(false);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllTopic();
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveBtn.Visibility = Visibility.Collapsed;
            BtnLoader.Visibility = Visibility.Visible;
            CreateTestDto dto = new CreateTestDto();

            if (!string.IsNullOrEmpty(txtTitle.Text) &&
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

                if(selectedTopics.Count > 0)
                {
                    dto.Topics = selectedTopics;
                }
                else
                {
                    BtnLoader.Visibility = Visibility.Collapsed;
                    SaveBtn.Visibility = Visibility.Visible;
                    notifier.ShowWarning("Topic tanlanmagan!");
                    return;
                }

                var result = await _testService.AddAsync(dto);

                if(result)
                {
                    notifier.ShowSuccess("Test muvaffaqiyatli yaratildi.");
                    this.Close();
                }
                else
                {
                    BtnLoader.Visibility = Visibility.Collapsed;
                    SaveBtn.Visibility = Visibility.Visible;
                    notifier.ShowError("Test yaratishda xatolik yuzberdi!");
                }
            }
            else
            {
                BtnLoader.Visibility = Visibility.Collapsed;
                SaveBtn.Visibility = Visibility.Visible;
                notifier.ShowWarning("Malumotlar to'liq emas!");
            }
        }
    }
}

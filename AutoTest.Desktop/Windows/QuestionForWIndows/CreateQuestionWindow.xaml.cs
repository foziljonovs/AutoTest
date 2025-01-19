using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.Desktop.Integrated.Services.Question;
using AutoTest.Desktop.Pages.OptionForPage;
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

namespace AutoTest.Desktop.Windows.QuestionForWIndows
{
    /// <summary>
    /// Interaction logic for CreateQuestionWindow.xaml
    /// </summary>
    public partial class CreateQuestionWindow : Window
    {
        private long TestId { get; set; }
        private readonly IQuestionService _service;
        public CreateQuestionWindow()
        {
            InitializeComponent();
            this._service = new QuestionService();
        }

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateOptionPage page = new CreateOptionPage();
            OptionPageNavigator.Content = page;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();

        public void SelectTestId(long  testId)
        {
            this.TestId = testId;
        }

        private async void CreateQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateQuestionDto dto = new CreateQuestionDto();

            if(!string.IsNullOrEmpty(txtProblem.Text))
            {
                dto.Problem = txtProblem.Text;
                dto.TestId = TestId;

                if (rbMultiple.IsChecked == true)
                {
                    dto.Type = Domain.Enums.QuestionType.Multiple;
                }
                else if (rbTrueOrFalse.IsChecked == true)
                {
                    dto.Type = Domain.Enums.QuestionType.TrueFalse;
                }
                else if (rbFillInTheBlank.IsChecked == true)
                {
                    dto.Type = Domain.Enums.QuestionType.FIllInTheBlank;
                }
                else
                {
                    notifierThis.ShowWarning("Savol turini tanlang!");
                    return;
                }

                var result = await _service.AddAsync(dto);

                if(result)
                {
                    notifier.ShowSuccess("Savol muvaffaqiyatli yaratildi.");
                    this.Close();
                }
                else
                {
                    notifierThis.ShowError("Savol yaratishda xatolik yuz berdi!");
                }
            }
            else
            {
                notifierThis.ShowWarning("Malumotlar to'liq emas!");
            }
        }
    }
}

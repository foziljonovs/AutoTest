using AutoTest.BLL.DTOs.Tests.Option;
using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.Desktop.Integrated.Services.Option;
using AutoTest.Desktop.Integrated.Services.Question;
using AutoTest.Desktop.Pages.OptionForPage;
using AutoTest.Domain.Enums;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Windows.QuestionForWIndows
{
    /// <summary>
    /// Interaction logic for UpdateQuestionWindow.xaml
    /// </summary>
    public partial class UpdateQuestionWindow : Window
    {
        private readonly IQuestionService _service;
        private readonly IOptionService _optionService;
        public UpdateQuestionWindow()
        {
            InitializeComponent();
            this._service = new QuestionService();
            this._optionService = new OptionService();
        }
        public QuestionDto Question { get; set; }

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
            var parentWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            if (parentWindow is null)
                parentWindow = Application.Current.MainWindow;

            cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: parentWindow,
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
        public void GetQuestion(QuestionDto dto)
        {
            if (dto is not null)
            {
                Question = dto;
                PageNavigator();
                Get(dto.Problem, dto.Type);
            }
            else
            {
                notifierThis.ShowWarning("Savol ma'lumotlarini olishda xatolik yuz berdi!");
                this.Hide();
            }
        }

        private void PageNavigator()
        {
            CreateOptionPage page = new CreateOptionPage();
            page.AddOptions(Question.Options);
            OptionPageNavigator.Content = page;
        }

        private void Get(string problem, QuestionType type)
        {
            if (!string.IsNullOrEmpty(problem))
            {
                txtProblem.Text = problem;

                var questionType = type switch
                {
                    QuestionType.Multiple => rbMultiple.IsChecked = true,
                    QuestionType.FIllInTheBlank => rbFillInTheBlank.IsChecked = true,
                    QuestionType.TrueFalse => rbTrueOrFalse.IsChecked = true,
                    _ => false
                };

                if (questionType == false)
                    notifierThis.ShowWarning("Savol turi tanlanmagan!");
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();

        private async void SaveQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateQuestionDto dto = new UpdateQuestionDto();
                if (!string.IsNullOrEmpty(txtProblem.Text))
                {
                    dto.Problem = txtProblem.Text;
                    dto.TestId = Question.TestId;

                    if (rbMultiple.IsChecked is true)
                        dto.Type = QuestionType.Multiple;
                    else if (rbTrueOrFalse.IsChecked is true)
                        dto.Type = QuestionType.TrueFalse;
                    else if (rbFillInTheBlank.IsChecked is true)
                        dto.Type = QuestionType.FIllInTheBlank;
                    else
                    {
                        notifierThis.ShowWarning("Savol turini tanlanmagan!");
                        return;
                    }

                    var currentPage = OptionPageNavigator.Content;
                    if (currentPage is CreateOptionPage page)
                    {
                        var options = page.GetOptions();
                        if (!options.Any())
                        {
                            notifierThis.ShowWarning("Savolga javoblar yozilmagan!");
                            return;
                        }

                        var result = await _service.UpdateAsync(Question.Id, dto);

                        if (result)
                        {
                            bool optionRes = false;
                            foreach (var option in options)
                            {
                                if (option.IsChange is false)
                                {
                                    var optionDto = new CreateOptionDto
                                    {
                                        Text = option.Text,
                                        IsCorrect = option.IsCorrect,
                                        QuestionId = Question.Id,
                                        IsChange = option.IsChange,
                                    };

                                    var optionResult = await _optionService.AddAsync(optionDto);
                                    if (optionResult < 0)
                                    {
                                        notifierThis.ShowWarning("Savolni saqlashda xatolik yuz berdi!");
                                        return;
                                    }
                                }
                                else
                                {
                                    var optionDto = new UpdateOptionDto
                                    {
                                        Text = option.Text,
                                        IsCorrect = option.IsCorrect,
                                        QuestionId = Question.Id,
                                        IsChange = option.IsChange
                                    };

                                    optionRes = await _optionService.UpdateAsync(option.Id, optionDto);
                                }
                            }

                            if (optionRes)
                            {
                                notifier.ShowSuccess("Savol muvaffaqiyatli o'zgartirildi!");
                                this.Close();
                            }
                            else
                                notifierThis.ShowError("Savolni saqlashda xatolik yuz berdi!");
                        }
                        else
                            notifierThis.ShowError("Savolni saqlashda xatolik yuz berdi!");
                    }
                    else
                        notifierThis.ShowWarning("Xatolik yuz berdi, qayta urining.");
                }
                else
                    notifierThis.ShowWarning("Savol kiritilmagan!");
            }
            catch (Exception ex)
            {
                notifierThis.ShowError("Xatolik yuz berdi!");
            }
        }
    }
}

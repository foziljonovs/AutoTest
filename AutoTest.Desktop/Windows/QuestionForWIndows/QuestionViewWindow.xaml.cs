using AutoTest.BLL.DTOs.Tests.Question;
using AutoTest.Desktop.Components.QuestionForComponents;
using AutoTest.Desktop.Integrated.Services.Question;
using System.Windows;

namespace AutoTest.Desktop.Windows.QuestionForWIndows
{
    /// <summary>
    /// Interaction logic for QuestionViewWindow.xaml
    /// </summary>
    public partial class QuestionViewWindow : Window
    {
        private readonly IQuestionService _questionService;
        public QuestionViewWindow()
        {
            InitializeComponent();
            this._questionService = new QuestionService();
        }

        public long TestId { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllQuestion();
        }

        public void SelectTestId(long id)
        {
            TestId = id;
        }

        private async Task GetAllQuestion()
        {
            QuestionLoader.Visibility = Visibility.Visible;

            var questions = await Task.Run(async () => await _questionService.GetQuestionsByTestAsync(TestId));
            ShowQuestions(questions);
        }

        private void ShowQuestions(List<QuestionDto> questions)
        {
            stQuestions.Children.Clear();
            int count = 1;

            if (questions.Any())
            {
                QuestionLoader.Visibility = Visibility.Collapsed;
                EmptyQuestionData.Visibility = Visibility.Collapsed;

                foreach (var question in questions)
                {
                    QuestionComponent component = new QuestionComponent();
                    component.Tag = question;
                    component.SetValues(question, count);
                    component.IsDeleted = GetAllQuestion;
                    stQuestions.Children.Add(component);
                    count++;
                }
            }
            else
            {
                QuestionLoader.Visibility = Visibility.Collapsed;
                EmptyQuestionData.Visibility = Visibility.Visible;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
            => this.Close();

        private void AddQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateQuestionWindow window = new CreateQuestionWindow();
            window.SelectTestId(TestId);
            window.ShowDialog();
            GetAllQuestion();
        }
    }
}

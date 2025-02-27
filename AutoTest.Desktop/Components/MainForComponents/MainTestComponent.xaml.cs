using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.Desktop.Windows.TestForWindows;
using System.Windows.Controls;

namespace AutoTest.Desktop.Components.MainForComponents
{
    /// <summary>
    /// Interaction logic for MainTestComponent.xaml
    /// </summary>
    public partial class MainTestComponent : UserControl
    {
        public MainTestComponent()
        {
            InitializeComponent();
        }

        public long Id { get; set; }

        public void SetValues(TestDto dto, int number)
        {
            Id = dto.Id;
            tbNumber.Text = number.ToString();
            tbTitle.Text = dto.Title;
            tbTopic.Text = dto.Topics.FirstOrDefault()?.Name ?? "?";
        }

        private void st_border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(Id > 0)
            {
                TestViewWindow window = new TestViewWindow();
                window.SetTestId(Id);
                window.ShowDialog();
            }
        }

        private void SolutionBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TestSolutionWindow window = new TestSolutionWindow();
            window.ShowDialog();
            window.SetTestId(Id);
        }
    }
}

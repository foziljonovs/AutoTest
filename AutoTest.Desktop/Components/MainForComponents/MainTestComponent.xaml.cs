using AutoTest.BLL.DTOs.Tests.Test;
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
    }
}

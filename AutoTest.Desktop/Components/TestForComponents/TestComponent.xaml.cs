using AutoTest.BLL.DTOs.Tests.Test;
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

namespace AutoTest.Desktop.Components.TestForComponents
{
    /// <summary>
    /// Interaction logic for TestComponent.xaml
    /// </summary>
    public partial class TestComponent : UserControl
    {
        public TestComponent()
        {
            InitializeComponent();
        }

        private long Id { get; set; }

        public void SetValues(TestDto test, long number)
        {
            test.Id = test.Id;
            tbNumber.Text = number.ToString();
            tbTitle.Text = test.Title;
            tbLevel.Text = test.Level.ToString();
            tbStatus.Text = test.Status.ToString();
            tbTopic.Text = test.Topics.FirstOrDefault()?.Name ?? "?";
        }
    }
}

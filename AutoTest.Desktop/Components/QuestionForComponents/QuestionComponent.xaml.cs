using AutoTest.BLL.DTOs.Tests.Question;
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

namespace AutoTest.Desktop.Components.QuestionForComponents
{
    /// <summary>
    /// Interaction logic for QuestionComponent.xaml
    /// </summary>
    public partial class QuestionComponent : UserControl
    {
        public QuestionComponent()
        {
            InitializeComponent();
        }
        private QuestionDto Question { get; set; }

        public void SetValues(QuestionDto question, int number)
        {
            this.Question = question;
            tbNumber.Text = number.ToString();
            tbProblem.Text = question.Problem;
            tbType.Text = question.Type.ToString();
            tbTestName.Text = question.Test.Title.ToString();
        }
    }
}

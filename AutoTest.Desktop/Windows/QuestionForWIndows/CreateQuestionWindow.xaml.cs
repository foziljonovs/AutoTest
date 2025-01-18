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

namespace AutoTest.Desktop.Windows.QuestionForWIndows
{
    /// <summary>
    /// Interaction logic for CreateQuestionWindow.xaml
    /// </summary>
    public partial class CreateQuestionWindow : Window
    {
        public CreateQuestionWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateOptionPage page = new CreateOptionPage();
            OptionPageNavigator.Content = page;
        }
    }
}

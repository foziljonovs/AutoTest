using AutoTest.Desktop.Components.QuestionForComponents;
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
    /// Interaction logic for QuestionViewWindow.xaml
    /// </summary>
    public partial class QuestionViewWindow : Window
    {
        public QuestionViewWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stQuestions.Children.Clear();

            for(int i = 0; i < 10; i++)
            {
                QuestionComponent component = new QuestionComponent();
                stQuestions.Children.Add(component);
            }
        }
    }
}

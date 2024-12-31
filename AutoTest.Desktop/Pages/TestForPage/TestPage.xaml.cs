using AutoTest.Desktop.Components.TestForComponents;
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

namespace AutoTest.Desktop.Pages.TestForPage
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        public TestPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ShowTests();
        }

        private void ShowTests()
        {
            st_tests.Children.Clear();

            for(int i = 0; i < 10; i++)
            {
                TestComponent component = new TestComponent();
                st_tests.Children.Add(component);
            }
        }
    }
}

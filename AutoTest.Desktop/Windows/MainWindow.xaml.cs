using AutoTest.Desktop.Pages.MainForPage;
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

namespace AutoTest.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage page = new MainPage();
            PageNavigator.Content = page;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainPage page = new MainPage();
            PageNavigator.Content = page;
        }
    }
}

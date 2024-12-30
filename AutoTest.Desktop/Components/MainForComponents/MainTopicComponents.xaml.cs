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

namespace AutoTest.Desktop.Components.MainForComponents
{
    /// <summary>
    /// Interaction logic for MainTopicComponents.xaml
    /// </summary>
    public partial class MainTopicComponents : UserControl
    {
        public MainTopicComponents()
        {
            InitializeComponent();
        }

        public void SetValues(string value)
        {
            tbTopic.Text = value;
        }
    }
}

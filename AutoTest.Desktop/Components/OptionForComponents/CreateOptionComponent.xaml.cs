using AutoTest.BLL.DTOs.Tests.Option;
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

namespace AutoTest.Desktop.Components.OptionForComponents
{
    /// <summary>
    /// Interaction logic for CreateOptionComponent.xaml
    /// </summary>
    public partial class CreateOptionComponent : UserControl
    {
        public CreateOptionComponent()
        {
            InitializeComponent();
        }

        public void SetValues(OptionDto dto)
        {
            tbText.Text = dto.Text;

            if(dto.IsCorrect)
            {
                CancelIcon.Visibility = Visibility.Collapsed;
                CheckIcon.Visibility = Visibility.Visible;
            }
        }
    }
}

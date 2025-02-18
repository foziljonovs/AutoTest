﻿using AutoTest.BLL.DTOs.Tests.Test;
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

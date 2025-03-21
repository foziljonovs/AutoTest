﻿using AutoTest.Desktop.Pages.TestForPage;
using System.Windows;

namespace AutoTest.Desktop.Windows.TestForWindows;

/// <summary>
/// Interaction logic for TestSolutionWindow.xaml
/// </summary>
public partial class TestSolutionWindow : Window
{
    private long TestId { get; set; }
    public TestSolutionWindow()
    {
        InitializeComponent();
    }

    private void CloseBtn_Click(object sender, RoutedEventArgs e)
        => this.Close();

    public void SetTestId(long testId)
        => this.TestId = testId;

    private void StartBtn_Click(object sender, RoutedEventArgs e)
    {
        StartBtn.Visibility = Visibility.Collapsed;
        StopBtn.Visibility = Visibility.Visible;
        CloseBtn.Visibility = Visibility.Collapsed;

        SolutionPage page = new SolutionPage();
        page.SetTestId(TestId);
        PageNavigator.Content = page;
    }

    public void CompletedTest(long testSolutionId)
    {
        CompletedTestSolution page = new CompletedTestSolution();
        page.SetTestSolutionId(testSolutionId);
        PageNavigator.Content = page;
        StopBtn.Visibility = Visibility.Collapsed;
        CloseBtn.Visibility = Visibility.Visible;
    }

    private void StopBtn_Click(object sender, RoutedEventArgs e)
    {
        StopBtn.Visibility = Visibility.Collapsed;
        StartBtn.Visibility = Visibility.Visible;
        CloseBtn.Visibility = Visibility.Visible;
    }
}

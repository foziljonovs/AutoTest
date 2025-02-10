using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using QuestPDF.Infrastructure;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AutoTest.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Set the QuestPDF license type
            QuestPDF.Settings.License = LicenseType.Community;
        }

    }

}

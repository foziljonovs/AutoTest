using AutoTest.Desktop.Integrated.Security;
using AutoTest.Domain.Entities.Users;
using System.Windows.Controls;

namespace AutoTest.Desktop.Pages.ProfileForPage
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ProfileLbl.Name = GetUser();
        }

        private string GetUser()
        {
            var fullName = IdentitySingelton.GetInstance().Name;
            if (fullName is null)
                return "Sizning profilingiz";
            else
                return fullName;
        }
    }
}

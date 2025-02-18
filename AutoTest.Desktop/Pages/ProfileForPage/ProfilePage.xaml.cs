using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Services.Test;
using System.Windows.Controls;

namespace AutoTest.Desktop.Pages.ProfileForPage
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private readonly ITestService _testService;
        private long userId { get; set; }
        public ProfilePage()
        {
            InitializeComponent();
            this._testService = new TestService();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            GetAll();
        }

        private async void GetAll()
        {
            userId = IdentitySingelton.GetInstance().Id;
            var fullName = GetUser();
            if (fullName is not null)
                UserFullNameLbl.Content = fullName;
            else
                UserFullNameLbl.Content = "...";

            var count = await GetTestsCount(userId);
            if (count > 0)
                YourTestCount.Content = count.ToString();
            else
            {
                YourTestCount.Content = "0";
                YourTestCountText.Content = "Testlar mavjud emas";
            }
        }

        private async Task<int> GetTestsCount(long userId)
        {
            var tests = await _testService.GetAllByUserIdAsync(userId);
            if (!tests.Any())
                return 0;

            return tests.Count();
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

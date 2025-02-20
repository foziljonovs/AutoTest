using AutoTest.BLL.DTOs.User;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.Integrated.Services.User;
using System.Windows.Controls;

namespace AutoTest.Desktop.Pages.ProfileForPage
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private readonly ITestService _testService;
        private readonly IUserService _userService;
        private long userId { get; set; }
        public UserDto User { get; set; }
        public ProfilePage()
        {
            InitializeComponent();
            this._testService = new TestService();
            this._userService = new UserService();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            GetAll();
        }

        private async void GetAll()
        {
            userId = IdentitySingelton.GetInstance().Id;
            var user = await GetUser();
            if (user is not null)
            {
                User = user;
                ProfileLbl.Content = User.Firstname + " " + User.Lastname;
                UserFullNameLbl.Content = User.Firstname + " " + User.Lastname;
                UserPhoneNumberLbl.Content = User.PhoneNumber;
            }
            else
            {
                ProfileLbl.Content = "...";
                UserFullNameLbl.Content = "...";
                UserPhoneNumberLbl.Content = "...";
            }

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

        private async Task<UserDto> GetUser()
        {
            var id = IdentitySingelton.GetInstance().Id;

            var user = await _userService.GetByIdAsync(id);
            if (user is not null)
                return user;
            else
                return null;
        }
    }
}

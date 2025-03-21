﻿using AutoTest.BLL.DTOs.Users.User;
using AutoTest.Desktop.Integrated.Security;
using AutoTest.Desktop.Integrated.Services.Test;
using AutoTest.Desktop.Integrated.Services.User;
using AutoTest.Desktop.Pages.TestForPage;
using AutoTest.Desktop.Windows;
using AutoTest.Desktop.Windows.ProfileForWindows;
using AutoTest.Desktop.Windows.TestForWindows;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Pages.ProfileForPage
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private readonly ITestService _testService;
        private readonly IUserService _userService;
        private readonly ISavedTestService _savedTestService;
        private readonly IUserTestSolutionService _userTestSolutionService;
        private long userId { get; set; }
        public UserDto User { get; set; }
        public ProfilePage()
        {
            InitializeComponent();
            this._testService = new TestService();
            this._userService = new UserService();
            this._savedTestService = new SavedTestService();
            this._userTestSolutionService = new UserTestSolutionService();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: System.Windows.Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 200;
            cfg.DisplayOptions.TopMost = true;
        });

        Notifier notifierThis = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 200;
            cfg.DisplayOptions.TopMost = true;
        });

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
                YourTestCountText.Text = "Testlar mavjud emas";
            }

            var savedCount = await GetSavedTestsCount(userId);
            if (savedCount > 0)
                YourSavedTestsCount.Content = savedCount.ToString();
            else
                YourSavedTestsCount.Content = "0";

            var userTestSolutionCount = await GetUserTestSolution();
            if (userTestSolutionCount > 0)
                YourTestSolutionCount.Content = userTestSolutionCount.ToString();
            else
                YourTestSolutionCount.Content = "0";
        }

        private async Task<int> GetTestsCount(long userId)
        {
            var tests = await _testService.GetAllByUserIdAsync(userId);
            if (!tests.Any())
                return 0;

            return tests.Count();
        }

        private async Task<int> GetSavedTestsCount(long userId)
        {
            var tests = await _savedTestService.GetAllByUserIdAsync(userId);
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

        private async Task<int> GetUserTestSolution()
        {
            var userTestSolution = await _userTestSolutionService.GetAllByUserIdAsync(userId);
            if(!userTestSolution.Any())
                return 0;

            return userTestSolution.Count();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChangePasswordWindow window = new ChangePasswordWindow();
            window.ShowDialog();
        }

        private void VerifyBtn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            VerifyPasswordWindow window = new VerifyPasswordWindow();
            window.ShowDialog();
        }

        private void SavedTests_Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SavedTestWindow window = new SavedTestWindow();
            window.SetUserId(userId);
            window.ShowDialog();
            GetAll();
        }

        private void Border_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TestSolutionViewWindow window = new TestSolutionViewWindow();
            window.ShowDialog();
        }
    }
}

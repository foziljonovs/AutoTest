using AutoTest.BLL.DTOs.Tests.Option;
using AutoTest.Desktop.Integrated.Services.Option;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace AutoTest.Desktop.Components.OptionForComponents
{
    /// <summary>
    /// Interaction logic for CreateOptionComponent.xaml
    /// </summary>
    public partial class CreateOptionComponent : UserControl
    {
        private readonly IOptionService _service;
        private long optionId { get; set; }
        public Func<Task> IsDeleted {  get; set; }
        public CreateOptionComponent()
        {
            InitializeComponent();
            this._service = new OptionService();
        }

        Notifier notifierThis = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
            corner: Corner.TopRight,
            offsetX: 50,
            offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;

            cfg.DisplayOptions.Width = 200;
            cfg.DisplayOptions.TopMost = true;
        });

        public void SetValues(OptionDto dto)
        {
            optionId = dto.Id;
            tbText.Text = dto.Text;

            if(dto.IsCorrect)
            {
                CancelIcon.Visibility = Visibility.Collapsed;
                CheckIcon.Visibility = Visibility.Visible;
            }
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (optionId > 0)
                {
                    var message = MessageBox.Show("Javob o'chirilsinmi?", "Tasdiqlash", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if(message is MessageBoxResult.Yes)
                    {
                        var result = await _service.DeleteAsync(optionId);

                        if(result)
                        {
                            IsDeleted?.Invoke();
                            notifierThis.ShowSuccess("Javob o'chirildi.");
                        }
                        else
                        {
                            notifierThis.ShowInformation("Javobni o'chirilmadi, qayta urining!");
                        }
                    }
                    else
                    {
                        notifierThis.ShowWarning("Javobni o'chirishda xatolik yuz berdi!");
                    }
                }
                else
                    notifierThis.ShowWarning("O'chirishga urinish muvaffaqiyatsiz bo'ldi!");
            }
            catch(Exception ex)
            {
                notifierThis.ShowError("Xatolik yuz berdi!");
            }
        }
    }
}

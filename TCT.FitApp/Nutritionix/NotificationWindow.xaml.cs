using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Windows;

namespace Nutritionix
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public NotificationWindow()
        {
            InitializeComponent();
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var connection = new HubConnectionBuilder()
             .WithUrl("https://tct-fitapp.azurewebsites.net/notificationHub/")
             .Build();

            await connection.StartAsync();
            var userId = Guid.Parse(txtName.Text);
            await connection.InvokeAsync("SendMessage", userId, txtMessage.Text);
            await connection.StopAsync();
        }


    }
}

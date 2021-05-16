using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace TCT.FitApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HttpClient Client
        {
            get
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://tct-fitapp.azurewebsites.net/");
                return client;
            }

        }

        public static HubConnection HubConnection
        {
            get
            {
                var client = new HubConnectionBuilder()
                .WithUrl("https://tct-fitapp.azurewebsites.net/notificationHub")
                .Build();

                return client;
            }
        }
    }
}

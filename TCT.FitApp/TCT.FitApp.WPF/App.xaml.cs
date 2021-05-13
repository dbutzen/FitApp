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
    }
}

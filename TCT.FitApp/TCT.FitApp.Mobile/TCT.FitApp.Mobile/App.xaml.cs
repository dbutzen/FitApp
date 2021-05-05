using System;
using System.Net.Http;
using TCT.FitApp.Mobile.Models;
using TCT.FitApp.Mobile.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCT.FitApp.Mobile
{
    public partial class App : Application
    {

        public static User LoggedInUser { get; set; }
        public App()
        {
            InitializeComponent();

            if (LoggedInUser != null)
                MainPage = new NavigationPage(new HomePage());
            else
                MainPage = new NavigationPage(new LoginPage());


        }

        public static HttpClient Client
        {
            get
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://313a2ef9e235.ngrok.io/");
                return client;
            }

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

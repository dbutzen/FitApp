using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using TCT.FitApp.Mobile.Models;
using TCT.FitApp.Mobile.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCT.FitApp.Mobile
{
    public enum ReturnPage { Login, Register, Home, Profile, Settings }
    public partial class App : Application
    {
        public static ReturnPage ReturnPage;
        public static Guid SessionKey;// = Guid.Parse("D98D63B4-84AD-42EF-8F68-1665A4D2F29C");
        public static User LoggedInUser;
        public App()
        {
            InitializeComponent();
            LoadUser();
            MainPage = new NavigationPage(new HomePage());
        }

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

        public static void LoadUser()
        {
            try
            {
                SessionKey = (Guid)Application.Current.Properties["fitappskey"];
                if (SessionKey != Guid.Empty)
                {
                    HttpResponseMessage response;
                    string result;

                    response = App.Client.PostAsync($"User/LoadBySessionKey/{SessionKey}", null).Result;
                    result = response.Content.ReadAsStringAsync().Result;
                    LoggedInUser = JsonConvert.DeserializeObject<User>(result);
                }
            }
            catch { }
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

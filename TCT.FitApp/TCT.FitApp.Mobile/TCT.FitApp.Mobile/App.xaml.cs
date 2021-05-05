using Newtonsoft.Json;
using System;
using System.Net.Http;
using TCT.FitApp.Mobile.Models;
using TCT.FitApp.Mobile.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCT.FitApp.Mobile
{
    public enum ReturnPage {Login, Register, Home}
    public partial class App : Application
    {
        public static ReturnPage ReturnPage;
        public static Guid SessionKey = Guid.Parse("0264DE04-88C2-4642-A37E-15B22494AE45");
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
                client.BaseAddress = new Uri("https://313a2ef9e235.ngrok.io/");
                return client;
            }

        }

        private void LoadUser()
        {
            HttpResponseMessage response;
            string result;


            response = App.Client.PostAsync($"User/LoadBySessionKey/{SessionKey}", null).Result;
            result = response.Content.ReadAsStringAsync().Result;
            LoggedInUser = JsonConvert.DeserializeObject<User>(result);
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

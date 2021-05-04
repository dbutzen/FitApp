using System;
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

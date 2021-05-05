using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCT.FitApp.Mobile.Components;
using TCT.FitApp.Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCT.FitApp.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void EnableLoadingScreen()
        {
            grdLogin.IsEnabled = false;
            aiLoading.IsRunning = true;
        }

        private void DisableLoadingScreen()
        {
            grdLogin.IsEnabled = true;
            aiLoading.IsRunning = false;
        }


        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            EnableLoadingScreen();
            try
            {
                var client = App.Client;
                var user = new User();
                user.Username = txtUserName.Text;
                user.Password = txtPassword.Text;

                var serializedObject = JsonConvert.SerializeObject(user);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = null;
                await Task.Run(()=> { response = client.PostAsync("User/Login", content).Result; });
                if (response.IsSuccessStatusCode)
                {
                    App.SessionKey = JsonConvert.DeserializeObject<Guid>(response.Content.ReadAsStringAsync().Result);
                    // Store the session key in the device's memory
                    App.LoadUser();
                    App.ReturnPage = ReturnPage.Home;
                    await Navigation.PopModalAsync();

                }
                else
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                 await DisplayAlert("Login Failed", ex.Message, "OK");
            }
            DisableLoadingScreen();
        }

        private  void btnRegister_Clicked(object sender, EventArgs e)
        {
            App.ReturnPage = ReturnPage.Register;
            Navigation.PopModalAsync();
        }


    }
}
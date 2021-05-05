using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TCT.FitApp.Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCT.FitApp.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
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

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            EnableLoadingScreen();
            try
            {
                ValidateEntry();
                var client = App.Client;
                var user = new User();
                user.Username = txtUserName.Text;
                user.Password = txtPassword.Text;
                user.Name = txtDisplayName.Text;

                if (user.Password == txtConfirmPassword.Text)
                {
                    user.CalorieGoal = 0;
                    user.ProteinGoal = 0;
                    user.DaysInARowSucceeded = 0;
                    user.HeightInches = 0;
                    user.WeightPounds = 0;
                    user.Sex = "";
                    var serializedObject = JsonConvert.SerializeObject(user);
                    var content = new StringContent(serializedObject);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = null;
                    await Task.Run(() => { response = client.PostAsync("User", content).Result; });
                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Success","Account has been created","OK");
                        App.ReturnPage = ReturnPage.Login;
                        await Navigation.PopModalAsync();

                    }
                    else
                    {
                        throw new Exception("Username is already taken");
                    }
                }
                else
                {
                    throw new Exception("Passwords don't match");
                }
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failed", ex.Message, "OK");
            }
            DisableLoadingScreen();
        }

        private void ValidateEntry()
        {
            try
            {
                if (string.IsNullOrEmpty(txtDisplayName.Text))
                    throw new Exception("Display Name cannot be blank");
                if (string.IsNullOrEmpty(txtUserName.Text))
                    throw new Exception("Username cannot be blank");
                if (string.IsNullOrEmpty(txtPassword.Text))
                    throw new Exception("Password cannot be blank");
                if (string.IsNullOrEmpty(txtConfirmPassword.Text))
                    throw new Exception("Confirm your password");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            App.ReturnPage = ReturnPage.Login;
            Navigation.PopModalAsync();
        }
    }
}
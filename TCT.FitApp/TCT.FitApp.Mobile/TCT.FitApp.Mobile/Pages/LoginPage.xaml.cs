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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            Title = "Account Login";
            InitializeComponent();
        }

        private HttpClient InitializeClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://daadd72e4c4f.ngrok.io/");
            return client;
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                var client = InitializeClient();
                var user = new User();
                user.Username = txtUserName.Text;
                user.Password = txtPassword.Text;

                var serializedObject = JsonConvert.SerializeObject(user);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = client.PostAsync("User/Login", content).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    App.LoggedInUser = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);
                    Navigation.PushModalAsync(new NavigationPage(new HomePage { Title = App.LoggedInUser.Name}));
                }
                else
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                grdErrorMessage.IsVisible = true;
                lblErrorMessage.Text = ex.Message;
            }
        }

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            grdErrorMessage.IsVisible = false;
        }

        private void btnRegister_Clicked(object sender, EventArgs e)
        {

        }
    }
}
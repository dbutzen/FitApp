using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCT.FitApp.Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Forms9Patch;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;

namespace TCT.FitApp.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        User user;

        public ProfilePage(User user)
        {
            InitializeComponent();
            this.user = user;
            lblProfile.Text = user.Username + "'s Profile";
            txtName.Text = user.Name;
            txtHeight.Text = user.HeightInches.ToString();
            txtWeight.Text = user.WeightPounds.ToString();
            txtSex.Text = user.Sex;
        }

        private async void btnHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void btnEdit_Clicked(object sender, EventArgs e)
        {
            SaveEditToggle();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            var client = App.Client;
            user.Name = txtName.Text;
            user.HeightInches = int.Parse(txtHeight.Text);
            user.WeightPounds = int.Parse(txtWeight.Text);
            user.Sex = txtSex.Text;

            var serializedObject = JsonConvert.SerializeObject(user);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = null;
            await Task.Run(() => { response = client.PutAsync("User/" + user.Id, content).Result; });

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Profile has been updated", "OK");
                SaveEditToggle();
            }

        }

        private void SaveEditToggle()
        {         

            if(txtName.IsReadOnly == true)
            {
                btnEdit.IsEnabled = false;
                btnEdit.IsVisible = false;
                btnSave.IsEnabled = true;
                btnSave.IsVisible = true;
                txtName.IsReadOnly = false;
                txtHeight.IsReadOnly = false;
                txtWeight.IsReadOnly = false;
                txtSex.IsReadOnly = false;
            }
            else
            {
                btnEdit.IsEnabled = true;
                btnEdit.IsVisible = true;
                btnSave.IsEnabled = false;
                btnSave.IsVisible = false;
                txtName.IsReadOnly = true;
                txtHeight.IsReadOnly = false;
                txtWeight.IsReadOnly = false;
                txtSex.IsReadOnly = false;
            }
        }

        private async void btnSettings_Clicked(object sender, EventArgs e)
        {
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var page = new SettingsPage(user) { Title = "Settings" };
            page.Disappearing += (sender2, e2) =>
            {
                waitHandle.Set();
            };
            await Navigation.PushAsync(page);
            await Task.Run(() => waitHandle.WaitOne());
            //LoadUserData();
            //Rebind();
        }

        //private void LoadUserData()
        //{
        //    var client = App.Client;
        //    HttpResponseMessage response;
        //    string result;
        //    response = client.GetAsync($"User/{user.Id}/").Result;
        //    result = response.Content.ReadAsStringAsync().Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        user = JsonConvert.DeserializeObject<User>(result);
        //    }
        //}

        //private void Rebind()
        //{
        //    txtName.Text = user.Name;
        //    txtHeight.Text = user.HeightInches.ToString();
        //    txtWeight.Text = user.WeightPounds.ToString();
        //    txtSex.Text = user.Sex;
        //}
    }
}
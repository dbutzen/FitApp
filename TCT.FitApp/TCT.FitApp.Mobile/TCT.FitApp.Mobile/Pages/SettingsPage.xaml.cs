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
    public partial class SettingsPage : ContentPage
    {
        User user;

        public SettingsPage(User user)
        {
            InitializeComponent();
            this.user = user;
            lblSettings.Text = user.Username + "'s Settings";
            txtProteinGoal.Text = user.ProteinGoal.ToString();
            txtCalorieGoal.Text = user.CalorieGoal.ToString();
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void btnEdit_Clicked(object sender, EventArgs e)
        {
            SaveEditToggle();
        }

        private void SaveEditToggle()
        {

            if (txtProteinGoal.IsReadOnly == true)
            {
                btnEdit.IsEnabled = false;
                btnEdit.IsVisible = false;
                btnSave.IsEnabled = true;
                btnSave.IsVisible = true;
                txtProteinGoal.IsReadOnly = false;
                txtCalorieGoal.IsReadOnly = false;
            }
            else
            {
                btnEdit.IsEnabled = true;
                btnEdit.IsVisible = true;
                btnSave.IsEnabled = false;
                btnSave.IsVisible = false;
                txtProteinGoal.IsReadOnly = true;
                txtCalorieGoal.IsReadOnly = false;
            }
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            var client = App.Client;
            user.ProteinGoal = int.Parse(txtProteinGoal.Text);
            user.CalorieGoal = int.Parse(txtCalorieGoal.Text);

            var serializedObject = JsonConvert.SerializeObject(user);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = null;
            await Task.Run(() => { response = client.PutAsync("User/" + user.Id, content).Result; });

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Goal Settings have been updated", "OK");
                SaveEditToggle();
            }

        }
    }
}
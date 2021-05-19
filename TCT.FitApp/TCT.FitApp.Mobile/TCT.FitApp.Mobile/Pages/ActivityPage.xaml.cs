using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class ActivityPage : ContentPage
    {
        User user;
        List<Activity> activities;
        Day day;
        Activity selectedActivity;
        DayActivity selectedDayActivity;
        List<DayActivity> dayActivities;

        public ActivityPage(User user, Day day)
        {
            InitializeComponent();
            this.user = user;
            this.day = day;
            activities = GetActivities();
            cboActivity.ItemsSource = activities;
            LoadUserData();
            Rebind();
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            var selectedActivity = (Activity)cboActivity.SelectedItem;
            if (selectedActivity != null)
            {
                var dayActivity = new DayActivity();

                dayActivity.DayId = day.Id;
                dayActivity.ActivityId = selectedActivity.Id;
                dayActivity.Duration = int.Parse(txtDuration.Text);
                dayActivity.DifficultyLevel = int.Parse(txtDifficulty.Text);

                var serializedObject = JsonConvert.SerializeObject(dayActivity);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = App.Client.PostAsync("DayActivity", content).Result;
                var result = int.Parse(response.Content.ReadAsStringAsync().Result);

                if (result > 0)
                {
                    DisplayAlert("Success", "Selected activity has been added.", "OK");
                    LoadUserData();
                    Rebind();
                }
            }
        }

        private List<Activity> GetActivities()
        {
            HttpResponseMessage response;
            string result;

            response = App.Client.GetAsync("Activity").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var activityList = (JArray)JsonConvert.DeserializeObject(result);
            activities = activityList.ToObject<List<Activity>>();

            return activities;
        }

        private List<DayActivity> GetDayActivities()
        {
            HttpResponseMessage response;
            string result;

            response = App.Client.GetAsync("DayActivity").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var activityList = (JArray)JsonConvert.DeserializeObject(result);
            dayActivities = activityList.ToObject<List<DayActivity>>();

            return dayActivities;
        }

        private void LoadUserData()
        {
            try
            {
                var client = App.Client;
                HttpResponseMessage response;
                string result;
                response = client.GetAsync($"Day/{user.Id}/{cboDate.Date.ToString("yyyy-MM-dd")}").Result;
                result = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    day = JsonConvert.DeserializeObject<Day>(result);
                }

            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void Rebind()
        {
            if (day == null) { day = new Day(); }

            dgvActivities.ItemsSource = null;
            dgvActivities.ItemsSource = day.Activities;
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            selectedActivity = (Activity)dgvActivities.SelectedItem;
            if (selectedActivity != null)
            {
                var isYes = await DisplayAlert("Confirmation", "Are you sure you want to delete?", "Yes", "No");
                if (isYes)
                {
                    var response = App.Client.DeleteAsync($"DayActivity/{selectedActivity.DayActivityId}").Result;
                    int.Parse(response.Content.ReadAsStringAsync().Result);
                    LoadUserData();
                    Rebind();
                }

            }
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            selectedActivity = (Activity)dgvActivities.SelectedItem;
            if (selectedActivity != null)
            {
                DayActivity dayActivity = new DayActivity();

                dayActivity.DayId = day.Id;
                dayActivity.ActivityId = ((Activity)cboActivity.SelectedItem).Id;
                dayActivity.DifficultyLevel = int.Parse(txtDifficulty.Text);
                dayActivity.Duration = int.Parse(txtDuration.Text);

                var serializedObject = JsonConvert.SerializeObject(dayActivity);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = App.Client.PutAsync($"DayActivity/{dayActivity.Id}", content).Result;
                var result = int.Parse(response.Content.ReadAsStringAsync().Result);
                if (result > 0)
                {
                    var id = dayActivity.Id;
                    DisplayAlert("Success", "Item has been updated", "OK");
                    LoadUserData();
                    Rebind();

                    dgvActivities.SelectedItem = null;
                }
            }
        }

        private void cboDate_DateSelected(object sender, DateChangedEventArgs e)
        {      
            var client = App.Client;
            HttpResponseMessage response;
            string result;
            response = client.GetAsync($"Day/{user.Id}/{cboDate.Date.ToString("yyyy-MM-dd")}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                day = JsonConvert.DeserializeObject<Day>(result);
            }dgvActivities.ItemsSource = null;
            LoadUserData();
            Rebind();
        }        

    }
}
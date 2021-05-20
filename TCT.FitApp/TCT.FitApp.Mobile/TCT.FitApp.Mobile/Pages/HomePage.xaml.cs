using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCT.FitApp.Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCT.FitApp.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        User user;
        Day day;
        HubConnection hubConnection;

        public HomePage()
        {
            InitializeComponent();
            Title = "Home Page";
            hubConnection = App.HubConnection;
            StartHubConnection();
            ConnectToChannel();
            Authenticate();
        }

        private async void StartHubConnection()
        {
            await hubConnection.StartAsync();
        }


        private async Task SendNotification(string message)
        {
            try
            {
                await hubConnection.InvokeAsync("SendMessage", user.Id, message);
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void ConnectToChannel()
        {

            hubConnection.On<Guid, string>("ReceiveMessage", (userId, message) => OnSend(userId, message));
        }

        private void OnSend(Guid userId, string message)
        {
            if (userId == user.Id)
                Notify(message);
        }

        private async void Authenticate()
        {
            if (App.LoggedInUser == null)
            {
                if (App.ReturnPage == ReturnPage.Login)
                {
                    var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                    var page = new LoginPage { Title = "Login" };
                    page.Disappearing += (sender2, e2) =>
                    {
                        waitHandle.Set();
                    };
                    await Navigation.PushModalAsync(new NavigationPage(page));
                    await Task.Run(() => waitHandle.WaitOne());
                }
                else if (App.ReturnPage == ReturnPage.Register)
                {
                    var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                    var page = new RegisterPage() { Title = "Register" };
                    page.Disappearing += (sender2, e2) =>
                    {
                        waitHandle.Set();
                    };
                    await Navigation.PushModalAsync(new NavigationPage(page));
                    await Task.Run(() => waitHandle.WaitOne());
                }
                Authenticate();
                return;
            }
            Load();
            //Title = App.LoggedInUser.Name;
            //lblWelcome.Text = $"Welcome {App.LoggedInUser.Name}";
        }

        private void Load()
        {
            user = App.LoggedInUser;

            LoadUserData();
            Rebind();
        }

        private async void Rebind()
        {
            txtDisplayName.Text = user.Name;
            txtDate.Text = $"Today, {DateTime.Today.ToString("MMM d")}"; 
            if (day == null) { day = new Day(); }
            lblCalorieGoal.Text = $"<\t{user.CalorieGoal} cal";

            lblCalorieConsumed.Text = $"+\t{day.CaloriesConsumed} cal";
            lblCalorieBurned.Text = $"-\t{day.CaloriesBurned} cal";

            double toBurn = 0;
            var cal = day.CaloriesConsumed - day.CaloriesBurned;
            if (cal > user.CalorieGoal)
                toBurn = cal - user.CalorieGoal;
            lblCalorieToBurn.Text = $"{toBurn}";
            if (toBurn > 0)
            {
                await SendNotification($"You have {toBurn} calories to burn today.");
            }

            lblProtein.Text = $"{day.ProteinConsumed} g/{user.ProteinGoal} g";
            var proteinRate = 0;
            if (user.ProteinGoal > 0)
            {
                var rate = (day.ProteinConsumed * 100) / user.ProteinGoal;
                proteinRate = rate > 100 ? 100 : rate;
            }

            lblProteinRate.Text = $"{proteinRate}%";
            pbProteinRate.Progress = proteinRate;

            dgvActivities.ItemsSource = null;
            dgvActivities.ItemsSource = day.Activities;

            dgvItems.ItemsSource = null;
            dgvItems.ItemsSource = day.Items;

            if (day.ProteinConsumed >= user.ProteinGoal && toBurn == 0)
            {
                await SendNotification($"Congratulations! You have reached your goal for today.");
            }
        }

        private void LoadUserData()
        {
            try
            {
                day = new Day();
                var client = App.Client;
                HttpResponseMessage response;
                string result;
                response = client.GetAsync($"Day/{user.Id}/{DateTime.Today.ToString("yyyy-MM-dd")}").Result;
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

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            App.LoggedInUser = null;
            Application.Current.Properties["fitappskey"] = null;
            Application.Current.SavePropertiesAsync();
            App.ReturnPage = ReturnPage.Login;
            Authenticate();
        }

        private async void btnViewProfile_Clicked(object sender, EventArgs e)
        {
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var page = new ProfilePage(user) { Title = "Profile" };
            page.Disappearing += (sender2, e2) =>
            {
                waitHandle.Set();
            };
            await Navigation.PushAsync(page);
            await Task.Run(() => waitHandle.WaitOne());
            Rebind();
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
            Rebind();
        }

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            CloseNotification();
        }

        private void Notify(string message)
        {
            grdNotification.IsVisible = true;
            txtMessage.Text = message;
        }

        private void CloseNotification()
        {
            grdNotification.IsVisible = false;
            txtMessage.Text = string.Empty;
        }

        private async void btnManageActivity_Clicked(object sender, EventArgs e)
        {
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var page = new ActivityPage(user, day) { Title = "Activities" };
            page.Disappearing += (sender2, e2) =>
            {
                waitHandle.Set();
            };
            await Navigation.PushAsync(page);
            await Task.Run(() => waitHandle.WaitOne());
            LoadUserData();
            Rebind();
        }

        private async void btnManageItems_Clicked(object sender, EventArgs e)
        {
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var page = new ItemPage(day, user) { Title = "Manage User Items" };
            page.Disappearing += (sender2, e2) =>
            {
                waitHandle.Set();
            };
            await Navigation.PushAsync(page);
            await Task.Run(() => waitHandle.WaitOne());
            LoadUserData();
            Rebind();
        }

        private async void btnAddItem_Clicked(object sender, EventArgs e)
        {
            var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            var page = new DayItemPage(day, user) { Title = "Add Day Item" };
            page.Disappearing += (sender2, e2) =>
            {
                waitHandle.Set();
            };
            await Navigation.PushAsync(page);
            await Task.Run(() => waitHandle.WaitOne());
            LoadUserData();
            Rebind();
        }

        private async void btnDeleteItem_Clicked(object sender, EventArgs e)
        {

            var item = (Item)dgvItems.SelectedItem;
            if (item != null)
            {
                var isYes = await DisplayAlert("Confirmation", "Are you sure you want to delete?", "Yes", "No");
                if (isYes)
                {
                    var response = App.Client.DeleteAsync($"DayItem/{item.DayItemId}").Result;
                    var result = int.Parse(response.Content.ReadAsStringAsync().Result);
                    if (result > 0)
                    {
                        LoadUserData();
                        Rebind();
                    }
                }
            }
        }
    }
}
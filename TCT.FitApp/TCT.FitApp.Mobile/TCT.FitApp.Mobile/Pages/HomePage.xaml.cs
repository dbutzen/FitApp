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
        public HomePage()
        {
            Title = "Home Page";
            InitializeComponent();
            Authenticate();
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

        private void Rebind()
        {
            txtDisplayName.Text = user.Name;
            txtDate.Text = $"Today, {DateTime.Today.ToString("MMM d")}";
            if (day == null) { day = new Day(); }
            lblCalorieGoal.Text = $"<\t{user.CalorieGoal} cal";
            //var calorieRate = (day.CaloriesBurned * 100) / user.CalorieGoal;
            lblCalorieConsumed.Text = $"+\t{day.CaloriesConsumed} cal";
            lblCalorieBurned.Text = $"-\t{day.CaloriesBurned} cal";

            double toBurn = 0;
            var cal = day.CaloriesConsumed - day.CaloriesBurned;
            if (cal > user.CalorieGoal)
                toBurn = cal - user.CalorieGoal;
            lblCalorieToBurn.Text = $"{toBurn}";
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
            var page = new ProfilePage(user) { Title = "Profile"};
            page.Disappearing += (sender2, e2) =>
            {
                waitHandle.Set();
            };
            await Navigation.PushAsync(page);
            await Task.Run(() => waitHandle.WaitOne());
            Rebind();
        }
    }
}
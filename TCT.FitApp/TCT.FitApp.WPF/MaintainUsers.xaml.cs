using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TCT.FitApp.BL.Models;
using TCT.Utilities.Reporting;
using TCT.Utilities.Reporting.Models;

namespace TCT.FitApp.WPF
{
    /// <summary>
    /// Interaction logic for MaintainUsers.xaml
    /// </summary>
    public partial class MaintainUsers : Window
    {
        User user;

        HubConnection hubConnection;
        List<UserAccessLevel> userAccessLevels;
        public MaintainUsers(User user)
        {
            InitializeComponent();
            this.user = user;
            hubConnection = App.HubConnection;
            StartHubConnection();
            Reload();
        }
        private async void StartHubConnection()
        {
            await hubConnection.StartAsync();
        }
        private async Task SendNotification(string message)
        {
            await hubConnection.InvokeAsync("SendMessage", user.Id, message);
        }


        private async void Reload()
        {
            lblUsername.Content = user.Username;
            lblFullName.Content = user.Name;
            cboAccessLevels.ItemsSource = null;
            userAccessLevels = LoadUserAccessLevels();
            cboAccessLevels.ItemsSource = userAccessLevels;
            cboAccessLevels.SelectedValuePath = "Id";
            cboAccessLevels.DisplayMemberPath = "Name";
            dpStartDate.SelectedDate = DateTime.Today;
            dpEndDate.SelectedDate = DateTime.Today;
        }



        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            UserAccessLevel userAccessLevel = userAccessLevels[cboAccessLevels.SelectedIndex];
            user.UserAccessLevelId = userAccessLevel.Id;
            //int results = await UserManager.Update(user);
            string serializedObject = JsonConvert.SerializeObject(user);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = App.Client.PutAsync("User/" + user.Id, content).Result;
            if (response != null)
            {
                await SendNotification($"System: Your user access level has been updated to {userAccessLevel.Name}.");
            }
            this.Close();
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            CreateReport();
        }

        private void CreateReport()
        {
            var userReport = new UserReport();
            userReport.Name = user.Name;
            userReport.CalorieGoal = user.CalorieGoal;
            userReport.ProteinGoal = user.ProteinGoal;
            userReport.UserDataList = new List<UserData>();

            HttpResponseMessage response;
            string result;
            dynamic items;

            response = App.Client.GetAsync($"Day/GenerateReport?userId={user.Id}&startDate={dpStartDate.SelectedDate}&endDate={dpEndDate.SelectedDate}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Day> days = items.ToObject<List<Day>>();


            userReport.UserDataList = new List<UserData>();
            foreach (var d in days)
            {
                var data = new UserData();
                data.Date = d.Date;
                data.Activities = d.ActivityCount;
                data.CaloriesConsumed = d.CaloriesConsumed;
                data.CaloriesBurned = d.CaloriesBurned;
                data.ProteinConsumed = d.ProteinConsumed;
                data.Succeeded = d.Succeeded ? "Yes" : "No";

                userReport.UserDataList.Add(data);
            }

            
            var sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.Filter = "PDF Document (*.pdf)| *.pdf|Microsoft Word Document (*.doc)| *.doc|Microsoft Excel Spreadsheet (*.xls)| *.xls";
            sfd.FileName = $"report_{user.Username}-{DateTime.Now.ToString("yyMMddhhmmss")}";
            var dialogResult = (bool)sfd.ShowDialog();
            if (dialogResult && !string.IsNullOrEmpty(sfd.FileName))
            {
                var extension = System.IO.Path.GetExtension(sfd.FileName).Replace(".",string.Empty);
                var stream = UserReportManager.Print(userReport, extension);
                System.IO.File.WriteAllBytes(sfd.FileName, stream);
            }

        }

        private List<UserAccessLevel> LoadUserAccessLevels()
        {
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = App.Client.GetAsync("UserAccessLevel").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<UserAccessLevel> userAccessLevels = items.ToObject<List<UserAccessLevel>>();

            return userAccessLevels;
        }
    }
}

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
using TCT.FitApp.BL;
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
        List<UserAccessLevel> userAccessLevels;
        public MaintainUsers(User user)
        {
            this.user = user;
            InitializeComponent();
            Reload();
        }

        private async void Reload()
        {
            cboAccessLevels.ItemsSource = null;
            userAccessLevels = (List<UserAccessLevel>)await UserAccessLevelManager.Load();
            cboAccessLevels.ItemsSource = userAccessLevels;
            dpStartDate.SelectedDate = DateTime.Today;
            dpEndDate.SelectedDate = DateTime.Today;
        }



        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {

                UserAccessLevel userAccessLevel = userAccessLevels[cboAccessLevels.SelectedIndex];
                user.UserAccessLevelId = userAccessLevel.Id;
                int results = await UserManager.Update(user);
            });
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
    }
}

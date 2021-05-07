using System;
using System.Collections.Generic;
using System.Linq;
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
        public MaintainUsers(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void CreateReport()
        {
            var userReport = new UserReport();
            userReport.Name = user.Name;
            userReport.CalorieGoal = user.CalorieGoal;
            userReport.ProteinGoal = user.ProteinGoal;
            userReport.UserDataList = new List<UserData>();

            // Call the API GenerateReport

            var stream = UserReportManager.Print(userReport, ReportType.Pdf);
            System.IO.File.WriteAllBytes($"report_{user.Username}-{DateTime.Now.ToString("yyMMddhhmmss")}.pdf", stream);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}

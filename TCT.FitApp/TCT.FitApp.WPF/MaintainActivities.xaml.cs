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

namespace TCT.FitApp.WPF
{
    /// <summary>
    /// Interaction logic for MaintainActivities.xaml
    /// </summary>
    public partial class MaintainActivities : Window
    {
        Activity activity = new Activity();
        bool isNew = false;

        //Add activity
        public MaintainActivities()
        {
            InitializeComponent();
            txtActivity.Text = string.Empty;
            txtEasyWorkout.Text = string.Empty;
            txtMediumWorkout.Text = string.Empty;
            txtHardWorkout.Text = string.Empty;
            isNew = true;
        }

        //Edit activity
        public MaintainActivities(Activity activity)
        {
            this.activity = activity;
            InitializeComponent();
            txtActivity.Text = activity.Name;
            txtEasyWorkout.Text = activity.EasyCaloriesPerHour.ToString();
            txtMediumWorkout.Text = activity.MediumCaloriesPerHour.ToString();
            txtHardWorkout.Text = activity.HardCaloriesPerHour.ToString();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            activity.Name = txtActivity.Text;
            activity.EasyCaloriesPerHour = Convert.ToInt32(txtEasyWorkout.Text);
            activity.MediumCaloriesPerHour = Convert.ToInt32(txtMediumWorkout.Text);
            activity.HardCaloriesPerHour = Convert.ToInt32(txtHardWorkout.Text);

            if(isNew == false)
            {
                await ActivityManager.Update(activity);
                MessageBox.Show("Activity has been updated");
                this.Close();
            }
            else
            {
                await ActivityManager.Insert(activity);
                MessageBox.Show("Activity has been added");
                this.Close();
            }
        }

    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;


namespace TCT.FitApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    

    public enum ButtonPressed
    {
        Add, Edit, Delete
    }

    public partial class MainWindow : Window
    {
        List<Item> items;
        List<ItemType> itemTypes;
        List<Activity> activities;
        List<User> users;
        List<UserAccessLevel> userAccessLevels;

        ButtonPressed buttonPressed;


        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            items = (List<Item>)await ItemManager.Load();
            itemTypes = (List<ItemType>)await ItemTypeManager.Load();
            activities = (List<Activity>)await ActivityManager.Load();
            users = LoadUsers(); // api
            userAccessLevels = (List<UserAccessLevel>)await UserAccessLevelManager.Load();
        }

        private void btnItems_Click(object sender, RoutedEventArgs e)
        {
            ItemRebind();
        }

        private void ItemRebind()
        {
            grdMain.ItemsSource = null;
            grdMain.ItemsSource = items;

            grdMain.Columns[0].Visibility = Visibility.Hidden;
            grdMain.Columns[1].Visibility = Visibility.Hidden;
            grdMain.Columns[2].Visibility = Visibility.Hidden;

            grdMain.Columns[3].Header = "Item Name";
            grdMain.Columns[4].Header = "Calories";
            grdMain.Columns[5].Header = "Protein";

            btnDelete.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Visible;
        }

        private void btnItemTypes_Click(object sender, RoutedEventArgs e)
        {
            ItemTypeRebind();
        }

        private void ItemTypeRebind()
        {
            grdMain.ItemsSource = null;
            grdMain.ItemsSource = itemTypes;

            grdMain.Columns[0].Visibility = Visibility.Hidden;

            btnDelete.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Visible;
        }

        private void btnActivities_Click(object sender, RoutedEventArgs e)
        {
            ActivityRebind();
            
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            UserRebind();
        }

        private void UserRebind()
        {
            grdMain.ItemsSource = null;
            grdMain.ItemsSource = users;

            grdMain.Columns[0].Visibility = Visibility.Hidden;
            grdMain.Columns[3].Visibility = Visibility.Hidden;
            grdMain.Columns[4].Visibility = Visibility.Hidden;
            grdMain.Columns[5].Visibility = Visibility.Hidden;
            grdMain.Columns[6].Visibility = Visibility.Hidden;
            grdMain.Columns[7].Visibility = Visibility.Hidden;
            grdMain.Columns[8].Visibility = Visibility.Hidden;
            grdMain.Columns[9].Visibility = Visibility.Hidden;
            grdMain.Columns[10].Visibility = Visibility.Hidden;
            grdMain.Columns[11].Visibility = Visibility.Hidden;
            grdMain.Columns[12].Visibility = Visibility.Hidden;

            grdMain.Columns[1].Header = "Name";
            grdMain.Columns[2].Header = "Username";
            grdMain.Columns[13].Header = "Access Level";

            btnAdd.Visibility = Visibility.Hidden;
            btnDelete.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (grdMain.SelectedIndex > -1)
            {
                if (grdMain.ItemsSource == activities)
                {
                    var activity = activities[grdMain.SelectedIndex];
                    new MaintainActivities(activity).ShowDialog();
                    ActivityRebind();
                }
                else if (grdMain.ItemsSource == users)
                {
                    var user = users[grdMain.SelectedIndex];
                    var window = new MaintainUsers(user);
                    window.Owner = this;
                    window.ShowDialog();
                    UserRebind();
                }
                else if (grdMain.ItemsSource == items)
                {
                    var item = items[grdMain.SelectedIndex];
                    var window = new MaintainItems(item);
                }
            }
        }

        private void ActivityRebind()
        {
            grdMain.ItemsSource = null;
            grdMain.ItemsSource = activities;

            grdMain.Columns[0].Visibility = Visibility.Hidden;

            grdMain.Columns[1].Header = "Activity Name";
            grdMain.Columns[2].Header = "Easy Cal/Hr";
            grdMain.Columns[3].Header = "Medium Cal/Hr";
            grdMain.Columns[4].Header = "Hard Cal/Hr";

            btnDelete.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Visible;
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (grdMain.ItemsSource == activities)
            {
                var activity = activities[grdMain.SelectedIndex];
                var result = MessageBox.Show("Are you sure you want to delete ", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    await ActivityManager.Delete(activity.Id);
                activities.Remove(activity);
                ActivityRebind();
            }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (grdMain.ItemsSource == activities)
            {
                new MaintainActivities().ShowDialog();
                activities = await ActivityManager.Load();
                ActivityRebind();
            }
                
        }

        private List<User> LoadUsers()
        {
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = App.Client.GetAsync("User").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<User> users = items.ToObject<List<User>>();

            return users;
        }
    }
}

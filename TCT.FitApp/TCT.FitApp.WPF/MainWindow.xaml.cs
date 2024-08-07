﻿using Newtonsoft.Json;
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
            items = LoadItems();
            itemTypes = LoadItemTypes();
            activities = LoadActivities();
            users = LoadUsers(); // api
            userAccessLevels = LoadUserAccessLevels();
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
            grdMain.Columns[6].Visibility = Visibility.Hidden;

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
                    activities = LoadActivities();
                    ActivityRebind();
                }
                if (grdMain.ItemsSource == users)
                {
                    var user = users[grdMain.SelectedIndex];
                    new MaintainUsers(user).ShowDialog();
                    users = LoadUsers();
                    UserRebind();
                }
                if (grdMain.ItemsSource == items)
                {
                    var item = items[grdMain.SelectedIndex];
                    new MaintainItems(item).ShowDialog();
                    items = LoadItems();
                    ItemRebind();
                }
                if (grdMain.ItemsSource == itemTypes)
                {
                    var itemType = itemTypes[grdMain.SelectedIndex];
                    new MaintainItemTypes(itemType).ShowDialog();
                    itemTypes = LoadItemTypes();
                    ItemTypeRebind();
                }
            }
        }

        private void ActivityRebind()
        {
            grdMain.ItemsSource = null;
            grdMain.ItemsSource = activities;

            grdMain.Columns[0].Visibility = Visibility.Hidden;
            grdMain.Columns[5].Visibility = Visibility.Hidden;
            grdMain.Columns[6].Visibility = Visibility.Hidden;
            grdMain.Columns[7].Visibility = Visibility.Hidden;

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
                {
                    HttpResponseMessage response = App.Client.DeleteAsync("Activity/" + activity.Id).Result;
                }
                activities.Remove(activity);
                ActivityRebind();
            }

            if (grdMain.ItemsSource == items)
            {
                var item = items[grdMain.SelectedIndex];
                var result = MessageBox.Show("Are you sure you want to delete ", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {                     
                    HttpResponseMessage response = App.Client.DeleteAsync("Item/" + item.Id).Result;
                }
                items.Remove(item);
                ItemRebind();
            }

            if (grdMain.ItemsSource == itemTypes)
            {
                var itemType = itemTypes[grdMain.SelectedIndex];
                var result = MessageBox.Show("Are you sure you want to delete ", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    HttpResponseMessage response = App.Client.DeleteAsync("ItemType/" + itemType.Id).Result;
                }
                itemTypes.Remove(itemType);
                ItemTypeRebind();
            }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (grdMain.ItemsSource == activities)
            {
                new MaintainActivities().ShowDialog();
                activities = LoadActivities();
                ActivityRebind();
            }
            if (grdMain.ItemsSource == items)
            {
                new MaintainItems().ShowDialog();
                items = LoadItems();
                ItemRebind();
            }
            if(grdMain.ItemsSource == itemTypes)
            {
                new MaintainItemTypes().ShowDialog();
                itemTypes = LoadItemTypes();
                ItemTypeRebind();
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

        private List<Item> LoadItems()
        {
            HttpResponseMessage response;
            string result;
            dynamic things;

            response = App.Client.GetAsync("Item").Result;
            result = response.Content.ReadAsStringAsync().Result;
            things = (JArray)JsonConvert.DeserializeObject(result);
            List<Item> items = things.ToObject<List<Item>>();

            return items;
        }

        private List<Activity> LoadActivities()
        {
            HttpResponseMessage response;
            string result;
            dynamic things;

            response = App.Client.GetAsync("Activity").Result;
            result = response.Content.ReadAsStringAsync().Result;
            things = (JArray)JsonConvert.DeserializeObject(result);
            List<Activity> activities = things.ToObject<List<Activity>>();

            return activities;
        }

        private List<ItemType> LoadItemTypes()
        {
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = App.Client.GetAsync("ItemType").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<ItemType> itemTypes = items.ToObject<List<ItemType>>();

            return itemTypes;
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

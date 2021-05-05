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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Item> items;
        List<ItemType> itemTypes;
        List<Activity> activities;
        List<User> users;
        List<UserAccessLevel> userAccessLevels;


        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            items = (List<Item>)await ItemManager.Load();
            itemTypes = (List<ItemType>)await ItemTypeManager.Load();
            activities = (List<Activity>)await ActivityManager.Load();
            users = (List<User>)await UserManager.Load();
            userAccessLevels = (List<UserAccessLevel>)await UserAccessLevelManager.Load();

        }

        private void btnItems_Click(object sender, RoutedEventArgs e)
        {
            grdMain.ItemsSource = null;
            grdMain.ItemsSource = items;

            grdMain.Columns[0].Visibility = Visibility.Hidden;
            grdMain.Columns[1].Visibility = Visibility.Hidden;
            grdMain.Columns[2].Visibility = Visibility.Hidden;

            grdMain.Columns[3].Header = "Item Name";
            grdMain.Columns[4].Header = "Calories";
            grdMain.Columns[5].Header = "Protein";
        }

        private void btnItemTypes_Click(object sender, RoutedEventArgs e)
        {
            grdMain.ItemsSource = null;
            grdMain.ItemsSource = itemTypes;

            grdMain.Columns[0].Visibility = Visibility.Hidden;
        }

        private void btnActivities_Click(object sender, RoutedEventArgs e)
        {
            grdMain.ItemsSource = null;
            grdMain.ItemsSource = activities;

            grdMain.Columns[0].Visibility = Visibility.Hidden;

            grdMain.Columns[1].Header = "Activity Name";
            grdMain.Columns[2].Header = "Easy Cal/Hr";
            grdMain.Columns[3].Header = "Medium Cal/Hr";
            grdMain.Columns[4].Header = "Hard Cal/Hr";
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
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

            grdMain.Columns[1].Header = "Name";
            grdMain.Columns[2].Header = "Username";
            grdMain.Columns[12].Header = "Access Level";
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

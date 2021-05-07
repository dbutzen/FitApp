using System;
using System.Windows;
using TCT.Utilities.Reporting;

namespace Nutritionix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private async void btnSubmitUPC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = await Manager.GetByUPC(txtUPC.Text);
                txtItemName.Text = item.item_name;
                txtItemProtein.Text = item.nf_protein.ToString();
                txtItemCalories.Text = item.nf_calories.ToString();
            }
            catch { }
        }

        private async void btnSubmitName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = await Manager.GetByName(txtKeyword.Text);
                dgItems.ItemsSource = null;
                dgItems.ItemsSource = items;
                //txtItemName.Text = item.item_name;
                //txtItemProtein.Text = item.nf_protein.ToString();
                //txtItemCalories.Text = item.nf_calories.ToString();
            }
            catch(Exception) { throw; }
        }
    }
}

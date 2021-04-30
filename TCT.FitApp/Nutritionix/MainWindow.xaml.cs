using Nutritionix;
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

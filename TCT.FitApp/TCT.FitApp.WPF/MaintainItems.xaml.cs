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
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.WPF
{
    /// <summary>
    /// Interaction logic for MaintainItems.xaml
    /// </summary>
    public partial class MaintainItems : Window
    {
        Item item = new Item();
        bool isNew = false;

        //Add activity
        public MaintainItems()
        {
            InitializeComponent();
            txtItem.Text = string.Empty;
            txtCalories.Text = string.Empty;
            txtProtein.Text = string.Empty;
            isNew = true;
        }

        //Edit activity
        public MaintainItems(Item item)
        {
            this.item = item;
            InitializeComponent();
            txtItem.Text = item.Name;
            txtCalories.Text = item.Calories.ToString();
            txtProtein.Text = item.Protein.ToString();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            item.Name = txtItem.Text;
            item.Calories = Convert.ToInt32(txtCalories.Text);
            item.Protein = Convert.ToInt32(txtProtein.Text);
            item.Servings = 1;

            if (isNew == false)
            {
                await ItemManager.Update(item);
                MessageBox.Show("Item has been updated");
                this.Close();
            }
            else
            {
                await ItemManager.Insert(item);
                MessageBox.Show("Item has been added");
                this.Close();
            }
        }
    }
}

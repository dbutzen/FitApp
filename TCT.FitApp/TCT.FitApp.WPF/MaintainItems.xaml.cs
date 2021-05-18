using Newtonsoft.Json;
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
            item.Servings = 0;

            if (isNew == false)
            {
                UpdateItem(item);
                MessageBox.Show("Item has been updated");
                this.Close();
            }
            else
            {
                InsertItem(item);
                MessageBox.Show("Item has been added");
                this.Close();
            }
        }
        private void UpdateItem(Item item)
        {
            string serializedObject = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = App.Client.PutAsync("Item/" + item.Id, content).Result;
        }

        private void InsertItem(Item item)
        {
            string serializedObject = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = App.Client.PostAsync("Item", content).Result;
        }
    }
}

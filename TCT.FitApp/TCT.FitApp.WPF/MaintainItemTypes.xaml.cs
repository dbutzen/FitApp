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
    /// Interaction logic for MaintainItemTypes.xaml
    /// </summary>
    public partial class MaintainItemTypes : Window
    {
        ItemType itemType = new ItemType();
        bool isNew = false;

        //Add activity
        public MaintainItemTypes()
        {
            InitializeComponent();
            txtItemType.Text = String.Empty;
            isNew = true;
        }

        //Edit activity
        public MaintainItemTypes(ItemType itemType)
        {
            this.itemType = itemType;
            InitializeComponent();
            txtItemType.Text = itemType.Name;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            itemType.Name = txtItemType.Text;

            if (isNew == false)
            {
                //await ItemTypeManager.Update(itemType);
                UpdateItemType(itemType);
                MessageBox.Show("Item Type has been updated");
                this.Close();
            }
            else
            {
                //await ItemTypeManager.Insert(itemType);
                CreateItemType(itemType);
                MessageBox.Show("Item Type has been added");
                this.Close();
            }
        }

        private void UpdateItemType(ItemType itemType)
        {
            string serializedObject = JsonConvert.SerializeObject(itemType);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = App.Client.PutAsync("ItemType/" + itemType.Id, content).Result;

        }

        private void CreateItemType(ItemType itemType)
        {
            string serializedObject = JsonConvert.SerializeObject(itemType);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = App.Client.PostAsync("ItemType", content).Result;
        }
    }
}

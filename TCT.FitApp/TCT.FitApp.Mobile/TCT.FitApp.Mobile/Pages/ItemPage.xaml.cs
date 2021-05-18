using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TCT.FitApp.Mobile.Models;
using TCT.FitApp.Mobile.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCT.FitApp.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemPage : ContentPage
    {
        Item item;
        User user;
        Day day;

        public ItemPage(Day day, User user)
        {
            InitializeComponent();
            this.user = user;
            this.day = day;
            LoadItems();
            Rebind();
        }

        private void Rebind()
        {
            if (item != null)
            {
                txtName.Text = item.Name;
                var type = ((List<ItemType>)(pckTypes.ItemsSource)).FirstOrDefault(t => t.Id == item.TypeId);
                pckTypes.SelectedItem = type;
                txtCalories.Text = item.Calories.ToString();
                txtProtein.Text = item.Protein.ToString();
            }
        }

        private void LoadItems()
        {
            pckItems.ItemsSource = null;
            pckItems.ItemsSource = GetItems();
            pckItems.Title = "Select an item";

            pckTypes.ItemsSource = null;
            pckTypes.ItemsSource = GetItemTypes();
            pckTypes.Title = "Select item type";
        }


        private List<Item> GetItems()
        {
            HttpResponseMessage response;
            string result;

            response = App.Client.GetAsync("Item").Result;
            result = response.Content.ReadAsStringAsync().Result;
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(result);

            return items;
        }

        private List<ItemType> GetItemTypes()
        {
            HttpResponseMessage response;
            string result;

            response = App.Client.GetAsync("ItemType").Result;
            result = response.Content.ReadAsStringAsync().Result;
            List<ItemType> itemTypes = JsonConvert.DeserializeObject<List<ItemType>>(result);

            return itemTypes;
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            item = new Item();
            item.Name = txtName.Text;
            item.TypeId = ((ItemType)pckTypes.SelectedItem).Id;
            item.Protein = int.Parse(txtProtein.Text);
            item.Calories = int.Parse(txtCalories.Text);
            //dayItem.Servings = int.Parse(txtServings.Text);

            var serializedObject = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = App.Client.PostAsync("Item", content).Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);
            if (result > 0)
            {
                var name = item.Name;
                DisplayAlert("Success", "Item has been added", "OK");
                LoadItems();
                Rebind();
                var new_item = ((List<Item>)(pckItems.ItemsSource)).FirstOrDefault(i => i.Name == name);
                pckItems.SelectedItem = new_item;
            }
        }

        private void btnUpdate_Clicked(object sender, EventArgs e)
        {
            item = (Item)pckItems.SelectedItem;
            if (item != null)
            {
                item.Name = txtName.Text;
                item.TypeId = ((ItemType)pckTypes.SelectedItem).Id;
                item.Protein = int.Parse(txtProtein.Text);
                item.Calories = int.Parse(txtCalories.Text);
                var serializedObject = JsonConvert.SerializeObject(item);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = App.Client.PutAsync($"Item/{item.Id}", content).Result;
                var result = int.Parse(response.Content.ReadAsStringAsync().Result);
                if (result > 0)
                {
                    var id = item.Id;
                    DisplayAlert("Success", "Item has been updated", "OK");
                    LoadItems();
                    Rebind();
                    var updated_item = ((List<Item>)(pckItems.ItemsSource)).FirstOrDefault(i => i.Id == id);
                    pckItems.SelectedItem = updated_item;
                }

            }
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            item = (Item)pckItems.SelectedItem;
            if (item != null)
            {
                var isYes = await DisplayAlert("Confirmation", "Are you sure you want to delete?", "Yes", "No");
                if (isYes)
                {
                    var response = App.Client.DeleteAsync($"Item/{item.Id}").Result;
                    int.Parse(response.Content.ReadAsStringAsync().Result);
                    LoadItems();
                    Rebind();
                    Clear();
                }

            }

        }

        private void pckItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            item = (Item)pckItems.SelectedItem;
            Rebind();
            txtUPC.Text = string.Empty;
        }

        private async void btnLookUp_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUPC.Text))
            {
                try
                {
                    pckItems.SelectedIndex = -1;
                    pckTypes.SelectedIndex = 0;
                    item = new Item();
                    var product = await Manager.GetByUPC(txtUPC.Text);
                    item.Name = product.item_name;
                    item.Protein = (int)product.nf_protein;
                    item.Calories = (int)product.nf_calories;
                    Rebind();
                }
                catch
                {
                    await DisplayAlert("Error", "Product not found", "OK");
                    Clear();
                    txtUPC.Focus();
                }
            }
            else
            {
                await DisplayAlert("Error", "UPC must not be empty", "OK");
                Clear();
                txtUPC.Focus();
            }

        }

        private void Clear()
        {
            txtUPC.Text = string.Empty;
            txtName.Text = string.Empty;
            txtProtein.Text = string.Empty;
            txtCalories.Text = string.Empty;

        }

        private void txtUPC_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtName.Text = string.Empty;
            txtProtein.Text = string.Empty;
            txtCalories.Text = string.Empty;
        }
    }
}
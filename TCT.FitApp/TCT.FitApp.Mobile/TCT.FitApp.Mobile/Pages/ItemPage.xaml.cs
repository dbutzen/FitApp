using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TCT.FitApp.Mobile.Models;
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

        public ItemPage(Item item, Day day, User user)
        {
            InitializeComponent();
            this.user = user;
            this.day = day;
            this.item = item;
            LoadItems();
            Rebind();
        }

        private void Rebind()
        {
            if (item != null)
            {
                // Delete Mode
                pckItems.SelectedItem = ((List<Item>)(pckItems.ItemsSource)).FirstOrDefault(i => i.Name == this.item.Name);
                txtServings.Text = item.Servings.ToString();
                btnDelete.IsVisible = true;
                pckItems.IsEnabled = false;
            }
            else
            {
                // Add Item Mode
                txtServings.IsEnabled = true;
                pckItems.IsEnabled = true;
                btnAdd.IsVisible = true;
                btnDelete.IsVisible = false;
            }

        }

        private void LoadItems()
        {
            pckItems.ItemsSource = null;
            pckItems.ItemsSource = GetItems();
            pckItems.Title = "Select an item";
            //if (pckItems.Items.Count > 0)
            //    pckItems.SelectedItem = dayItem.Id;
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

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            var selectedItem = (Item)pckItems.SelectedItem;
            if (selectedItem != null)
            {
                var dayItem = new DayItem();

                dayItem.DayId = day.Id;
                dayItem.ItemId = selectedItem.Id;
                dayItem.Servings = int.Parse(txtServings.Text);

                var serializedObject = JsonConvert.SerializeObject(dayItem);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = App.Client.PostAsync("DayItem", content).Result;
                var result = int.Parse(response.Content.ReadAsStringAsync().Result);
                if (result > 0)
                {
                    DisplayAlert("Success", "Selected item has been added.", "OK");
                }
            }
        }

        private void btnUpdate_Clicked(object sender, EventArgs e)
        {
            var item = (Item)pckItems.SelectedItem;
            if (item != null)
            {
                // update
            }
        }

        private void btnDelete_Clicked(object sender, EventArgs e)
        {
            if (item != null)
            {
                var response = App.Client.DeleteAsync($"DayItem/{day.Id}/{item.Id}").Result;
                var result = int.Parse(response.Content.ReadAsStringAsync().Result);
                if (result > 0)
                {
                    DisplayAlert("Success", "Item has been removed.", "OK");
                    Navigation.PopAsync();
                }
            }
        }
    }
}
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
    public partial class DayItemPage : ContentPage
    {
        User user;
        Day day;

        public DayItemPage(Day day, User user)
        {
            InitializeComponent();
            this.user = user;
            this.day = day;
            LoadItems();
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
    }
}
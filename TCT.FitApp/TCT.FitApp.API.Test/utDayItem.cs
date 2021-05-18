using TCT.FitApp.BL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System;

namespace TCT.FitApp.API.Test
{
    [TestClass]
    public class utDayItem
    {
        public HttpClient client { get; }

        public utDayItem()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<TCT.FitApp.API.Startup>();

            var testServer = new TestServer(webHostBuilder);

            client = testServer.CreateClient();
        }


        private List<DayItem> GetDayItems()
        {
            HttpResponseMessage response;
            string result;
            dynamic dayItems;

            response = client.GetAsync("DayItem").Result;
            result = response.Content.ReadAsStringAsync().Result;
            dayItems = (JArray)JsonConvert.DeserializeObject(result);
            List<DayItem> dayItemList = dayItems.ToObject<List<DayItem>>();

            return dayItemList;
        }

        private List<Item> GetItems()
        {
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = client.GetAsync("Item").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Item> itemList = items.ToObject<List<Item>>();

            return itemList;
        }

        private List<Day> GetDays()
        {
            HttpResponseMessage response;
            string result;
            dynamic days;

            response = client.GetAsync("Day").Result;
            result = response.Content.ReadAsStringAsync().Result;
            days = (JArray)JsonConvert.DeserializeObject(result);
            List<Day> dayList = days.ToObject<List<Day>>();

            return dayList;
        }

        [TestMethod]
        public void LoadTest()
        {
            List<DayItem> dayItems = GetDayItems();

            Assert.AreEqual(13, dayItems.Count);

        }             

        [TestMethod]
        public void InsertTest()
        {
            var dayId = GetDays().FirstOrDefault(d => d.Date == DateTime.Parse("03-12-21")).Id;
            var itemId = GetItems().FirstOrDefault(i => i.Name == "Cauliflower").Id;

            var dayItem = new DayItem();

            // hard coded ID placeholders until rest is done
            dayItem.DayId = dayId;
            dayItem.ItemId = itemId;
            dayItem.Servings = 3;

            var serializedObject = JsonConvert.SerializeObject(dayItem);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PostAsync("DayItem?rollback=true", content).Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }


        [TestMethod]
        public void DeleteTest()
        {
            var dayItem = GetDayItems().FirstOrDefault();

            var response = client.DeleteAsync($"DayItem/{dayItem.DayId}/{dayItem.ItemId}?rollback=true").Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }


    }
}

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

        [TestMethod]
        public void LoadTest()
        {
            List<DayItem> dayItems = GetDayItems();

            Assert.AreEqual(7, dayItems.Count);

        }

       

        [TestMethod]
        public void InsertTest()
        {
            var dayItem = new DayItem();

            // hard coded ID placeholders until rest is done
            dayItem.DayId = Guid.Parse("BCAE77FD-2E15-4354-9D71-375609C6370A");
            dayItem.ItemId = Guid.Parse("0F7D719C-27AA-45ED-922A-D09D1383673B");
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
            var id = GetDayItems().FirstOrDefault().Id;

            var response = client.DeleteAsync($"DayItem/{id}?rollback=true").Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }


    }
}

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
    public class utItem
    {
        public HttpClient client { get; }

        public utItem()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<TCT.FitApp.API.Startup>();

            var testServer = new TestServer(webHostBuilder);

            client = testServer.CreateClient();
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

        [TestMethod]
        public void LoadTest()
        {
            List<Item> items = GetItems();

            Assert.AreEqual(4, items.Count);

        }

        [TestMethod]
        public void LoadByIdTest()
        {
            HttpResponseMessage response;
            string result;

            var id = GetItems().FirstOrDefault(i => i.Name == "Cauliflower").Id;

            response = client.GetAsync($"Item/{id}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var item = JsonConvert.DeserializeObject<Item>(result);


            Assert.AreEqual("Cauliflower", item.Name);

        }

        [TestMethod]
        public void LoadByNameTest()
        {
            HttpResponseMessage response;
            string result;

            var name = "Cauliflower";
            response = client.GetAsync($"Item/{name}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var item = JsonConvert.DeserializeObject<Item>(result);


            Assert.AreEqual(name, item.Name);

        }


        // ItemTypeController is required
        //[TestMethod]
        public void LoadByTypeId()
        {
            HttpResponseMessage response;
            string result;

            var typeId = Guid.Empty;  // ItemTypeController is required
            response = client.GetAsync($"Item/Type/{typeId}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var item = JsonConvert.DeserializeObject<Item>(result);


            Assert.AreEqual(typeId, item.Id);

        }

        // ItemTypeController is required
        //[TestMethod]
        public void InsertTest()
        {
            var item = new Item();
            item.Name = "New Item";
            //item.TypeId = 
            item.Calories = 10;
            item.Protein = 7;

            var serializedObject = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PostAsync("Item?rollback=true", content).Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            var item = GetItems().FirstOrDefault(i => i.Name == "Cauliflower");
            item.Name = "Updated Name";

            var serializedObject = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PutAsync($"Item/{item.Id}?rollback=true", content).Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {
            var id = GetItems().FirstOrDefault(i => i.Name == "Cauliflower").Id;

            var response = client.DeleteAsync($"Item/{id}?rollback=true").Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }


    }
}

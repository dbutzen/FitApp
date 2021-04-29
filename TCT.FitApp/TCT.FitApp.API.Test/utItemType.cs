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
    public class utItemType
    {
        public HttpClient client { get; }

        public utItemType()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<TCT.FitApp.API.Startup>();

            var testServer = new TestServer(webHostBuilder);

            client = testServer.CreateClient();
        }


        private List<ItemType> GetItemTypes()
        {
            HttpResponseMessage response;
            string result;
            dynamic itemTypes;

            response = client.GetAsync("ItemType").Result;
            result = response.Content.ReadAsStringAsync().Result;
            itemTypes = (JArray)JsonConvert.DeserializeObject(result);
            List<ItemType> itemTypeList = itemTypes.ToObject<List<ItemType>>();

            return itemTypeList;
        }

        [TestMethod]
        public void LoadTest()
        {
            List<ItemType> itemTypes = GetItemTypes();

            Assert.AreEqual(2, itemTypes.Count);

        }

        [TestMethod]
        public void LoadByIdTest()
        {
            HttpResponseMessage response;
            string result;

            var id = GetItemTypes().FirstOrDefault(i => i.Name == "Food").Id;

            response = client.GetAsync($"ItemType/{id}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var itemType = JsonConvert.DeserializeObject<ItemType>(result);


            Assert.AreEqual("Food", itemType.Name);

        }



        [TestMethod]
        public void InsertTest()
        {
            var itemType = new ItemType();
            itemType.Name = "Plasma";

            var serializedObject = JsonConvert.SerializeObject(itemType);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PostAsync("ItemType?rollback=true", content).Result;
            var result = bool.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            var itemType = GetItemTypes().FirstOrDefault(i => i.Name == "Food");
            itemType.Name = "Better Food";

            var serializedObject = JsonConvert.SerializeObject(itemType);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PutAsync($"ItemType/{itemType.Id}?rollback=true", content).Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {
            var id = GetItemTypes().FirstOrDefault(i => i.Name == "Food").Id;

            var response = client.DeleteAsync($"ItemType/{id}?rollback=true").Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }


    }
}

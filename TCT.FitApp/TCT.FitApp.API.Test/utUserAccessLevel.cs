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
    public class utUserAccessLevel
    {
        public HttpClient client { get; }

        public utUserAccessLevel()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<TCT.FitApp.API.Startup>();

            var testServer = new TestServer(webHostBuilder);

            client = testServer.CreateClient();
        }


        private List<UserAccessLevel> GetUserAccessLevels()
        {
            HttpResponseMessage response;
            string result;
            dynamic userAccessLevels;

            response = client.GetAsync("UserAccessLevel").Result;
            result = response.Content.ReadAsStringAsync().Result;
            userAccessLevels = (JArray)JsonConvert.DeserializeObject(result);
            List<UserAccessLevel> userAccessLevelList = userAccessLevels.ToObject<List<UserAccessLevel>>();

            return userAccessLevelList;
        }

        [TestMethod]
        public void LoadTest()
        {
            List<UserAccessLevel> userAccessLevels = GetUserAccessLevels();

            Assert.AreEqual(3, userAccessLevels.Count);

        }

        [TestMethod]
        public void LoadByIdTest()
        {
            HttpResponseMessage response;
            string result;

            var id = GetUserAccessLevels().FirstOrDefault(i => i.Name == "Admin").Id;

            response = client.GetAsync($"UserAccessLevel/{id}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var userAccessLevel = JsonConvert.DeserializeObject<UserAccessLevel>(result);


            Assert.AreEqual("Admin", userAccessLevel.Name);

        }

        [TestMethod]
        public void LoadByNameTest()
        {
            HttpResponseMessage response;
            string result;

            var name = "Admin";
            response = client.GetAsync($"UserAccessLevel/{name}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var item = JsonConvert.DeserializeObject<Item>(result);


            Assert.AreEqual(name, item.Name);

        }

        public void InsertTest()
        {
            var userAccessLevel = new UserAccessLevel();
            userAccessLevel.Name = "Slug";
            userAccessLevel.Description = "Also Slug";

            var serializedObject = JsonConvert.SerializeObject(userAccessLevel);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PostAsync("UserAccessLevel?rollback=true", content).Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            var userAccessLevel = GetUserAccessLevels().FirstOrDefault(i => i.Name == "Admin");
            userAccessLevel.Name = "Overlord";

            var serializedObject = JsonConvert.SerializeObject(userAccessLevel);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PutAsync($"UserAccessLevel/{userAccessLevel.Id}?rollback=true", content).Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {
            var id = GetUserAccessLevels().FirstOrDefault(i => i.Name == "Admin").Id;

            var response = client.DeleteAsync($"UserAccessLevel/{id}?rollback=true").Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }


    }
}

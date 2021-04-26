using TCT.FitApp.BL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace TCT.FitApp.API.Test
{
    [TestClass]
    public class utUser
    {
        public HttpClient client { get; }

        public utUser()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<TCT.FitApp.API.Startup>();

            var testServer = new TestServer(webHostBuilder);

            client = testServer.CreateClient();
        }


        private List<User> GetUsers()
        {
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = client.GetAsync("User").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<User> users = items.ToObject<List<User>>();

            return users;
        }

        [TestMethod]
        public void LoadTest()
        {
            List<User> users = GetUsers();

            Assert.AreEqual(3, users.Count);

        }

        [TestMethod]
        public void LoadByIdTest()
        {
            HttpResponseMessage response;
            string result;

            var id = GetUsers().FirstOrDefault(u => u.Username == "jryan").Id;

            response = client.GetAsync($"User/{id}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<User>(result);


            Assert.AreEqual("jryan", user.Username);

        }

        [TestMethod]
        public void LoadByUsernameTest()
        {
            HttpResponseMessage response;
            string result;

            var username = "jryan";
            response = client.GetAsync($"User/{username}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<User>(result);


            Assert.AreEqual(username, user.Username);

        }

        [TestMethod]
        public void InsertTest()
        {
            var user = new User();
            user.Name = "New User";
            user.Username = "newuser123";
            user.Password = "pass";
            user.CalorieGoal = 1500;
            user.ProteinGoal = 99;
            user.DaysInARowSucceeded = 10;
            user.HeightInches = 80;
            user.WeightPounds = 180;
            user.Sex = "Female";

            var serializedObject = JsonConvert.SerializeObject(user);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PostAsync("User?rollback=true", content).Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            var user = GetUsers().FirstOrDefault(u => u.Username == "jryan");
            user.Name = "Updated Name";

            var serializedObject = JsonConvert.SerializeObject(user);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PutAsync($"User/{user.Id}?rollback=true", content).Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {
            var id = GetUsers().FirstOrDefault(u => u.Username == "jryan").Id;

            var response = client.DeleteAsync($"User/{id}?rollback=true").Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void LoginTest()
        {
            var user = new User();
            user.Username = "jryan";
            user.Password = "password1";

            var serializedObject = JsonConvert.SerializeObject(user);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PostAsync("User/Login", content).Result;
            var result = JsonConvert.DeserializeObject<User>(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(user.Username, result.Username);

        }

    }
}

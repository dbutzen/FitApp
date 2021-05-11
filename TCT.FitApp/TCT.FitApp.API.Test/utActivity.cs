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
    public class utActivity
    {
        public HttpClient client { get; }

        public utActivity()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<TCT.FitApp.API.Startup>();

            var testServer = new TestServer(webHostBuilder);

            client = testServer.CreateClient();
        }


        private List<Activity> GetActivities()
        {
            HttpResponseMessage response;
            string result;
            dynamic activities;

            response = client.GetAsync("Activity").Result;
            result = response.Content.ReadAsStringAsync().Result;
            activities = (JArray)JsonConvert.DeserializeObject(result);
            List<Activity> activityList = activities.ToObject<List<Activity>>();

            return activityList;
        }

        [TestMethod]
        public void LoadTest()
        {
            List<Activity> activities = GetActivities();

            Assert.AreEqual(4, activities.Count);

        }

        [TestMethod]
        public void LoadByIdTest()
        {
            HttpResponseMessage response;
            string result;

            var id = GetActivities().FirstOrDefault(i => i.Name == "Running").Id;

            response = client.GetAsync($"Activity/{id}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var activity = JsonConvert.DeserializeObject<Activity>(result);


            Assert.AreEqual("Running", activity.Name);

        }

        [TestMethod]
        public void InsertTest()
        {
            var activity = new Activity();
            activity.Name = "Eating";
            activity.EasyCaloriesPerHour = 10;
            activity.MediumCaloriesPerHour = 15;
            activity.HardCaloriesPerHour = 20;

            var serializedObject = JsonConvert.SerializeObject(activity);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PostAsync("Activity?rollback=true", content).Result;
            var result = bool.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void UpdateTest()
        {
            var activity = GetActivities().FirstOrDefault(i => i.Name == "Running");
            activity.Name = "Running Faster";

            var serializedObject = JsonConvert.SerializeObject(activity);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PutAsync($"Activity/{activity.Id}?rollback=true", content).Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {
            var id = GetActivities().FirstOrDefault(i => i.Name == "Running").Id;

            var response = client.DeleteAsync($"Activity/{id}?rollback=true").Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }


    }
}

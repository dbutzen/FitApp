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
using TCT.FitApp.BL;

namespace TCT.FitApp.API.Test
{
    [TestClass]
    public class utDayActivity
    {
        public HttpClient client { get; }

        public utDayActivity()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<TCT.FitApp.API.Startup>();

            var testServer = new TestServer(webHostBuilder);

            client = testServer.CreateClient();
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

        private List<DayActivity> GetDayActivities()
        {
            HttpResponseMessage response;
            string result;
            dynamic dayActivities;

            response = client.GetAsync("DayActivity").Result;
            result = response.Content.ReadAsStringAsync().Result;
            dayActivities = (JArray)JsonConvert.DeserializeObject(result);
            List<DayActivity> dayActivityList = dayActivities.ToObject<List<DayActivity>>();

            return dayActivityList;
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
            List<DayActivity> dayActivities = GetDayActivities();

            Assert.AreEqual(7, dayActivities.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            var activityId = GetActivities().FirstOrDefault(a => a.Name == "Running").Id;
            var dayId = GetDays().FirstOrDefault(d => d.Date == DateTime.Parse("03-12-21")).Id;

            DayActivity dayactivity = new DayActivity();
            dayactivity.Id = Guid.NewGuid();
            dayactivity.DayId = dayId;
            dayactivity.ActivityId = activityId;
            dayactivity.Duration = 45;
            dayactivity.DifficultyLevel = 2;

            var serializedObject = JsonConvert.SerializeObject(dayactivity);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PostAsync("DayActivity?rollback=true", content).Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var id = GetDays().FirstOrDefault(d => d.Date == DateTime.Parse("02-23-21")).Id;

            var response = client.DeleteAsync($"Day/{id}?rollback=true").Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteByIdsTest()
        {
            var dayActivity = GetDayActivities().FirstOrDefault();

            var response = client.DeleteAsync($"DayActivity/{dayActivity.DayId}/{dayActivity.ActivityId}?rollback=true").Result;
            var result = int.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.IsTrue(result > 0);

        }

    }
}

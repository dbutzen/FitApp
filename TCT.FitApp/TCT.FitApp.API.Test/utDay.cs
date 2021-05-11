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
    public class utDay
    {
        public HttpClient client { get; }

        public utDay()
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
            List<Day> days = GetDays();

            Assert.AreEqual(10, days.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            HttpResponseMessage response;
            string result;

            var id = GetDays().FirstOrDefault(d => d.Date == DateTime.Parse("03-12-21")).Id;

            response = client.GetAsync($"Day/{id}").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var day = JsonConvert.DeserializeObject<Day>(result);


            Assert.AreEqual(DateTime.Parse("03-12-21"), day.Date);
        }

        [TestMethod]
        public void LoadByUserIdAndDateTest()
        {
            HttpResponseMessage response;
            string result;

            var userId = GetUsers().FirstOrDefault(u => u.Username == "jryan").Id;

            response = client.GetAsync($"Day/{userId}/2021-03-11").Result;
            result = response.Content.ReadAsStringAsync().Result;
            var day = JsonConvert.DeserializeObject<Day>(result);


            Assert.AreEqual(DateTime.Parse("2021-03-11"), day.Date);
        }

        [TestMethod]
        public void InsertTest()
        {
            var userId = GetUsers().FirstOrDefault(u => u.Username == "cvanhefty").Id;

            Day day = new Day();
            day.Id = Guid.NewGuid();
            day.Date = DateTime.Now;
            day.CaloriesBurned = 0;
            day.CaloriesConsumed = 9999;
            day.ProteinConsumed = 56;
            day.Succeeded = false;
            day.Activities = null;
            day.Items = null;
            day.UserId = userId;

            var serializedObject = JsonConvert.SerializeObject(day);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PostAsync("Day?rollback=true", content).Result;
            var result = bool.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var day = GetDays().FirstOrDefault(d => d.Date == DateTime.Parse("02-23-21"));
            day.Succeeded = true;

            var serializedObject = JsonConvert.SerializeObject(day);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = client.PutAsync($"Day/{day.Id}?rollback=true", content).Result;
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
        public void LoadReportTest()
        {
            var userId = GetUsers().FirstOrDefault(u => u.Username == "jryan").Id;

            HttpResponseMessage response;
            string result;
            dynamic items;

            response = client.GetAsync($"Day/GenerateReport?userId={userId}&startDate=2021-03-11&endDate=2021-03-12").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Day> days = items.ToObject<List<Day>>();

            Assert.AreEqual(2, days.Count);
        }

        // Stored Procedure UT

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.BL.Test
{
    [TestClass]
    public class utDay
    {
        [TestMethod]
        public void LoadReportTest()
        {
            var userTask = UserManager.LoadByUsername("jryan");
            userTask.Wait();
            var user = userTask.Result;

            var task = DayManager.LoadReport(user.Id, new DateTime(2021, 03, 11), new DateTime(2021, 03, 12));
            task.Wait();
            Assert.AreEqual(2, task.Result.Count);
        }

        [TestMethod]
        public void LoadTest()
        {
            var task = DayManager.Load();
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(6, results.Count);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var taskd = DayManager.Load();
            taskd.Wait();
            var days = taskd.Result;

            Day day = days.FirstOrDefault(d => d.Date == DateTime.Parse("03-12-21"));
            day.Succeeded = true;

            var task = DayManager.Update(day, true);
            task.Wait();
            Assert.IsTrue(task.Result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var taskd = DayManager.Load();
            taskd.Wait();
            var days = taskd.Result;

            Day day = days.FirstOrDefault(d => d.Date == DateTime.Parse("03-12-21"));

            var task = DayManager.Delete(day.Id, true);
            task.Wait();
            Assert.IsTrue(task.Result > 0);
        }

        [TestMethod]
        public void InsertTest()
        {
            var utask = UserManager.LoadByUsername("cvanhefty");
            utask.Wait();
            var userId = utask.Result.Id;

            Day day = new Day();
            day.Id = Guid.NewGuid();
            day.UserId = userId;
            day.Succeeded = false;
            day.Date = DateTime.Parse("04-21-21");

            var task = DayManager.Insert(day, true);
            task.Wait();
            Assert.AreEqual(task.Result, true);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            var dtask = DayManager.Load();
            dtask.Wait();
            var days = dtask.Result;

            var day = days.FirstOrDefault(d => d.Date == DateTime.Parse("03-12-21"));

            var results = DayManager.LoadById(day.Id);
            results.Wait();
            Assert.AreEqual(1, results.Result.Count);
        }
    }
}

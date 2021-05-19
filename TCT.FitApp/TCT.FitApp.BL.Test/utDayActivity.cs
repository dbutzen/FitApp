using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.BL.Test
{
    [TestClass]
    public class utDayActivity
    {
        [TestMethod]
        public void InsertTest()
        {
            var atask = ActivityManager.Load();
            atask.Wait();
            var activities= atask.Result;

            var activity = activities.FirstOrDefault(a => a.Name == "Swimming");

            var dtask = DayManager.Load();
            dtask.Wait();
            var days = dtask.Result;

            var day = days.FirstOrDefault(d => d.Date == DateTime.Parse("03-12-21"));

            var task = DayActivityManager.Insert(day.Id, activity.Id, 60, 2, true);
            task.Wait();
            Assert.IsTrue(task.Result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var atask = ActivityManager.Load();
            atask.Wait();
            var activities = atask.Result;

            var activity = activities.FirstOrDefault(a => a.Name == "Swimming");

            var dtask = DayManager.Load();
            dtask.Wait();
            var days = dtask.Result;

            var day = days.FirstOrDefault(d => d.Date == DateTime.Parse("03-12-21"));

            var task = DayActivityManager.Load();
            task.Wait();
            var dayActivities = task.Result;

            var dayActivity = dayActivities.FirstOrDefault(da => da.ActivityId == activity.Id && da.DayId == day.Id);

            var datask = DayActivityManager.Delete(dayActivity.Id, true);
            datask.Wait();
            Assert.IsTrue(datask.Result > 0);
        }

        [TestMethod]
        public void DeleteByIdsTest()
        {
            var itask = ActivityManager.Load();
            itask.Wait();
            var activities = itask.Result;

            var activity = activities.FirstOrDefault(a => a.Name == ("Swimming"));

            var dtask = DayManager.Load();
            dtask.Wait();
            var days = dtask.Result;

            var day = days.FirstOrDefault(d => d.Date == DateTime.Parse("03-01-21"));

            var task = DayActivityManager.Load();
            task.Wait();
            var dayActivities = task.Result;

            var dayActivity = dayActivities.FirstOrDefault(da => da.ActivityId == activity.Id && da.DayId == day.Id);

            var ditask = DayActivityManager.Delete(dayActivity.DayId, dayActivity.ActivityId, true);
            ditask.Wait();
            Assert.IsTrue(ditask.Result > 0);
        }

        [TestMethod]
        public void LoadTest()
        {
            var task = DayActivityManager.Load();
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(7, results.Count);
        }
    }
}

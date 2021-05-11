using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.BL.Test
{
    [TestClass]
    public class utActivity
    {

        [TestMethod]
        public void LoadTest()
        {

            var task = ActivityManager.Load();
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(4, results.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            var task = ActivityManager.Load();
            task.Wait();
            var id = task.Result.First().Id;

            var task2 = ActivityManager.LoadById(id);
            task2.Wait();
            var results = task2.Result;
            Assert.AreEqual(id, results.Id);
        }


        [TestMethod]
        public void InsertTest()
        {

            var activity = new Activity();
            activity.Name = "New Activity";
            activity.Id = Guid.NewGuid();

            var task = ActivityManager.Insert(activity, true);
            task.Wait();

            Assert.IsTrue(task.Result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var loadTask = ActivityManager.Load();
            loadTask.Wait();
            var activities = loadTask.Result;
            var activity = activities.FirstOrDefault(i => i.Name == "Swimming");
            activity.Name = "Falling With Style";
            var task = ActivityManager.Update(activity, true);
            task.Wait();

            Assert.IsTrue(task.Result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var loadTask = ActivityManager.Load();
            loadTask.Wait();
            var activities = loadTask.Result;

            var activity = activities.FirstOrDefault(i => i.Name == "Swimming");
            var task = ActivityManager.Delete(activity.Id, true);
            task.Wait();

            Assert.IsTrue(task.Result > 0);
        }



    }
}

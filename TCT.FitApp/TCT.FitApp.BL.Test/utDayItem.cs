using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.BL.Test
{
    [TestClass]
    public class utDayItem
    {
        [TestMethod]
        public void InsertTest()
        {
            var itask = ItemManager.LoadByName("strawberry");
            itask.Wait();
            var itemId = itask.Result.Id;

            var dtask = DayManager.Load();
            dtask.Wait();
            var days = dtask.Result;

            var day = days.FirstOrDefault(d => d.Date == DateTime.Parse("03-12-21"));

            var task = DayItemManager.Insert(day.Id, itemId, 2, true);
            task.Wait();
            Assert.IsTrue(task.Result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var itask = ItemManager.LoadByName("strawberry");
            itask.Wait();
            var itemId = itask.Result.Id;

            var dtask = DayManager.Load();
            dtask.Wait();
            var days = dtask.Result;

            var day = days.FirstOrDefault(d => d.Date == DateTime.Parse("03-01-21"));

            var task = DayItemManager.Load();
            task.Wait();
            var dayItems= task.Result;

            var dayItem = dayItems.FirstOrDefault(di => di.ItemId == itemId && di.DayId == day.Id);
                        
            var ditask = DayItemManager.Delete(dayItem.Id, true);
            ditask.Wait();
            Assert.IsTrue(ditask.Result > 0);
        }

        [TestMethod]
        public void LoadTest()
        {
            var task = DayItemManager.Load();
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(13, results.Count);
        }
    }
}

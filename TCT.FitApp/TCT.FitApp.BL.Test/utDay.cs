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

    }
}

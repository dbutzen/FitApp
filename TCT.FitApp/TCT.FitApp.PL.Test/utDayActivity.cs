using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TCT.FitApp.PL.Test
{
    [TestClass]
    public class utDayActivity
    {
        protected FitAppEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new FitAppEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            transaction.Rollback();
            transaction.Dispose();
        }



        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(7, dc.TblDayActivities.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            var row = new TblDayActivity();
            row.Id = Guid.NewGuid();
            row.DayId = dc.TblDays.FirstOrDefault(d => d.Date == new DateTime(2021, 03, 11)).Id;
            row.ActivityId = dc.TblActivities.FirstOrDefault(a => a.Name == "Swimming").Id;
            row.Duration = 35;
            row.DifficultyLevel = 2;

            dc.TblDayActivities.Add(row);
            var result = dc.SaveChanges();


            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            var row = dc.TblDayActivities.FirstOrDefault();
            var id = row.Id;

            if (row != null)
            {
                row.Duration = 30;
                dc.SaveChanges();
            }

            var dayActivity = dc.TblDayActivities.FirstOrDefault(d => d.Id == id);

            Assert.AreEqual(row.Duration, dayActivity.Duration);

        }

        [TestMethod]
        public void DeleteTest()
        {

            var row = dc.TblDayActivities.FirstOrDefault();
            var id = row.Id;

            if (row != null)
            {
                dc.TblDayActivities.Remove(row);
                dc.SaveChanges();
            }

            var deletedrow = dc.TblDayActivities.FirstOrDefault(d => d.Id == id);

            Assert.IsNull(deletedrow);
        }

    }
}


using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TCT.FitApp.PL.Test
{
    [TestClass]
    public class utActivity
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
            Assert.AreEqual(4, dc.TblActivities.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            var row = new TblActivity();
            row.Id = Guid.NewGuid();
            row.Name = "New Activity";
            row.EasyCaloriesPerHour = 100;
            row.MediumCaloriesPerHour = 200;
            row.HardCaloriesPerHour = 300;

            dc.TblActivities.Add(row);
            var result = dc.SaveChanges();


            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            var row = dc.TblActivities.FirstOrDefault(u => u.Name == "New Activity");

            if (row != null)
            {
                row.EasyCaloriesPerHour = 50;
                dc.SaveChanges();
            }

            var activity = dc.TblActivities.FirstOrDefault(u => u.Name == "New Activity");

            Assert.AreEqual(row.EasyCaloriesPerHour, activity.EasyCaloriesPerHour);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            var row = dc.TblActivities.FirstOrDefault(u => u.Name == "New Activity");

            if (row != null)
            {
                dc.TblActivities.Remove(row);
                dc.SaveChanges();
            }

            var deletedrow = dc.TblActivities.FirstOrDefault(u => u.Name == "New Activity");

            Assert.IsNull(deletedrow);
        }

    }
}


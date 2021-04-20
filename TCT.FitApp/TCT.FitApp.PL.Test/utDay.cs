using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TCT.FitApp.PL.Test
{
    [TestClass]
    public class utDay
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
            Assert.AreEqual(4, dc.TblDays.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            var row = new TblDay();
            row.Id = Guid.NewGuid();
            row.UserId = dc.TblUsers.FirstOrDefault(u => u.Name == "Jason Ryan").Id;
            row.Date = DateTime.Today;
            row.Succeeded = true;

            dc.TblDays.Add(row);
            var result = dc.SaveChanges();


            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            var row = dc.TblDays.FirstOrDefault(u => u.Date == DateTime.Today);

            if (row != null)
            {
                row.Succeeded = false;
                dc.SaveChanges();
            }

            var day = dc.TblDays.FirstOrDefault(u => u.Date == DateTime.Today);

            Assert.AreEqual(row.Succeeded, day.Succeeded);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            var row = dc.TblDays.FirstOrDefault(u => u.Date == DateTime.Today);

            if (row != null)
            {
                dc.TblDays.Remove(row);
                dc.SaveChanges();
            }

            var deletedrow = dc.TblDays.FirstOrDefault(u => u.Date == DateTime.Today);

            Assert.IsNull(deletedrow);
        }

    }
}


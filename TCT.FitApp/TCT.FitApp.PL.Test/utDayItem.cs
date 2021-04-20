using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TCT.FitApp.PL.Test
{
    [TestClass]
    public class utDayItem
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
            Assert.AreEqual(3, dc.TblDayItems.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            var row = new TblDayItem();
            row.Id = Guid.NewGuid();
            row.DayId = dc.TblDays.FirstOrDefault(d => d.Date == new DateTime(2021, 03, 11)).Id;
            row.ItemId = dc.TblItems.FirstOrDefault(i => i.Name == "Strawberry").Id;
            row.Servings = 4;

            dc.TblDayItems.Add(row);
            var result = dc.SaveChanges();


            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            var row = dc.TblDayItems.FirstOrDefault();
            var id = row.Id;

            if (row != null)
            {
                row.Servings = 2;
                dc.SaveChanges();
            }

            var dayItem = dc.TblDayItems.FirstOrDefault(d => d.Id == id);

            Assert.AreEqual(row.Servings, dayItem.Servings);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            var row = dc.TblDayItems.FirstOrDefault();
            var id = row.Id;

            if (row != null)
            {
                dc.TblDayItems.Remove(row);
                dc.SaveChanges();
            }

            var deletedrow = dc.TblDayItems.FirstOrDefault(d => d.Id == id);

            Assert.IsNull(deletedrow);
        }

    }
}


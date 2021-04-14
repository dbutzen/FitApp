using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TCT.FitApp.PL.Test
{
    [TestClass]
    public class utItem
    {
        protected FitAppDataContext dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new FitAppDataContext();
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
            Assert.AreEqual(3, dc.TblItems.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            var row = new TblItem();
            row.Id = Guid.NewGuid();
            row.Name = "Banana";
            row.TypeId = dc.TblItemTypes.FirstOrDefault(t => t.Name == "Food").Id;
            row.Calories = 105;
            row.Protein = 1;
            dc.TblItems.Add(row);
            var result = dc.SaveChanges();


            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            var row = dc.TblItems.FirstOrDefault(i => i.Name == "Banana");

            if (row != null)
            {
                row.Calories = 100;
                dc.SaveChanges();
            }

            var item = dc.TblItems.FirstOrDefault(i => i.Name == "Banana");

            Assert.AreEqual(row.Calories, item.Calories);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            var row = dc.TblItems.FirstOrDefault(i => i.Name == "Banana");

            if (row != null)
            {
                dc.TblItems.Remove(row);
                dc.SaveChanges();
            }

            var deletedrow = dc.TblItems.FirstOrDefault(i => i.Name == "Banana");

            Assert.IsNull(deletedrow);
        }

    }
}


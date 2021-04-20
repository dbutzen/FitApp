using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TCT.FitApp.PL.Test
{
    [TestClass]
    public class utUserAccessLevel
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
            Assert.AreEqual(3, dc.TblUserAccessLevels.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            var row = new TblUserAccessLevel();
            row.Id = Guid.NewGuid();
            row.Name = "New Name";
            row.Description = "New Description";

            dc.TblUserAccessLevels.Add(row);
            var result = dc.SaveChanges();


            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            var row = dc.TblUserAccessLevels.FirstOrDefault(u => u.Name == "New Name");

            if (row != null)
            {
                row.Description = "Updated Description";
                dc.SaveChanges();
            }

            var userAccessLevel = dc.TblUserAccessLevels.FirstOrDefault(u => u.Name == "New Name");

            Assert.AreEqual(row.Description, userAccessLevel.Description);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            var row = dc.TblUserAccessLevels.FirstOrDefault(u => u.Name == "New Name");

            if (row != null)
            {
                dc.TblUserAccessLevels.Remove(row);
                dc.SaveChanges();
            }

            var deletedrow = dc.TblUserAccessLevels.FirstOrDefault(u => u.Name == "New Name");

            Assert.IsNull(deletedrow);
        }

    }
}


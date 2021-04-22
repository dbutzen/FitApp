using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        [TestMethod]
        public void LoadReportTest()
        {
            var paramUserId = new SqlParameter()
            {
                ParameterName = "UserId",
                SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                Value = dc.TblUsers.FirstOrDefault(u => u.Username == "jryan").Id
            };

            var paramStartDate = new SqlParameter()
            {
                ParameterName = "StartDate",
                SqlDbType = System.Data.SqlDbType.Date,
                Value = new DateTime(2021, 03, 11)
            };

            var paramEndDate = new SqlParameter()
            {
                ParameterName = "EndDate",
                SqlDbType = System.Data.SqlDbType.Date,
                Value = new DateTime(2021, 03, 12)
            };

            var results = dc.Set<spGenerateReport>().FromSqlRaw("exec spGenerateReport @UserId, @StartDate, @EndDate", paramUserId, paramStartDate, paramEndDate).ToList();

            Assert.AreEqual(2, results.Count());

        }

    }
}


using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;


namespace TCT.FitApp.PL.Test
{
    [TestClass]
    public class utSpGenerateReport
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
        public void GenerateTest()
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

            Assert.AreEqual(2, results.Count);

        }
    }
}
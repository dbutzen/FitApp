using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JZR.UserTracker.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
using TCT.FitApp.PL;
using System.Security.Cryptography;
using System.Text;

namespace JZR.UserTracker.PL.Test
{
    [TestClass]
    public class utUser
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
            Assert.AreEqual(3, dc.tblUsers.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            var row = new tblUser();
            row.Id = Guid.NewGuid();
            row.Name = "New Guy";
            row.Username = "imanewguy";
            row.UniqueKey = Guid.NewGuid();
            row.Password = "pass";
            row.Password = ComputeSha256Hash($"{row.Password}{row.UniqueKey.ToString().ToUpper()}");
            row.CalorieGoal = 2000;
            row.ProteinGoal = 100;
            row.DaysInArowSucceeded = 13;
            row.HeightInches = 75;
            row.WeightPounds = 180;
            row.UserAccessLevelId = dc.tblUserAccessLevels.FirstOrDefault(a => a.Name == "User").Id;
            row.Sex = "Male";

            dc.tblUsers.Add(row);
            var result = dc.SaveChanges();


            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            var row = dc.tblUsers.FirstOrDefault(u => u.Username == "imanewguy");

            if (row != null)
            {
                row.Name = "Old Guy";
                dc.SaveChanges();
            }

            var user = dc.tblUsers.FirstOrDefault(u => u.Username == "imanewguy");

            Assert.AreEqual(row.Name, user.Name);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            var row = dc.tblUsers.FirstOrDefault(u => u.Username == "imanewguy");

            if (row != null)
            {
                dc.tblUsers.Remove(row);
                dc.SaveChanges();
            }

            var deletedrow = dc.tblUsers.FirstOrDefault(u => u.Username == "imanewguy");

            Assert.IsNull(deletedrow);
        }


        private static string ComputeSha256Hash(string rawData)
        {
            {
                using (var sha = SHA256.Create())
                {
                    var data = sha.ComputeHash(Encoding.Unicode.GetBytes(rawData));

                    var builder = new StringBuilder();

                    foreach (var d in data)
                    {
                        builder.Append(d.ToString("X2"));
                    }
                    return builder.ToString();
                }
            }
        }
    }
}

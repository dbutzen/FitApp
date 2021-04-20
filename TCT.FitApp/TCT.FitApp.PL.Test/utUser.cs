using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TCT.FitApp.PL.Test
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
            Assert.AreEqual(3, dc.TblUsers.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            var row = new TblUser();
            row.Id = Guid.NewGuid();
            row.Name = "New User";
            row.Username = "newuser";
            row.UniqueKey = Guid.NewGuid();
            row.Password = "pass";
            row.Password = ComputeSha256Hash($"{row.Password}{row.UniqueKey.ToString().ToUpper()}");
            row.CalorieGoal = 2000;
            row.ProteinGoal = 100;
            row.DaysInARowSucceeded = 13;
            row.HeightInches = 75;
            row.WeightPounds = 180;
            row.UserAccessLevelId = dc.TblUserAccessLevels.FirstOrDefault(a => a.Name == "User").Id;
            row.Sex = "Male";

            dc.TblUsers.Add(row);
            var result = dc.SaveChanges();


            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            var row = dc.TblUsers.FirstOrDefault(u => u.Username == "newuser");

            if (row != null)
            {
                row.Name = "Old User";
                dc.SaveChanges();
            }

            var user = dc.TblUsers.FirstOrDefault(u => u.Username == "newuser");

            Assert.AreEqual(row.Name, user.Name);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            var row = dc.TblUsers.FirstOrDefault(u => u.Username == "newuser");

            if (row != null)
            {
                dc.TblUsers.Remove(row);
                dc.SaveChanges();
            }

            var deletedrow = dc.TblUsers.FirstOrDefault(u => u.Username == "newuser");

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


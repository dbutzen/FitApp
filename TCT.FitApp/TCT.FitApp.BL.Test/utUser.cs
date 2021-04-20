using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.BL.Test
{
    [TestClass]
    public class utUser
    {

        [TestMethod]
        public void LoadTest()
        {

            var task = UserManager.Load();
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            var task = UserManager.LoadByUsername("jryan");
            task.Wait();
            var id = task.Result.Id;

            task = UserManager.LoadById(id);
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(id, results.Id);
        }

        [TestMethod]
        public void LoadByUsernameTest()
        {
            var task = UserManager.LoadByUsername("jryan");
            task.Wait();
            var results = task.Result;
            Assert.AreEqual("jryan", results.Username);
        }

        [TestMethod]
        public void LoginTest()
        {
            var user = new User();
            user.Username = "jryan";
            user.Password = "password1";
            var task = UserManager.Login(user);
            task.Wait();
            Assert.AreEqual("Jason Ryan", user.Name);
        }

        [TestMethod]
        public void ChangePasswordTest()
        {
            var loadTask = UserManager.LoadByUsername("jryan");
            loadTask.Wait();
            var user = loadTask.Result;
            user.Password = "password1"; // <-- Old password must be entered
            var task = UserManager.ChangePassword(user, "newpass", true);
            task.Wait();
            Assert.IsTrue(task.Result > 0);
        }


        [TestMethod]
        public void InsertTest()
        {
            var user = new User();
            user.Name = "New User";
            user.Username = "newuser123";
            user.Password = "pass";
            user.CalorieGoal = 1500;
            user.ProteinGoal = 99;
            user.DaysInARowSucceeded = 10;
            user.HeightInches = 80;
            user.WeightPounds = 180;
            user.Sex = "Female";

            var task =  UserManager.Insert(user, true);
            task.Wait();

            Assert.IsTrue(task.Result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var loadTask = UserManager.Load();
            loadTask.Wait();
            var users = loadTask.Result;
            var user = users.FirstOrDefault(u => u.Username == "jryan");
            user.Name = "Updated User";
            var task = UserManager.Update(user, true);
            task.Wait();

            Assert.IsTrue(task.Result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var loadTask = UserManager.Load();
            loadTask.Wait();
            var users = loadTask.Result;

            var user = users.FirstOrDefault(u => u.Username == "jryan");
            var task = UserManager.Delete(user.Id, true);
            task.Wait();

            Assert.IsTrue(task.Result > 0);
        }



    }
}

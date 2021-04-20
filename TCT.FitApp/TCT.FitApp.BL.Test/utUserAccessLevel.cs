using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.BL.Test
{
    [TestClass]
    public class utUserAccessLevel
    {
        [TestMethod]
        public void LoadTest()
        {
            var task = UserAccessLevelManager.Load();
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            var task = UserAccessLevelManager.Insert(new UserAccessLevel { Name = "Compliance", Description = "Read/Write/Modify" }, true);
            task.Wait();
            Assert.IsTrue(task.Result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var task = UserAccessLevelManager.Load();
            task.Wait();
            var userAccessLevels = task.Result;
            UserAccessLevel userAccessLevel = userAccessLevels.FirstOrDefault(ual => ual.Name == "Super User");
            userAccessLevel.Name = "Superhuman User";
            var results = UserAccessLevelManager.Update(userAccessLevel, true);
            results.Wait();
            Assert.IsTrue(results.Result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var task = UserAccessLevelManager.Load();
            task.Wait();
            var userAccessLevels = task.Result;
            UserAccessLevel userAccessLevel = userAccessLevels.FirstOrDefault(ual => ual.Name == "Super User");
            var results = UserAccessLevelManager.Delete(userAccessLevel.Id, true);
            results.Wait();
            Assert.IsTrue(results.Result > 0);
        }

        [TestMethod]
        public void LoadByNameTest()
        {
            var task = UserAccessLevelManager.LoadByName("Super User");
            task.Wait();
            var results = task.Result;
            Assert.AreEqual("Modify", results.Description);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            var task = UserAccessLevelManager.LoadByName("Super User");
            task.Wait();
            var id = task.Result.Id;

            task = UserAccessLevelManager.LoadById(id);
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(id, results.Id);
        }
    }
}

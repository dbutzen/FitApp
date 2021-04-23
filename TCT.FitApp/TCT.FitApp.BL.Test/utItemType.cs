using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.BL.Test
{
    [TestClass]
    public class utItemType
    {

        [TestMethod]
        public void LoadTest()
        {

            var task = ItemTypeManager.Load();
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            var task = ItemTypeManager.Load();
            task.Wait();
            var id = task.Result.FirstOrDefault(i => i.Name == "Food").Id;

            var task2 = ItemTypeManager.LoadById(id);
            task2.Wait();
            var results = task2.Result;
            Assert.AreEqual(id, results.Id);
        }


        [TestMethod]
        public void InsertTest()
        {

            var itemType = new ItemType();
            itemType.Name = "New ItemType";
            itemType.Id = Guid.NewGuid();

            var task = ItemTypeManager.Insert(itemType, true);
            task.Wait();

            Assert.IsTrue(task.Result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var loadTask = ItemTypeManager.Load();
            loadTask.Wait();
            var itemTypes = loadTask.Result;
            var itemType = itemTypes.FirstOrDefault(i => i.Name == "Food");
            itemType.Name = "Munchies";
            var task = ItemTypeManager.Update(itemType, true);
            task.Wait();

            Assert.IsTrue(task.Result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var loadTask = ItemTypeManager.Load();
            loadTask.Wait();
            var itemTypes = loadTask.Result;

            var itemType = itemTypes.FirstOrDefault(i => i.Name == "Food");
            var task = ItemTypeManager.Delete(itemType.Id, true);
            task.Wait();

            Assert.IsTrue(task.Result > 0);
        }



    }
}
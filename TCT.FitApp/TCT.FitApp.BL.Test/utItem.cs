using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.BL.Test
{
    [TestClass]
    public class utItem
    {

        [TestMethod]
        public void LoadTest()
        {

            var task = ItemManager.Load();
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(12, results.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            var task = ItemManager.LoadByName("Strawberry");
            task.Wait();
            var id = task.Result.Id;

            task = ItemManager.LoadById(id);
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(id, results.Id);
        }

        [TestMethod]
        public void LoadByNameTest()
        {
            var task = ItemManager.LoadByName("Strawberry");
            task.Wait();
            var results = task.Result;
            Assert.AreEqual("Strawberry", results.Name);
        }


        [TestMethod]
        public void LoadByTypeIdTest()
        {
            var loadTask = ItemTypeManager.Load();
            loadTask.Wait();
            var types = loadTask.Result;
            var task = ItemManager.LoadByTypeId(types.FirstOrDefault(t => t.Name == "Food").Id);
            task.Wait();
            var results = task.Result;
            Assert.AreEqual(9, results.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            var loadTask = ItemTypeManager.Load();
            loadTask.Wait();
            var types = loadTask.Result;

            var item = new Item();
            item.Name = "New Item";
            item.TypeId = types.FirstOrDefault(t => t.Name == "Food").Id;
            item.Calories = 10;
            item.Protein = 7;

            var task = ItemManager.Insert(item, true);
            task.Wait();

            Assert.IsTrue(task.Result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var loadTask = ItemManager.Load();
            loadTask.Wait();
            var items = loadTask.Result;
            var item = items.FirstOrDefault(i => i.Name == "Strawberry");
            item.Name = "Updated Item";
            var task = ItemManager.Update(item, true);
            task.Wait();

            Assert.IsTrue(task.Result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var loadTask = ItemManager.Load();
            loadTask.Wait();
            var items = loadTask.Result;

            var item = items.FirstOrDefault(i => i.Name == "Strawberry");
            var task = ItemManager.Delete(item.Id, true);
            task.Wait();

            Assert.IsTrue(task.Result > 0);
        }



    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TCT.FitApp.BL;

namespace JZR.SurveyMaker.BL.Test
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
            Assert.AreEqual(3, results.Count);
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

        //   ItemTypeManager.Load() is REQUIRED - I will fix this later - JR

        //[TestMethod]
        //public void InsertTest()
        //{
        //    var loadTask = ItemTypeManager.Load();
        //    loadTask.Wait();
        //    var itemType = loadTask.Result;

        //    var item = new Item();
        //    item.Name = "New Item";
        //    item.TypeId = itemType.Id;
        //    item.Calories = 10;
        //    item.Protein = 7;

        //    var task = ItemManager.Insert(item, true);
        //    task.Wait();

        //    Assert.IsTrue(task.Result > 0);
        //}

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

using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCT.FitApp.BL.Models;
using TCT.FitApp.PL;

namespace TCT.FitApp.BL
{
    public static class ItemManager
    {

        public async static Task<int> Insert(Item item, bool rollback = false)
        {
            try
            {
                var results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;

                    using (var dc = new FitAppDataContext())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        var row = new TblItem();

                        row.Id = Guid.NewGuid();
                        row.Name = item.Name;
                        row.TypeId = item.TypeId;
                        row.Calories = item.Calories;
                        row.Protein = item.Protein;
                        row.UserId = item.CreatedUserId;

                        item.Id = row.Id;
 
                        dc.TblItems.Add(row);

                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                });

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<int> Update(Item item, bool rollback = false)
        {
            try
            {
                var results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;

                    using (var dc = new FitAppDataContext())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        var row = dc.TblItems.FirstOrDefault(i => i.Id == item.Id);

                        row.Name = item.Name;
                        row.TypeId = item.TypeId;
                        row.Calories = item.Calories;
                        row.Protein = item.Protein;
                        row.UserId = item.CreatedUserId;

                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                });

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                var results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;

                    using (var dc = new FitAppDataContext())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        var row = dc.TblItems.FirstOrDefault(i => i.Id == id);
                        dc.TblItems.Remove(row);
                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                });

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<List<Item>> Load()
        {
            try
            {
                var items = new List<Item>();
                await Task.Run(() =>
                {
                    using (var dc = new FitAppDataContext())
                    {
                        dc.TblItems
                        .ToList()
                        .ForEach(u =>
                        {
                            var item = new Item();
                            GetFromTableRow(item, u);
                            items.Add(item);
                        });
                    }
                });

                return items;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<Item> LoadById(Guid id)
        {
            try
            {
                var item = new Item();
                await Task.Run(() =>
                {
                    using (var dc = new FitAppDataContext())
                    {
                        var row = dc.TblItems.FirstOrDefault(u => u.Id == id);
                        if (row != null)
                        {
                            GetFromTableRow(item, row);
                        }
                        else
                        {
                            throw new Exception("Item could not be found");
                        }

                    }
                });

                return item;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<Item> LoadByName(string name)
        {
            try
            {
                var item = new Item();
                await Task.Run(() =>
                {
                    using (var dc = new FitAppDataContext())
                    {
                        var row = dc.TblItems.FirstOrDefault(u => u.Name == name);
                        if (row != null)
                        {
                            GetFromTableRow(item, row);
                        }
                        else
                        {
                            throw new Exception("Item could not be found");
                        }
                    }
                });

                return item;

            }
            catch (Exception)
            {
                throw;
            }
        }


        public async static Task<List<Item>> LoadByTypeId(Guid typeId)
        {
            try
            {
                var items = new List<Item>();
                await Task.Run(() =>
                {
                    using (var dc = new FitAppDataContext())
                    {
                        dc.TblItems.Where(u => u.TypeId == typeId)
                        .ToList()
                        .ForEach(u =>
                        {
                            var item = new Item();
                            GetFromTableRow(item, u);
                            items.Add(item);
                        });
                    }
                });

                return items;

            }
            catch (Exception)
            {
                throw;
            }
        }


        private static void GetFromTableRow(Item item, TblItem row)
        {
            item.Id = row.Id;
            item.Name = row.Name;
            item.TypeId = row.TypeId;
            item.Calories = row.Calories;
            item.Protein = row.Protein;
            item.CreatedUserId = row.UserId;
        }
    }
}

using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCT.FitApp.BL.Models;
using TCT.FitApp.PL;

namespace TCT.FitApp.BL
{
    public static class ItemTypeManager
    {
        public async static Task<List<ItemType>> Load()
        {
            try
            {
                List<ItemType> itemTypes = new List<ItemType>();
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        foreach (TblItemType it in dc.TblItemTypes.ToList())
                        {
                            ItemType itemType = new ItemType { Id = it.Id, Name = it.Name};
                            itemTypes.Add(itemType);
                            
                        }
                    }
                });
                return itemTypes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<List<ItemType>> LoadById(Guid itemTypeId)
        {
            try
            {
                List<ItemType> itemTypes = new List<ItemType>();
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        foreach (TblItemType it in dc.TblItemTypes.ToList())
                        {
                                if (it.Id == itemTypeId)
                                {
                                    ItemType itemType = new ItemType
                                    {
                                        Id = it.Id,
                                        Name = it.Name
                                    };
                                    itemTypes.Add(itemType);
                                }
                        }
                    }
                });
                return itemTypes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<bool> Insert(ItemType itemType, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        TblItemType newrow = new TblItemType();
                        newrow.Id = Guid.NewGuid();
                        newrow.Name = itemType.Name;

                        itemType.Id = newrow.Id;

                        dc.TblItemTypes.Add(newrow);
                        int results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();

                        if (results > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                });
                return true;
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

                    using (var dc = new FitAppEntities())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        var row = dc.TblItemTypes.FirstOrDefault(i => i.Id == id);
                        dc.TblItemTypes.Remove(row);
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

        public async static Task<int> Update(ItemType itemType, bool rollback = false)
        {
            try
            {
                var results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;

                    using (var dc = new FitAppEntities())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        var row = dc.TblItemTypes.FirstOrDefault(i => i.Id == itemType.Id);

                        row.Name = itemType.Name;

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

       
    }
}

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

        public async static Task<ItemType> LoadById(Guid itemTypeId)
        {
            try
            {
                ItemType itemType = new ItemType();
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        var row = dc.TblActivities.FirstOrDefault(a => a.Id == itemTypeId);

                        if (row != null)
                        {
                            itemType.Id = row.Id;
                            itemType.Name = row.Name;
                        }
                        else
                            throw new Exception("Row could not be found");

                    }
                });
                return itemType;
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
                IDbContextTransaction transaction = null;
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        TblItemType row = dc.TblItemTypes.FirstOrDefault(c => c.Id == id);
                        int results = 0;
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.TblItemTypes.Remove(row);
                            results = dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                            return results;
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }

                });
                throw new Exception("Danger, Will Robinson!");
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
                IDbContextTransaction transaction = null;
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        TblItemType row = dc.TblItemTypes.FirstOrDefault(c => c.Id == itemType.Id);
                        int results = 0;
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.Name = itemType.Name;

                            results = dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                            return results;
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                });
                throw new Exception("Danger, Will Robinson!");
            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}

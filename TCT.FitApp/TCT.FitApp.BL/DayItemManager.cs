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
    public static class DayItemManager
    {
        public static async Task<int> Insert(Guid dayId, Guid itemId, int servings, bool rollback = false)
        {
            try
            {
                int results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        TblDayItem row = new TblDayItem();
                        row.Id = Guid.NewGuid();
                        row.ItemId = itemId;
                        row.DayId = dayId;
                        row.Servings = servings;

                        dc.TblDayItems.Add(row);
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

        public static async Task<int> InsertWithDayItem(DayItem dayItem, bool rollback = false)
        {
            try
            {
                int results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();
                        TblDayItem row = new TblDayItem();
                        row.Id = Guid.NewGuid();
                        row.ItemId = dayItem.ItemId;
                        row.DayId = dayItem.DayId;
                        row.Servings = dayItem.Servings;

                        dc.TblDayItems.Add(row);
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

        public static async Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                int results = 0;
                await Task.Run(() =>
                {
                    IDbContextTransaction transaction = null;
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        if (rollback == true) transaction = dc.Database.BeginTransaction();
                        TblDayItem row = dc.TblDayItems.FirstOrDefault(qa => qa.Id == id);
                        if (row != null)
                        {
                            dc.TblDayItems.Remove(row);
                            results = dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                });
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<DayItem>> Load()
        {
            try
            {
                List<DayItem> dayItems= new List<DayItem>();

                await Task.Run(() =>
                {
                    using (var dc = new FitAppEntities())
                    {
                        dc.TblDayItems
                            .ToList()
                            .ForEach(di => dayItems.Add(new DayItem
                            {
                                Id = di.Id,
                                DayId = di.DayId,
                                ItemId = di.ItemId,
                                Servings = di.Servings
                            }));
                    }
                });
                return dayItems;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

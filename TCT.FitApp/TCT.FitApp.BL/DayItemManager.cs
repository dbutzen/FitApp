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
                        int results = dc.SaveChanges();
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
                        return results;
                    }
                    else
                    {
                        throw new Exception("Row was not found.");
                    }
                }
            });
            return results;
        }

    }
}

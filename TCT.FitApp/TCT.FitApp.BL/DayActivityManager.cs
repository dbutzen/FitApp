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
    public static class DayActivityManager
    {
        public static async Task<int> Insert(Guid dayId, Guid activityId, int duration, int difficultyLevel, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                using (FitAppEntities dc = new FitAppEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    TblDayActivity row = new TblDayActivity();
                    row.Id = Guid.NewGuid();
                    row.ActivityId = activityId;
                    row.DayId = dayId;
                    row.Duration = duration;
                    row.DifficultyLevel = difficultyLevel;
                    dc.TblDayActivities.Add(row);
                    int results = dc.SaveChanges();
                    if (rollback) transaction.Rollback();
                    return results;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static async Task<int> Delete(Guid id, bool rollback = false)
        {
            IDbContextTransaction transaction = null;
            using (FitAppEntities dc = new FitAppEntities())
            {
                if (rollback == true) transaction = dc.Database.BeginTransaction();
                TblDayActivity row = dc.TblDayActivities.FirstOrDefault(qa => qa.Id == id);
                int results = 0;
                if (row != null)
                {
                    dc.TblDayActivities.Remove(row);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                    return results;
                }
                else
                {
                    throw new Exception("Row was not found.");
                }
            }
        }
        
    }
}

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
                int results = 0;
                await Task.Run(() =>
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
                        TblDayActivity row = dc.TblDayActivities.FirstOrDefault(qa => qa.Id == id);

                        if (row != null)
                        {
                            dc.TblDayActivities.Remove(row);
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

        public static async Task<List<DayActivity>> Load()
        {
            try
            {
                List<DayActivity> dayActivites = new List<DayActivity>();

                await Task.Run(() =>
                {
                    using (var dc = new FitAppEntities())
                    {
                        dc.TblDayActivities
                            .ToList()
                            .ForEach(da => dayActivites.Add(new DayActivity
                            {
                                Id = da.Id,
                                DayId = da.DayId,
                                ActivityId = da.ActivityId,
                                DifficultyLevel = da.DifficultyLevel,
                                Duration = da.Duration,
                            }));
                    }
                });
                return dayActivites;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

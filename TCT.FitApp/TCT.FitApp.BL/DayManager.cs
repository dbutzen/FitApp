using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCT.FitApp.PL;
using TCT.FitApp.BL.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace TCT.FitApp.BL
{
    public static class DayManager
    {
        public async static Task<List<Day>> Load()
        {
            try
            {
                List<Day> days = new List<Day>();
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        foreach (TblDay d in dc.TblDays.ToList())
                        {
                            Day day = new Day { Id = d.Id, UserId = d.UserId, Date = d.Date, Succeeded = d.Succeeded };
                            day.Activities = new List<Activity>();
                            foreach (TblDayActivity da in d.TblDayActivities.ToList())
                            {
                                Activity activity = new Activity
                                {
                                    Id = da.ActivityId,
                                    Name = da.Activity.Name,
                                    EasyCaloriesPerHour = da.Activity.EasyCaloriesPerHour,
                                    MediumCaloriesPerHour = da.Activity.MediumCaloriesPerHour,
                                    HardCaloriesPerHour = da.Activity.HardCaloriesPerHour
                                };
                                day.Activities.Add(activity);
                            }
                            day.Items = new List<Item>();
                            foreach (TblDayItem di in d.TblDayItems.ToList())
                            {
                                Item item = new Item
                                {
                                    Id = di.ItemId,
                                    Calories = di.Item.Calories,
                                    Name = di.Item.Name,
                                    Protein = di.Item.Protein,
                                    TypeId = di.Item.TypeId
                                };
                                if (di.Item.CreatedByUserId != null)
                                {
                                    item.CreatedByUserId = (Guid)di.Item.CreatedByUserId;
                                }
                                day.Items.Add(item);
                            }
                            days.Add(day);

                        }
                    }
                });
                return days;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<Day> LoadById(Guid id)
        {
            try
            {
                Day day = new Day();

                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        var row = dc.TblDays.FirstOrDefault(d => d.Id == id);

                        if (row != null)
                        {
                            day.Id = row.Id;
                            day.UserId = row.UserId;
                            day.Date = row.Date;
                            day.Succeeded = row.Succeeded;

                            day.Activities = new List<Activity>();
                            foreach (TblDayActivity da in row.TblDayActivities.ToList())
                            {
                                Activity activity = new Activity
                                {
                                    Id = da.ActivityId,
                                    Name = da.Activity.Name,
                                    EasyCaloriesPerHour = da.Activity.EasyCaloriesPerHour,
                                    MediumCaloriesPerHour = da.Activity.MediumCaloriesPerHour,
                                    HardCaloriesPerHour = da.Activity.HardCaloriesPerHour
                                };
                                day.Activities.Add(activity);
                            }
                            day.Items = new List<Item>();
                            foreach (TblDayItem di in row.TblDayItems.ToList())
                            {
                                Item item = new Item
                                {
                                    Id = di.ItemId,
                                    Calories = di.Item.Calories,
                                    Name = di.Item.Name,
                                    Protein = di.Item.Protein,
                                    TypeId = di.Item.TypeId
                                };
                                if (di.Item.CreatedByUserId != null)
                                {
                                    item.CreatedByUserId = (Guid)di.Item.CreatedByUserId;
                                }
                                day.Items.Add(item);
                            }
                        }
                        else
                        {
                            throw new Exception("Row could not be found");
                        }
                    }
                });

                return day;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<Day> Load(Guid userId, DateTime date)
        {
            try
            {
                Day day = new Day();

                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        var row = dc.TblDays.FirstOrDefault(d => d.UserId == userId && d.Date == date);

                        if (row != null)
                        {
                            day.Id = row.Id;
                            day.UserId = row.UserId;
                            day.Date = row.Date;
                            day.Succeeded = row.Succeeded;

                            day.Activities = new List<Activity>();
                            foreach (TblDayActivity da in row.TblDayActivities.ToList())
                            {
                                Activity activity = new Activity
                                {
                                    Id = da.ActivityId,
                                    Name = da.Activity.Name,
                                    EasyCaloriesPerHour = da.Activity.EasyCaloriesPerHour,
                                    MediumCaloriesPerHour = da.Activity.MediumCaloriesPerHour,
                                    HardCaloriesPerHour = da.Activity.HardCaloriesPerHour
                                };
                                day.Activities.Add(activity);
                            }
                            day.Items = new List<Item>();
                            foreach (TblDayItem di in row.TblDayItems.ToList())
                            {
                                Item item = new Item
                                {
                                    Id = di.ItemId,
                                    Calories = di.Item.Calories,
                                    Name = di.Item.Name,
                                    Protein = di.Item.Protein,
                                    TypeId = di.Item.TypeId
                                };
                                if (di.Item.CreatedByUserId != null)
                                {
                                    item.CreatedByUserId = (Guid)di.Item.CreatedByUserId;
                                }
                                day.Items.Add(item);
                            }
                        }
                        else
                        {
                            throw new Exception("Row could not be found");
                        }
                    }
                });

                return day;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<bool> Insert(Day day, bool rollback = false)
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

                        TblDay newrow = new TblDay();
                        newrow.Id = Guid.NewGuid();
                        newrow.UserId = day.UserId;
                        newrow.Date = day.Date;
                        newrow.Succeeded = day.Succeeded;
                        day.Id = newrow.Id;

                        dc.TblDays.Add(newrow);
                        results = dc.SaveChanges();
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
                int results = 0;
                IDbContextTransaction transaction = null;
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        TblDay row = dc.TblDays.FirstOrDefault(c => c.Id == id);
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.TblDays.Remove(row);
                            results = dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }

                });
                //throw new Exception("Danger, Will Robinson!");
                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async static Task<int> Update(Day day, bool rollback = false)
        {
            try
            {
                int results = 0;
                IDbContextTransaction transaction = null;
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        TblDay row = dc.TblDays.FirstOrDefault(c => c.Id == day.Id);
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.Succeeded = day.Succeeded;
                            row.UserId = day.UserId;
                            row.Date = day.Date;

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
                //throw new Exception("Danger, Will Robinson!");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<List<Day>> LoadReport(Guid userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var days = new List<Day>();
                await Task.Run(() =>
                {
                    using (var dc = new FitAppEntities())
                    {
                        var paramUserId = new SqlParameter()
                        {
                            ParameterName = "UserId",
                            SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                            Value = userId
                        };

                        var paramStartDate = new SqlParameter()
                        {
                            ParameterName = "StartDate",
                            SqlDbType = System.Data.SqlDbType.Date,
                            Value = startDate
                        };

                        var paramEndDate = new SqlParameter()
                        {
                            ParameterName = "EndDate",
                            SqlDbType = System.Data.SqlDbType.Date,
                            Value = endDate
                        };

                        var results = dc.Set<spGenerateReport>().FromSqlRaw("exec spGenerateReport @UserId, @StartDate, @EndDate", paramUserId, paramStartDate, paramEndDate).ToList();

                        results.ForEach(r => days.Add(new Day
                        {
                            Id = r.Id,
                            UserId = r.UserId,
                            Date = r.Date,
                            CaloriesConsumed = r.CaloriesConsumed,
                            CaloriesBurned = r.CaloriesBurned,
                            ProteinConsumed = r.ProteinConsumed,
                            Succeeded = r.Succeeded
                        }));
                    }

                });
                return days;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

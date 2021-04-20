﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCT.FitApp.PL;
using TCT.FitApp.BL.Models;
using Microsoft.EntityFrameworkCore.Storage;

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
                                if (di.Item.UserId != null)
                                {
                                    item.CreatedUserId = (Guid)di.Item.UserId;
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

        public async static Task<List<Day>> LoadById(Guid dayId)
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
                            if (d.Id == dayId)
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
                                    if (di.Item.UserId != null)
                                    {
                                        item.CreatedUserId = (Guid)di.Item.UserId;
                                    }
                                    day.Items.Add(item);
                                }
                            }
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

        public async static Task<bool> Insert(Day day, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                await Task.Run(() =>
                {
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
            catch (Exception ex)
            {

                throw ex;
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
                        TblDay row = dc.TblDays.FirstOrDefault(c => c.Id == id);
                        int results = 0;
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.TblDays.Remove(row);
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
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async static Task<int> Update(Day day, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        TblDay row = dc.TblDays.FirstOrDefault(c => c.Id == day.Id);
                        int results = 0;
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.Succeeded = day.Succeeded;
                            row.UserId = day.UserId;
                            row.Date = day.Date;

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
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
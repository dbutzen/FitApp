﻿using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCT.FitApp.BL.Models;
using TCT.FitApp.PL;

namespace TCT.FitApp.BL
{
    public static class ActivityManager
    {
        public async static Task<List<Activity>> Load()
        {
            try
            {
                List<Activity> activities = new List<Activity>();
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        foreach (TblActivity it in dc.TblActivities.ToList())
                        {
                            Activity activity = new Activity { Id = it.Id, Name = it.Name, EasyCaloriesPerHour = it.EasyCaloriesPerHour, MediumCaloriesPerHour = it.MediumCaloriesPerHour, HardCaloriesPerHour = it.HardCaloriesPerHour };
                            activities.Add(activity);

                        }
                    }
                });
                return activities;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Activity> LoadById(Guid activityId)
        {
            try
            {
                Activity activity = new Activity();
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        var row = dc.TblActivities.FirstOrDefault(a => a.Id == activityId);

                        if (row != null)
                        {
                            activity.Id = row.Id;
                            activity.Name = row.Name;
                            activity.EasyCaloriesPerHour = row.EasyCaloriesPerHour;
                            activity.MediumCaloriesPerHour = row.MediumCaloriesPerHour;
                            activity.HardCaloriesPerHour = row.HardCaloriesPerHour;
                        }
                        else
                            throw new Exception("Row could not be found");
                        
                    }
                });
                return activity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<bool> Insert(Activity activity, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        TblActivity newrow = new TblActivity();
                        newrow.Id = Guid.NewGuid();
                        newrow.Name = activity.Name;
                        newrow.EasyCaloriesPerHour = activity.EasyCaloriesPerHour;
                        newrow.MediumCaloriesPerHour = activity.MediumCaloriesPerHour;
                        newrow.HardCaloriesPerHour = activity.HardCaloriesPerHour;

                        activity.Id = newrow.Id;

                        dc.TblActivities.Add(newrow);
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
                int results = 0;
                IDbContextTransaction transaction = null;
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        TblActivity row = dc.TblActivities.FirstOrDefault(c => c.Id == id);
                        
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.TblActivities.Remove(row);
                            results = dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                            
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }

                }); return results;
                throw new Exception("Danger, Will Robinson!");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async static Task<int> Update(Activity activity, bool rollback = false)
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
                        TblActivity row = dc.TblActivities.FirstOrDefault(c => c.Id == activity.Id);
                       
                        if (row != null)
                        {
                            

                            row.Name = activity.Name;
                            row.EasyCaloriesPerHour = activity.EasyCaloriesPerHour;
                            row.MediumCaloriesPerHour = activity.MediumCaloriesPerHour;
                            row.HardCaloriesPerHour = activity.HardCaloriesPerHour;

                            results = dc.SaveChanges();

                            if (rollback) transaction.Rollback();
                            
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                }); return results;
                throw new Exception("Danger, Will Robinson!");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

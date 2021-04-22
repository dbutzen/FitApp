using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TCT.FitApp.BL.Models;
using TCT.FitApp.PL;

namespace TCT.FitApp.BL
{
    public static class UserAccessLevelManager
    {
        public async static Task<int> Insert(UserAccessLevel userAccessLevel, bool rollback = false)
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

                        var row = new TblUserAccessLevel();

                        row.Id = Guid.NewGuid();
                        row.Name = userAccessLevel.Name;
                        row.Description = userAccessLevel.Description;

                        userAccessLevel.Id = row.Id;

                        dc.TblUserAccessLevels.Add(row);

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

        public async static Task<int> Update(UserAccessLevel userAccessLevel, bool rollback = false)
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

                        var row = dc.TblUserAccessLevels.FirstOrDefault(ual => ual.Id == userAccessLevel.Id);

                        row.Name = userAccessLevel.Name;
                        row.Description = userAccessLevel.Description;

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

                    using (var dc = new FitAppEntities())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        var row = dc.TblUserAccessLevels.FirstOrDefault(ual => ual.Id == id);
                        dc.TblUserAccessLevels.Remove(row);
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

        public async static Task<List<UserAccessLevel>> Load()
        {
            try
            {
                List<UserAccessLevel> userAccessLevels = new List<UserAccessLevel>();

                await Task.Run(() =>
                {
                    using (var dc = new FitAppEntities())
                    {
                        dc.TblUserAccessLevels
                            .ToList()
                            .ForEach(ual => userAccessLevels.Add(new UserAccessLevel
                            {
                                Id = ual.Id,
                                Name = ual.Name,
                                Description = ual.Description,
                            }));
                    }
                });

                return userAccessLevels;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<UserAccessLevel> LoadByName(String name)
        {
            try
            {
                UserAccessLevel userAccessLevel = new UserAccessLevel();
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        TblUserAccessLevel tblUserAccessLevel = dc.TblUserAccessLevels.FirstOrDefault(ual => ual.Name == name);

                        if (tblUserAccessLevel != null)
                        {
                            userAccessLevel.Id = tblUserAccessLevel.Id;
                            userAccessLevel.Name = tblUserAccessLevel.Name;
                            userAccessLevel.Description = tblUserAccessLevel.Description;
                        }
                        else
                        {
                            throw new Exception("Could not find the row.");
                        }
                    }
                });
                return userAccessLevel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<UserAccessLevel> LoadById(Guid id)
        {
            try
            {
                UserAccessLevel userAccessLevel = new UserAccessLevel();
                await Task.Run(() =>
                {
                    using (FitAppEntities dc = new FitAppEntities())
                    {
                        TblUserAccessLevel tblUserAccessLevel = dc.TblUserAccessLevels.FirstOrDefault(ual => ual.Id == id);

                        if (tblUserAccessLevel != null)
                        {
                            userAccessLevel.Id = tblUserAccessLevel.Id;
                            userAccessLevel.Name = tblUserAccessLevel.Name;
                            userAccessLevel.Description = tblUserAccessLevel.Description;
                        }
                        else
                        {
                            throw new Exception("Could not find the row.");
                        }
                    }
                });
                return userAccessLevel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

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
    public static class UserManager
    {

        public async static Task<int> Insert(User user, bool rollback = false)
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

                        var row = new TblUser();

                        row.Id = Guid.NewGuid();
                        row.Name = user.Name;
                        row.Username = user.Username;
                        row.UniqueKey = Guid.NewGuid();
                        row.Password = ComputeSha256Hash($"{user.Password}{row.UniqueKey.ToString().ToUpper()}");
                        row.CalorieGoal = user.CalorieGoal;
                        row.ProteinGoal = user.ProteinGoal;
                        row.DaysInARowSucceeded = user.DaysInARowSucceeded;
                        row.HeightInches = user.HeightInches;
                        row.WeightPounds = user.WeightPounds;
                        if (user.UserAccessLevelId != Guid.Empty)
                            row.UserAccessLevelId = user.UserAccessLevelId;
                        else
                            row.UserAccessLevelId = dc.TblUserAccessLevels.FirstOrDefault(u => u.Name == "User").Id;
                        row.Sex = user.Sex;

                        user.Id = row.Id;

                        dc.TblUsers.Add(row);

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

        public async static Task<int> Update(User user, bool rollback = false)
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

                        var row = dc.TblUsers.FirstOrDefault(u => u.Id == user.Id);

                        row.Name = user.Name;
                        row.Username = user.Username;
                        row.SessionKey = user.SessionKey;
                        row.CalorieGoal = user.CalorieGoal;
                        row.ProteinGoal = user.ProteinGoal;
                        row.DaysInARowSucceeded = user.DaysInARowSucceeded;
                        row.HeightInches = user.HeightInches;
                        row.WeightPounds = user.WeightPounds;
                        row.UserAccessLevelId = user.UserAccessLevelId;
                        row.Sex = user.Sex;

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

                        var row = dc.TblUsers.FirstOrDefault(u => u.Id == id);
                        dc.TblUsers.Remove(row);
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

        public async static Task<List<User>> Load()
        {
            try
            {
                var users = new List<User>();
                await Task.Run(() =>
                {
                    using (var dc = new FitAppEntities())
                    {
                        dc.TblUsers
                        .ToList()
                        .ForEach(u =>
                        {
                            var user = new User();
                            Fill(user, u);
                            users.Add(user);
                        });
                    }
                });

                return users;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<User> LoadById(Guid id)
        {
            try
            {
                var user = new User();
                await Task.Run(() =>
                {
                    using (var dc = new FitAppEntities())
                    {
                        var row = dc.TblUsers.FirstOrDefault(u => u.Id == id);
                        if (row != null)
                        {
                            Fill(user, row);
                        }
                        else
                        {
                            throw new Exception("User could not be found");
                        }

                    }
                });

                return user;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<User> LoadBySessionKey(Guid sessionKey)
        {
            try
            {
                var user = new User();
                await Task.Run(() =>
                {
                    using (var dc = new FitAppEntities())
                    {
                        var row = dc.TblUsers.FirstOrDefault(u => u.SessionKey == sessionKey);
                        if (row != null)
                        {
                            Fill(user, row);
                        }
                        else
                        {
                            throw new Exception("User could not be found");
                        }

                    }
                });

                return user;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<User> LoadByUsername(string username)
        {
            try
            {
                var user = new User();
                await Task.Run(() =>
                {
                    using (var dc = new FitAppEntities())
                    {
                        var row = dc.TblUsers.FirstOrDefault(u => u.Username == username);
                        if (row != null)
                        {
                            Fill(user, row);
                        }
                        else
                        {
                            throw new Exception("User could not be found");
                        }
                    }
                });

                return user;

            }
            catch (Exception)
            {
                throw;
            }
        }

        // Returns the user's secured key
        public static async Task<Guid> Login(User user, bool logoutOtherDevices = false, bool rollback = false)
        {
            try
            {
                var results = Guid.Empty;
                await Task.Run(() =>
                {
                    using (var dc = new FitAppEntities())
                    {
                        var row = dc.TblUsers.FirstOrDefault(u => u.Username == user.Username);
                        if (row != null)
                        {
                            var hashed_password = ComputeSha256Hash($"{user.Password}{row.UniqueKey.ToString().ToUpper()}");

                            if (hashed_password == row.Password)
                            {
                                Fill(user, row);
                                user.SessionKey = row.SessionKey;
                                if (logoutOtherDevices || user.SessionKey == null)
                                {
                                    user.SessionKey = Guid.NewGuid();
                                    var task = Update(user, rollback);
                                    task.Wait();
                                }
                                results = (Guid)user.SessionKey;
                            }
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

        public static async Task<int> ChangePassword(User user, string newPassword, bool rollback = false)
        {
            try
            {
                var results = 0;
                await Task.Run(() =>
                {
                    using (var dc = new FitAppEntities())
                    {
                        IDbContextTransaction transaction = null;

                        if (rollback) transaction = dc.Database.BeginTransaction();


                        var row = dc.TblUsers.FirstOrDefault(u => u.Id == user.Id);
                        if (row != null)
                        {
                            var hashed_password = ComputeSha256Hash($"{user.Password}{row.UniqueKey.ToString().ToUpper()}");

                            if (hashed_password == row.Password)
                            {
                                if (!string.IsNullOrEmpty(newPassword.Trim()))
                                {
                                    row.UniqueKey = Guid.NewGuid();
                                    row.Password = ComputeSha256Hash($"{newPassword}{row.UniqueKey.ToString().ToUpper()}");


                                    results = dc.SaveChanges();

                                    if (rollback) transaction.Rollback();
                                }
                                else
                                {
                                    throw new Exception("New password can't be blank.");
                                }
                            }
                            else
                            {
                                throw new Exception("Old password is incorrect.");
                            }
                        }
                        else
                        {
                            throw new Exception("User could not be found.");
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

        private static void Fill(User user, TblUser row)
        {
            user.Id = row.Id;
            user.Name = row.Name;
            user.Username = row.Username;
            user.CalorieGoal = row.CalorieGoal;
            user.ProteinGoal = row.ProteinGoal;
            user.DaysInARowSucceeded = row.DaysInARowSucceeded;
            user.HeightInches = row.HeightInches;
            user.WeightPounds = row.WeightPounds;
            user.UserAccessLevelId = row.UserAccessLevelId;
            user.Sex = row.Sex;
            user.UserAccessLevelName = row.UserAccessLevel.Name;
        }



        // Use for hashing the plain password + uniquekey
        private static string ComputeSha256Hash(string rawData)
        {
            {
                using (var sha = SHA256.Create())
                {
                    var data = sha.ComputeHash(Encoding.Unicode.GetBytes(rawData));

                    var builder = new StringBuilder();

                    foreach (var d in data)
                    {
                        builder.Append(d.ToString("X2"));
                    }
                    return builder.ToString();
                }
            }
        }
    }
}

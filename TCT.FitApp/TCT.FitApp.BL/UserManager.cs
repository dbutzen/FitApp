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

                    using (var dc = new FitAppDataContext())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        var row = new TblUser();

                        row.Id = Guid.NewGuid();
                        row.Name = user.Name;
                        row.Username = user.Name;
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
                        user.UniqueKey = row.UniqueKey;

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

                    using (var dc = new FitAppDataContext())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        var row = dc.TblUsers.FirstOrDefault(u => u.Id == user.Id);

                        row.Name = user.Name;
                        row.Username = user.Username;
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

                    using (var dc = new FitAppDataContext())
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
                    using (var dc = new FitAppDataContext())
                    {
                        dc.TblUsers
                        .ToList()
                        .ForEach(u =>
                        {
                            var user = new User();
                            GetFromTableRow(user, u);
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
                    using (var dc = new FitAppDataContext())
                    {
                        var row = dc.TblUsers.FirstOrDefault(u => u.Id == id);
                        if (row != null)
                        {
                            GetFromTableRow(user, row);
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
                    using (var dc = new FitAppDataContext())
                    {
                        var row = dc.TblUsers.FirstOrDefault(u => u.Username == username);
                        if (row != null)
                        {
                            GetFromTableRow(user, row);
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

        // Returns true if login is successful
        public static async Task<bool> Login(User user)
        {
            try
            {
                var results = false;
                await Task.Run(() =>
                {
                    using (var dc = new FitAppDataContext())
                    {
                        var row = dc.TblUsers.FirstOrDefault(u => u.Username == user.Username);
                        if (row != null)
                        {
                            var hashed_password = ComputeSha256Hash($"{user.Password}{row.UniqueKey.ToString().ToUpper()}");

                            if (hashed_password == row.Password)
                            {
                                GetFromTableRow(user, row);
                                results = true;
                            }
                            else
                            {
                                throw new Exception("The username/email or password is incorrect.");
                            }
                        }
                        else
                        {
                            throw new Exception("The username/email or password is incorrect.");
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

        private static void GetFromTableRow(User user, TblUser row)
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

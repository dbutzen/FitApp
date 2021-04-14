using System;
using System.Security.Cryptography;
using System.Text;

namespace TCT.FitApp.BL
{
    public static class UserManager
    {


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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MoneyRecords.ViewModels
{
    internal class Password
    {
        public static string Hash(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}

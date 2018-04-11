using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AntreDeuxVins.Utils
{
    public static class Extensions
    {
        public static string Encrypt(this string value)
        {
            SHA256 sha256hash = SHA256.Create();
            byte[] data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                builder.Append(data[i].ToString("x2"));

            return builder.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Core.Problem14
{
    public static class Md5Hash
    {
        public static byte[] Compute(string data)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var bytes = System.Text.Encoding.Default.GetBytes(data);

                return md5.ComputeHash(bytes);
            }
        }

        public static T Compute<T>(string data, Func<byte[], T> output)
        {
            var bytes = Md5Hash.Compute(data);

            return output(bytes);
        }
    }
}

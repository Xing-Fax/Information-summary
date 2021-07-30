using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Information_summary.Digest_algorithm
{
    class SHA512
    {
        public static string Hash_SHA_512(string file)
        {
            FileStream stream = new FileStream(file, FileMode.Open);
            SHA512Managed Sha512 = new SHA512Managed();
            byte[] by = Sha512.ComputeHash(stream);
            return BitConverter.ToString(by).Replace("-", "").ToUpper(); 
        }
    }
}

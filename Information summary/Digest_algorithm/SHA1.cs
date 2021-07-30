using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Information_summary.Digest_algorithm
{
    class SHA1
    {
        public static string Hash_SHA_1(string file)
        {
            FileStream stream = new FileStream(file, FileMode.Open);
            SHA1Managed Sha1 = new SHA1Managed();
            byte[] by = Sha1.ComputeHash(stream);
            return BitConverter.ToString(by).Replace("-", "").ToUpper();
        }
    }
}

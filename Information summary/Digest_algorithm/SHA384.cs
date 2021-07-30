using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Information_summary.Digest_algorithm
{
    class SHA384
    {
        public static string Hash_SHA_384(string file)
        {
            FileStream stream = new FileStream(file, FileMode.Open);
            SHA384Managed Sha384 = new SHA384Managed();
            byte[] by = Sha384.ComputeHash(stream);
            return BitConverter.ToString(by).Replace("-", "").ToUpper(); 
        }
    }
}

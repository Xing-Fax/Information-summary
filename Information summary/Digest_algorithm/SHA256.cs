using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace Information_summary.Digest_algorithm
{
    class SHA256
    {
        public static string Hash_SHA_256(string file)
        {
            FileStream stream = new FileStream(file, FileMode.Open);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] by = Sha256.ComputeHash(stream);
            return BitConverter.ToString(by).Replace("-", "").ToUpper(); 
        }
    }
    
}

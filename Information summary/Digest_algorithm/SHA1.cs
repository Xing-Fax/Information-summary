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
        /// <summary>
        /// 计算文件的SHA1值
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>返回全部大写字符串</returns>
        public static string Hash_SHA_1(string file)
        {
            FileStream stream = new FileStream(file, FileMode.Open);
            SHA1Managed Sha1 = new SHA1Managed();
            byte[] by = Sha1.ComputeHash(stream);
            stream.Close();
            return BitConverter.ToString(by).Replace("-", "").ToUpper();
        }
    }
}

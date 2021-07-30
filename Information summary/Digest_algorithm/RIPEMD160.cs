using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Information_summary.Digest_algorithm
{
    class RIPEMD160
    {
        /// <summary>
        /// 计算文件的SHA1值
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>返回全部大写字符串</returns>
        public static string Hash_HMACRIPEMD160(string file)
        {
            FileStream stream = new FileStream(file, FileMode.Open);
            RIPEMD160Managed Ripemd160 = new RIPEMD160Managed();
            byte[] by = Ripemd160.ComputeHash(stream);
            stream.Close();
            return BitConverter.ToString(by).Replace("-", "").ToUpper();
        }
    }
}

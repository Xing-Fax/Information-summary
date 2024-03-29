﻿using System;
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
        /// <summary>
        /// 计算文件的SHA512值
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>返回全部大写字符串</returns>
        public static string Hash_SHA_512(string file)
        {
            FileStream stream = new FileStream(file, FileMode.Open);
            SHA512Managed Sha512 = new SHA512Managed();
            byte[] by = Sha512.ComputeHash(stream);
            stream.Close();
            return BitConverter.ToString(by).Replace("-", "").ToUpper(); 
        }
    }
}

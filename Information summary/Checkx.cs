using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Information_summary
{
    class Checkx
    {
        public static bool Document_verification(string file)
        {
            try
            {
                X509Certificate cert = X509Certificate.CreateFromSignedFile(file);
                string f = cert.GetCertHashString();
                if (f == "36A888B9F2A505BF92AC6B2796C2188E639AB1D1")
                { return true; }
                else{return false;}
            }
            catch { return false; }
        }
    }
}

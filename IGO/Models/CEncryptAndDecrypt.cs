using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IGO.Models
{
    public static class CEncryptAndDecrypt
    {
        public static string Encrypt(string input)

    {
            string a = Convert.ToBase64String(new UTF8Encoding().GetBytes(input));
            string b = "0213SDF" + a + "ASD854";
      return b;

    }

        public static string Decrypt(string input)
    {
            string a = input.Substring(7);
            string b = a.Remove(a.Length - 6, 6);
            try {
                return new UTF8Encoding().GetString(Convert.FromBase64String(b));
            }
            catch (Exception) {
                return "0";

            }
      

    }


}
}

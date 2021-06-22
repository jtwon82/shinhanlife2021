using System;
using System.Security.Cryptography;
using System.Text;

namespace MLib.Cipher
{
    public static class MD5
    {
        #region [MD5 암호화]
        /// <summary>
        /// MD5 암호화
        /// </summary>
        /// <param name="data">원본 문자열</param>
        /// <returns>string : MD5암호화 된 문자열</returns>
        public static string Encrypt(string data)
        {
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(data));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
        #endregion
    }
}

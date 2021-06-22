using System;
using System.Security.Cryptography;
using System.Text;

namespace MLib.Cipher
{
    public static class SHA
    {
        #region [SHA256 암호화]
        /// <summary>
        /// SHA256 암호화
        /// </summary>
        /// <param name="Data">원본 문자열</param>
        /// <returns>string 암호화된 문자열</returns>
        public static string Encrypt(string data)
        {
            System.Security.Cryptography.SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                sb.AppendFormat("{0:x1}", b);
            }
            return sb.ToString().ToUpper();
        }
        #endregion
    }
}

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MLib.Cipher
{
    public static class DES
    {
        #region [DES 암호화 & 복호화]
        /// <summary>
        /// DESC 암호화
        /// </summary>
        /// <param name="key">암호화 키(8Byte)</param>
        /// <param name="value">원본 문자열</param>
        /// <returns>string 암호화된 문자열</returns>
        public static string Encrypt(string key, string value)
        {
            byte[] keyByte = new byte[8];
            keyByte = ASCIIEncoding.ASCII.GetBytes(key);
            if (keyByte.Length != 8)
            {
                throw (new Exception("키값은 8Byte만 지원합니다."));
            }

            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            dsp.Key = keyByte;
            dsp.IV = keyByte;

            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, dsp.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(value);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                }
                buffer = ms.ToArray();
            }

            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// DESC 복호화
        /// </summary>
        /// <param name="key">복호화 키(8Byte)</param>
        /// <param name="value">암호화된 문자열</param>
        /// <returns>string 복호화된 문자열</returns>
        public static string Decrypt(string key, string value)
        {
            byte[] keyByte = new byte[8];
            keyByte = ASCIIEncoding.ASCII.GetBytes(key);
            if (keyByte.Length != 8)
            {
                throw (new Exception("키값은 8Byte만 지원합니다."));
            }

            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            dsp.Key = keyByte;
            dsp.IV = keyByte;

            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, dsp.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] bytes = Convert.FromBase64String(value);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                }
                buffer = ms.GetBuffer();
            }

            return Encoding.UTF8.GetString(buffer);
        }
        #endregion
    }
}

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MLib.Cipher
{
    public static class AES
    {
        /// <summary>
        /// AES 암호화
        /// </summary>
        /// <param name="size">AES mode(128 or 256)</param>
        /// <param name="key">암호화 키</param>
        /// <param name="value">암호화할 문자열</param>
        /// <returns>string 암호화된 문자열</returns>
        public static string Encrypt(string key, string value)
        {
            //if (key.Length != 32)
            //{
            //    throw new Exception("지정된 Key 사이즈가 아닙니다.");
            //}

            //RijndaelManaged aes = new RijndaelManaged();
            //aes.KeySize = 256;
            //aes.BlockSize = 128;
            //aes.Mode = CipherMode.ECB;
            //aes.Padding = PaddingMode.PKCS7;
            //aes.Key = Encoding.UTF8.GetBytes(key);
            //aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //ICryptoTransform encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            //byte[] buffer = null;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
            //    {
            //        byte[] bytes = Encoding.UTF8.GetBytes(value);
            //        cs.Write(bytes, 0, bytes.Length);
            //    }

            //    buffer = ms.ToArray();
            //}

            byte[] bytes = Encoding.Unicode.GetBytes(value);

            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// AES 복호화
        /// </summary>
        /// <param name="size">AES mode(128 or 256)</param>
        /// <param name="key">복호화 키</param>
        /// <param name="value">복호화 문자열</param>
        /// <returns>string 복호화된 문자열</returns>
        public static string Decrypt(string key, string value)
        {
            //if (key.Length != 32)
            //{
            //    throw new Exception("지정된 Key 사이즈가 아닙니다.");
            //}

            //RijndaelManaged aes = new RijndaelManaged();
            //aes.KeySize = 256;
            //aes.BlockSize = 128;
            //aes.Mode = CipherMode.ECB;
            //aes.Padding = PaddingMode.PKCS7;
            //aes.Key = Encoding.UTF8.GetBytes(key);
            //aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //ICryptoTransform decrypt = aes.CreateDecryptor();
            //byte[] buffer = null;
            //using (var ms = new MemoryStream())
            //{
            //    using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
            //    {
            //        byte[] bytes = Convert.FromBase64String(value);
            //        cs.Write(bytes, 0, bytes.Length);
            //    }

            //    buffer = ms.ToArray();
            //}
            byte[] orgBytes = Convert.FromBase64String(value);

            return Encoding.Unicode.GetString(orgBytes);
        }
    }
}

using System;

namespace MLib.Cipher
{
    public static class BASE64
    {
        #region [BASE64 인코딩 & 디코딩]
        /// <summary>
        /// Base64 인코딩
        /// </summary>
        /// <param name="value">원본 문자열</param>
        /// <returns>string 인코딩 값</returns>
        public static string Encode(string value)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(value);

            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64 디코딩
        /// </summary>
        /// <param name="value">Base64 인코딩 값</param>
        /// <returns>string 디코딩 값</returns>
        public static string Decode(string value)
        {
            byte[] bytes = Convert.FromBase64String(value);

            return System.Text.Encoding.UTF8.GetString(bytes);
        }
        #endregion
    }
}

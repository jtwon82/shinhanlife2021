using MLib.Cipher;
using MLib.Util;
using System;
using System.Web;
using System.Web.Security;

namespace MLib.Auth
{
    public static class Web
    {
        #region [세션 셋팅 & 획득]
        /// <summary>
        /// 세션 가져오기
        /// </summary>
        /// <param name="name">세션 명</param>
        /// <returns>string 세션 값</returns>
        public static string Session(string name)
        {
            string rtn = string.Empty;
            if (HttpContext.Current.Session[name] == null)
            {
                rtn = "";
            }
            else
            {
                rtn = HttpContext.Current.Session[name].ToString();
            }
            return rtn;
        }

        /// <summary>
        /// 세션 셋팅
        /// </summary>
        /// <param name="name">세션 명</param>
        /// <param name="value">세션 셋팅값</param>
        public static void Session(string name, string value)
        {
            HttpContext.Current.Session[name] = value;
        }
        #endregion

        #region [쿠키 셋팅 & 획득]
        /// <summary>
        /// 쿠키 셋팅
        /// </summary>
        /// <param name="group">쿠키그룹</param>
        /// <param name="key">아이디</param>
        /// <param name="value">값</param>
        /// <param name="expires">만료일</param>
        public static void Cookies(string group, string key, string value, int expires)
        {
            HttpCookie cookie = new HttpCookie(group);
            cookie[key] = value;
            cookie.Path = "/";
            cookie.Expires = DateTime.Now.AddDays(expires);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 쿠키 획득
        /// </summary>
        /// <param name="group">그룹</param>
        /// <param name="key">아이디</param>
        /// <returns>string 셋팅된 쿠키 값</returns>
        public static string Cookies(string group, string key)
        {
            string rtn = string.Empty;
            if (HttpContext.Current.Request.Cookies[group] != null)
            {
                if (HttpContext.Current.Request.Cookies[group][key] != null)
                {
                    rtn = HttpContext.Current.Request.Cookies[group][key];
                }
            }
            return rtn;
        }
        #endregion
    }
}

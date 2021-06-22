using MLib.Cipher;
using MLib.Util;
using System;
using System.Web;
using System.Web.Security;

namespace MLib.Auth
{
    public class Forms
    {
        /// <summary>
        /// ASP.NET Form인증
        /// </summary>
        /// <param name="key">암호화 키</param>
        /// <param name="id">인증 아이디</param>
        /// <param name="data">사용자 데이터</param>
        public static bool Authorize(string key, string id, string[] data)
        {
            bool rtn = false;
            try
            {
                string info = AES.Encrypt(key, string.Join("$", data));
                HttpContext hc = HttpContext.Current;

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    id,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(FormsAuthentication.Timeout.Minutes),
                    false,
                    info,
                    FormsAuthentication.FormsCookiePath
                );

                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                if (ticket.IsPersistent)
                    cookie.Expires = ticket.Expiration;

                hc.Response.Cookies.Add(cookie);

                rtn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rtn;
        }

        /// <summary>
        /// 로그인 판단
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                if (HttpContext.Current.User != null)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        if (HttpContext.Current.User.Identity is FormsIdentity)
                            return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 인증정보에서 구분로 구분된 정보 취득
        /// </summary>
        /// <param name="key">암호화된 쿠키값 복호화 키(AES암호화)</param>
        /// <param name="index">가져올 정보 index값</param>
        /// <returns>string 쿠키값 구분자로 구분된 정보를 가져옴</returns>
        public static string AuthInfo(string key, int index)
        {
            if (IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;

                string info = AES.Decrypt(key, ticket.UserData);

                return Tool.Separator(info, "$", index);
            }
            return "";
        }

        /// <summary>
        /// 인증정보에서 구분로 구분된 정보 취득
        /// </summary>
        /// <param name="key">암호화된 쿠키값 복호화 키(AES암호화)</param>
        /// <returns>string 쿠키값 구분자로 구분된 정보를 가져옴</returns>
        public static string AuthInfo(string key)
        {
            if (IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;

                string info = AES.Decrypt(key, ticket.UserData);

                return info;
            }
            return "";
        }

        /// <summary>
        /// 폼인증 해제
        /// </summary>
        public static void UnAuthorize()
        {
            FormsAuthentication.SignOut();
        }
    }
}

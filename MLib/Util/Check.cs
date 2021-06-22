using MLib.Config;
using System;
using System.Web;
using System.Web.Configuration;

namespace MLib.Util
{
    public static class Check
    {
        #region [ IsIn ]
        /// <summary>
        /// 문자열에 찾는 문자열 포함여부 판단
        /// </summary>
        /// <param name="text">원본 문자열</param>
        /// <param name="value">찾을 문자열</param>
        /// <returns>bool</returns>
        public static bool IsIn(string text, string value)
        {
            if (text.IndexOf(value, 0) >= 0)
                return true;
            else
                return false;
        }
        #endregion

        #region [ IsNone ]
        /// <summary>
        /// 문자열이 공백이나 null인지 판단
        /// </summary>
        /// <param name="value">데이터 원본</param>
        /// <returns>bool</returns>
        public static bool IsNone(string value)
        {
            if (string.IsNullOrEmpty(value) || value == "")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 원본데이터가 null이거나 빈값일때 대체할 String 리턴
        /// </summary>
        /// <param name="param">원본 데이터</param>
        /// <param name="value">대체 문자열</param>
        /// <returns>string 문자열</returns>
        public static string IsNone(string param, string value)
        {
            string rtn = string.Empty;
            if (Check.IsNone(param))
                rtn = value;
            else
                rtn = param;

            return rtn;
        }

        /// <summary>
        /// 원본데이터가 null이거나 빈값일때 대체할 Int 리턴
        /// </summary>
        /// <param name="param">원본 데이터</param>
        /// <param name="value">대체 숫자</param>
        /// <returns>Int 정수</returns>
        public static int IsNone(string param, int value)
        {
            int rtn;
            if (Check.IsNone(param))
                rtn = value;
            else
                rtn = Convert.ToInt32(param);

            return rtn;
        }

        /// <summary>
        /// 원본데이터가 null이거나 빈값일때 에러반환
        /// </summary>
        /// <param name="param">원본 데이터</param>
        /// <param name="required">필수 여부</param>
        /// <returns>string 문자열</returns>
        public static string IsNone(string param, bool required)
        {
            string rtn = string.Empty;
            if (Check.IsNone(param))
            {
                if (required)
                    throw new Exception("필수항목이 누락되었습니다.");
            }
            else
            {
                rtn = param;
            }
            return rtn;
        }

        /// <summary>
        /// 원본데이터가 null이거나 빈값일때 대체할 문자열 리턴
        /// </summary>
        /// <param name="param">원본 데이터</param>
        /// <param name="value">대체 문자열</param>
        /// <param name="required">필수 여부</param>
        /// <returns>string 문자열</returns>
        public static string IsNone(string param, string value, bool required)
        {
            string rtn = string.Empty;
            if (Check.IsNone(param))
            {
                if (required)
                    throw new Exception("필수항목이 누락되었습니다.");
                else
                    rtn = value;
            }
            else
            {
                rtn = param;
            }
            return rtn;
        }

        /// <summary>
        /// 원본데이터가 null이거나 빈값일때 대체할 숫자 리턴
        /// </summary>
        /// <param name="param">원본데이터</param>
        /// <param name="value">대체 숫자</param>
        /// <param name="required">필수 여부</param>
        /// <returns>int 숫자</returns>
        public static int IsNone(string param, int value, bool required)
        {
            int rtn = 0;
            if (Check.IsNone(param))
            {
                if (required)
                    throw new Exception("필수항목이 누락되었습니다.");
                else
                    rtn = value;
            }
            else
            {
                rtn = Convert.ToInt32(param);
            }
            return rtn;
        }
        #endregion

        #region [ 모바일 체크 ]
        /// <summary>
        /// 모바일 여부 판단
        /// </summary>
        /// <returns>bool : 모바일 여부</returns>
        public static bool IsMobile
        {
            get
            {
                HttpBrowserCapabilities myBrowserCaps = HttpContext.Current.Request.Browser;
                if (((HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// iOS 판단
        /// </summary>
        /// <returns>bool : iOS 여부</returns>
        public static bool IsIOS
        {
            get
            {
                string[] device = new string[] { "iphone", "ipod", "ipad" };
                string agent = ServerVariables.HttpUserAgent.ToLower();
                foreach (string item in device)
                {
                    if (Check.IsIn(agent, item))
                        return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Android 판단
        /// </summary>
        /// <returns>bool : Android 여부</returns>
        public static bool IsAndroid
        {
            get
            {
                string agent = ServerVariables.HttpUserAgent.ToLower();
                if (Check.IsIn(agent, "android"))
                    return true;
                else
                    return false;
            }
        }
        #endregion
    }
}
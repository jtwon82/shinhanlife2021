using MLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MLib.Config
{
    public static class Agent
    {
        #region [Web페이지 정보 취득]
        /// <summary>
        /// 현재페이지 url과 파라메터 반환
        /// </summary>
        /// <returns>string : 주소 +"?"+ 파라메터</returns>
        public static string Referer()
        {
            string param = string.Empty;
            int i = 0;
            foreach (string item in HttpContext.Current.Request.QueryString)
            {
                if (i != 0)
                {
                    param += "&";
                }
                param += item + "=" + HttpContext.Current.Request.QueryString[item];
                i++;
            }

            if (!Check.IsNone(param))
            {
                param = "?" + param;
            }
            return HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"] + param);
        }

        /// <summary>
        /// 폴더 경로 반환
        /// </summary>
        /// <param name="index">int: 가져올 루트 index(0:도메인 ~ 경로)</param>
        /// <returns>string : 폴더 또는 파일 이름</returns>
        public static string Directory(int index)
        {
            string rtn = string.Empty;
            string path = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];

            return Tool.Separator(path, "/", index);
        }

        /// <summary>
        /// GET 파라미터
        /// </summary>
        /// <returns>string : 파라미터 조합(&a=1&b=2)</returns>
        public static string Get()
        {
            string param = string.Empty;
            int i = 0;
            foreach (string item in HttpContext.Current.Request.QueryString)
            {
                if (i != 0)
                {
                    param += "&";
                }
                param += item + "=" + HttpContext.Current.Request.QueryString[item];
                i++;
            }

            return param;
        }

        /// <summary>
        /// POST 파라미터
        /// </summary>
        /// <returns>string : 파라미터 조합(&a=1&b=2)</returns>
        public static string Post()
        {
            string param = string.Empty;
            int i = 0;
            foreach (string item in HttpContext.Current.Request.Form)
            {
                if (i != 0)
                {
                    param += "&";
                }
                param += item + "=" + HttpContext.Current.Request.Form[item];
                i++;
            }

            return param;
        }

        /// <summary>
        /// 페이지 파라메터 조합
        /// </summary>
        /// <returns>string : 페이지 POST, GET파라메터 조합</returns>
        public static string Params()
        {
            string param = string.Empty;
            int i = 0;
            foreach (string item in HttpContext.Current.Request.QueryString)
            {
                if (i != 0)
                {
                    param += "&";
                }
                param += item + "=" + HttpContext.Current.Request.QueryString[item];
                i++;
            }

            i = 0;
            foreach (string item in HttpContext.Current.Request.Form)
            {
                if (i != 0 || !Check.IsNone(param))
                {
                    param += "&";
                }
                param += item + "=" + HttpContext.Current.Request.Form[item];
                i++;
            }
            return param;
        }
        #endregion
    }
}

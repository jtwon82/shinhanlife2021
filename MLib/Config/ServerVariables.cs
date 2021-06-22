using MLib.Util;
using System.Configuration;
using System.Web;

namespace MLib.Config
{
    public static class ServerVariables
    {

        /// <summary>
        /// upload fullpath
        /// </summary>
        public static string uploadFullPath{get{return ConfigurationManager.AppSettings["DIRECTORY_UPLOAD_PATH"]; }}

        #region [ ServerVariables 정보취득 ]
        /// <summary>
        /// 접속 아이피
        /// </summary>
        public static string RemoteAddr
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
        }

        /// <summary>
        /// 접속 도메인 ex) http[https]://test.com
        /// </summary>
        public static string Domain
        {
            get
            {
                string domain = Https ? "https://" : "http://";
                return string.Format("{0}{1}/", domain, HttpHost);
            }
        }

        /// <summary>
        /// 접속 호스트 ex) test.com
        /// </summary>
        public static string ServerName
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            }
        }

        /// <summary>
        /// 접속 포트 ex) 80
        /// </summary>
        public static string ServerPort
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            }
        }

        /// <summary>
        /// 접속 도메인 + 포트(80포트 무시) ex) test.com:80
        /// </summary>
        public static string HttpHost
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            }
        }

        /// <summary>
        /// 사용자 Agent정보
        /// </summary>
        public static string HttpUserAgent
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
            }
        }

        /// <summary>
        /// 홈 디렉토리
        /// EX) C:\webroot\test.com\
        /// </summary>
        public static string ApplPhysicalPath
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
            }
        }

        /// <summary>
        /// SSL인증서 여부
        /// </summary>
        public static bool Https
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["HTTPS"].Equals("on");
            }
        }

        /// <summary>
        /// URL
        /// EX) /api/user/auth/step1.aspx
        /// </summary>
        public static string Url
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["URL"];
            }
        }
        #endregion

        #region [서버 정보 ServerVariables 사용]
        /// <summary>
        /// 서버정보 표현
        /// </summary>
        public static void Info()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string item in HttpContext.Current.Request.ServerVariables)
            {
                sb.AppendFormat("Request.ServerVariables[\"{0}\"] = {1}<br />", item, HttpContext.Current.Request[item]);
            }
            Tool.Print(sb.ToString());
            Tool.End();
        }
        #endregion
    }
}

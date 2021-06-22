using System.Text;
using System.Web.UI;

namespace MLib.Util
{
    public static class JS
    {
        #region [ JAVASCRIPT 모음 ]
        /// <summary>
        /// 얼럿 호출 후 history.back();
        /// </summary>
        /// <param name="page">페이지 객체 this.Page</param>
        /// <param name="msg">경고창 메세지</param>
        public static void Back(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">");
            sb.Append("alert(\"" + msg + "\");");
            sb.Append("history.back();");
            sb.Append("</script>");

            Tool.End(sb.ToString());
        }

        /// <summary>
        /// 얼럿 호출 후 페이지 이동
        /// </summary>
        /// <param name="page">페이지 객체 this.Page</param>
        /// <param name="msg">경고창 메세지</param>
        /// <param name="url">이동할 URL</param>
        public static void Move(string msg, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">");
            sb.Append("alert(\"" + msg + "\");");
            sb.Append("location.href=\"" + url + "\";");
            sb.Append("</script>");

            Tool.End(sb.ToString());
        }

        /// <summary>
        /// 얼럿 호출 후 페이지 닫음
        /// </summary>
        /// <param name="page">페이지 객체 this.Page</param>
        /// <param name="msg">경고창 메세지</param>
        public static void Close(string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">");
            if (!Check.IsNone(msg))
            {
                sb.Append("alert(\"" + msg + "\");");
            }
            sb.Append("window.self.close();");
            sb.Append("</script>");

            Tool.End(sb.ToString());
        }

        /// <summary>
        /// 사용자 정의 스크립트
        /// </summary>
        /// <param name="page">페이지 객체 this.Page</param>
        /// <param name="script">스크립트 내용</param>
        public static void Script(string script)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">");
            sb.Append(script);
            sb.Append("</script>");

            Tool.End(sb.ToString());
        }
        #endregion
    }
}

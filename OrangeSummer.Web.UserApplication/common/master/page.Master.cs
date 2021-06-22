using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Auth;
using MLib.Config;
using MLib.Util;

namespace OrangeSummer.Web.UserApplication.common.master
{
    public partial class page : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            #region [ 로그인 체크 ]
            string url = ServerVariables.Url;
            bool checker = false;
            string[] directory = { "/default.aspx", "/member/regist/default.aspx", "/member/login/default.aspx", "/member/terms/default.aspx" };
            foreach (var item in directory)
            {
                if (url.Equals(item))
                {
                    checker = true;
                    break;
                }
            }

            if (!checker)
            {
                if (!Forms.IsAuthenticated)
                    Tool.RR("/member/login/?referer=" + Agent.Referer());
            }
            #endregion

            #region [ 로그인 상태에서 접근 막기 ]
            string[] exists = { "/member/regist/default.aspx", "/member/login/default.aspx" };
            foreach (var item in exists)
            {
                if (url.Equals(item))
                {
                    checker = true;
                    break;
                }
            }

            if (checker)
            {
                if (Forms.IsAuthenticated)
                    JS.Move("이미 로그인 상태입니다.\\n로그아웃 후 이용해주세요.", "/achieve/");
            }
            #endregion
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected string CssEdit()
        {
            if (Agent.Directory(3) == "detail.aspx")
                return "_edit";
            else
                return "";
        }
    }
}
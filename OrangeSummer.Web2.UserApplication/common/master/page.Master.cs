using MLib.Auth;
using MLib.Config;
using MLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web2.UserApplication.common.master
{
    public partial class page : System.Web.UI.MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

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
            else
            {
                // 아래 코드는 XSRF 공격으로부터 보호받는 데 도움이 됩니다.
                var requestCookie = Request.Cookies[AntiXsrfTokenKey];
                Guid requestCookieGuidValue;
                if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
                {
                    // 쿠키의 Anti-XSRF 토큰 사용
                    _antiXsrfTokenValue = requestCookie.Value;
                    Page.ViewStateUserKey = _antiXsrfTokenValue;
                }
                else
                {
                    // 새 Anti-XSRF 토큰을 생성하여 쿠키에 저장
                    _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                    Page.ViewStateUserKey = _antiXsrfTokenValue;

                    var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                    {
                        HttpOnly = true,
                        Value = _antiXsrfTokenValue
                    };
                    if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                    {
                        responseCookie.Secure = true;
                    }
                    Response.Cookies.Set(responseCookie);
                }

                Page.PreLoad += master_Page_PreLoad;
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
                    JS.Move("이미 로그인 상태입니다.\\n로그아웃 후 이용해주세요.", "/index");
            }
            #endregion

            if (Common.User.AppSetting.DevMode != "DEV" && !Request.IsSecureConnection)
            {
                string redirectUrl = Request.Url.AbsoluteUri.Replace("http:", "https:");
                Response.Redirect(redirectUrl);
            }
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Anti-XSRF 토큰 설정
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Anti-XSRF 토큰 유효성 검사
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Anti-XSRF 토큰의 유효성을 검사하지 못했습니다.");
                }
            }
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
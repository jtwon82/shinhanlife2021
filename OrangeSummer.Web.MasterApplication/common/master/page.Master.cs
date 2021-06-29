using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Auth;
using MLib.Config;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;
using System.Web.Security;

namespace OrangeSummer.Web.MasterApplication.common.master
{
    public partial class page : System.Web.UI.MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            string url = ServerVariables.Url;
            bool checker = false;
            string[] directory = { "/default.aspx" };
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
                    Tool.RR("/?referer=" + Agent.Referer());
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

            if (Common.Master.AppSetting.DevMode != "DEV" && !Request.IsSecureConnection)
            {
                //string redirectUrl = Request.Url.AbsoluteUri.Replace("http:", "https:");
                string nextUrl = "https://" + HttpContext.Current.Request.Url.Host + ":4433/";
                Response.Redirect(nextUrl);
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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.UnAuthorize();
                Tool.RR("/");
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnPwd_Click(object sender, EventArgs e)
        {
            try
            {
                string id = Common.Master.Identify.Id;
                string pwd = Element.Get(this.upwd1);
                using (Business.Admin biz = new Business.Admin(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Pwd(id, pwd);
                    if (result)
                    {
                        Forms.UnAuthorize();
                        JS.Move("비밀번호가 수정되었습니다.\\n로그인 후 이용해주세요.", "/");
                    }
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
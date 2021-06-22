using MLib.Auth;
using MLib.Cipher;
using MLib.Data;
using MLib.Logger;
using MLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web2.UserApplication.member.login
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageLoad();
            }
        }

        private void PageLoad()
        {
            try
            {
                string secret = MLib.Auth.Web.Cookies("ORANGESUMMER", "SECRET");
                if (!Check.IsNone(secret))
                {
                    secret = AES.Decrypt(Common.User.AppSetting.EncKey, secret);
                    string code = Tool.Separator(secret, "|", 0);
                    string pwd = Tool.Separator(secret, "|", 1);

                    Login(code, pwd);
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string code = Element.Get(this.code);
                string pwd = Element.Get(this.pwd);

                Login(code, pwd);
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        private void Login(string code, string pwd)
        {
            string referer = Check.IsNone(Request["referer"], "");
            using (Business.Member biz = new Business.Member(OrangeSummer.Common.User.AppSetting.Connection))
            {
                Model.Member member = biz.UserLogin(code, pwd);
                if (member != null)
                {
                    string[] array = { member.Id, member.Code, member.Name, member.Level, member.Branch.Id, member.Branch.Name, member.ProfileImg, member.BackgroundImg };
                    
                    Forms.Authorize(OrangeSummer.Common.User.AppSetting.EncKey, member.Id, array);
                    string remember = Element.Get(this.remember);
                    if (remember == "Y")
                        MLib.Auth.Web.Cookies("ORANGESUMMER", "SECRET", AES.Encrypt(Common.User.AppSetting.EncKey, $"{code}|{pwd}|{DateTime.Now.ToString("yyyyMMddHHmmss")}"), 360);

                    if (!Check.IsNone(referer))
                        Tool.RR(referer);
                    else
                    {
                        if (",FC,NEWFC".Contains(","+ member.Level) )
                        {
                            Tool.RR("/achieve/bm");
                        }
                        else if (",SL".Contains("," + member.Level))
                        {
                            Tool.RR("/achieve/sl");
                        }
                        else if (",BM,EM,ERM".Contains("," + member.Level))
                        {
                            Tool.RR("/achieve/point");
                        }
                    }
                }
                else
                {
                    MLib.Auth.Web.Cookies("ORANGESUMMER", "SECRET", "", -1);
                    JS.Move("일치하는 로그인 정보를 확인할 수 없습니다.", "/member/login/");
                }
            }
        }
    }
}
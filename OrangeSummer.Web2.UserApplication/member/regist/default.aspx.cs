using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;
using OrangeSummer.Common.User;
using MLib.Logger;
using MLib.Auth;

namespace OrangeSummer.Web2.UserApplication.member.regist
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void PageLoad()
        {
            
        }

        protected void btnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                Model.Member member = new Model.Member();
                member.Id = Tool.UniqueNewGuid;
                //member.FkBranch = Tool.UniqueNewGuid;
                //member.FkTravel = Tool.UniqueNewGuid;
                member.Code = Element.Get(this.user_fccode);
                member.Pwd = Element.Get(this.user_password);
                member.Reset = "N";
                member.Level = "";
                member.Name = Element.Get(this.user_name);
                member.Mobile = Element.Get(this.user_tel);
                member.AdvertYn = Element.Get(this.advert);

                new Log().Info($"{member.Id}, {member.Code}, {member.Pwd}, {member.Level}, {member.Name}, {member.Mobile}, {member.AdvertYn}");

                using (Business.Member biz = new Business.Member(AppSetting.Connection))
                {
                    bool result = biz.UserRegist(member);
                    if (result)
                    {
                        Login(member.Code, member.Pwd);
                        //JS.Move("놀라운썸머 회원가입이 완료되었습니다.\\n로그인 후 이용 가능합니다.", "/member/login/");
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
                    //string remember = Element.Get(this.remember);
                    //if (remember == "Y")
                    //    MLib.Auth.Web.Cookies("ORANGESUMMER", "SECRET", AES.Encrypt(Common.User.AppSetting.EncKey, $"{code}|{pwd}|{DateTime.Now.ToString("yyyyMMddHHmmss")}"), 360);

                    if (!Check.IsNone(referer))
                        Tool.RR(referer);
                    else
                    {
                        Tool.RR("/index");
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
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web2.UserApplication.member.find_pw
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
                this.pageLoad.Visible = true;
                this.result.Visible = false;
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                Model.Member member = new Model.Member();
                //member.Id = Tool.UniqueNewGuid;
                //member.FkBranch = Tool.UniqueNewGuid;
                //member.FkTravel = Tool.UniqueNewGuid;
                member.Code = Element.Get(this.user_fccode);
                //member.Pwd = Element.Get(this.user_password);
                //member.Reset = "N";
                //member.Level = "";
                member.Name = Element.Get(this.user_name);
                member.Mobile = Element.Get(this.user_tel);
                //member.AdvertYn = Element.Get(this.advert);

                using (Business.Member biz = new Business.Member(AppSetting.Connection))
                {
                    Model.Member result = biz.UserDetailV2(member);
                    if (result.Pwd !="" )
                    {
                        Element.Set(this.find_pw, result.Pwd);
                        this.pageLoad.Visible = false;
                        this.result.Visible = true;
                    }
                    else
                        JS.Back("일치하는 정보가 없습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
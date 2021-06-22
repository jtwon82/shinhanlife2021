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

namespace OrangeSummer.Web.UserApplication.member.regist
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
                // 지점
                using (Business.Branch biz = new Business.Branch(Common.User.AppSetting.Connection))
                {
                    List<Model.Branch> list = biz.Line();
                    if (list != null)
                    {
                        this.branch.DataSource = list;
                        this.branch.DataTextField = "Name";
                        this.branch.DataValueField = "Id";
                        this.branch.DataBind();
                    }

                    this.branch.Items.Insert(0, new ListItem("지점 선택", ""));
                }

                // 신분
                Dictionary<string, string> dic = Code.MemberLevel;
                this.level.DataSource = dic;
                this.level.DataTextField = "Value";
                this.level.DataValueField = "Key";
                this.level.DataBind();
                this.level.Items.Insert(0, new ListItem("신분 선택", ""));
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
                member.Id = Tool.UniqueNewGuid;
                member.FkBranch = Element.Get(this.branch);
                member.FkTravel = "";
                member.Code = Element.Get(this.code);
                member.Pwd = Element.Get(this.pwd1);
                member.Reset = "N";
                member.Level = Element.Get(this.level);
                member.Name = Element.Get(this.name);
                member.Mobile = Element.Get(this.mobile);
                member.AdvertYn = Element.Get(this.advert);
                using (Business.Member biz = new Business.Member(AppSetting.Connection))
                {
                    bool result = biz.UserRegist(member);
                    if (result)
                        JS.Move("슬기로운썸머생활 회원가입이 완료되었습니다.\\n로그인 후 이용 가능합니다.", "/member/login/");
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
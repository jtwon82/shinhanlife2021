using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.DataBase;

namespace OrangeSummer.Web.UserApplication
{
    public partial class template : System.Web.UI.Page
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
                //Template template = new Template(Common.User.AppSetting.Connection, "전윤기", "MEMBER", "회원관리", "USP_MEMBER", "OrangeSummer", "member");
                //Template template = new Template(Common.User.AppSetting.Connection, "전윤기", "UCC_REPLY", "UCC댓글", "USP_UCC_REPLY", "OrangeSummer", "reply");
                //Template template = new Template(Common.User.AppSetting.Connection, "전윤기", "ROULETTE", "룰렛", "USP_ROULETTE", "OrangeSummer", "roulette");
                //Template template = new Template(Common.User.AppSetting.Connection, "전윤기", "NOTICE", "공지사항", "USP_NOTICE", "OrangeSummer", "notice");
                //Template template = new Template(Common.User.AppSetting.Connection, "전윤기", "EVENT", "이벤트", "USP_EVENT", "OrangeSummer", "evt");
                //template.All();
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
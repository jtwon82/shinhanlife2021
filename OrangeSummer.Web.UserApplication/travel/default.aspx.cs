using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Data;
using MLib.Util;

namespace OrangeSummer.Web.UserApplication.travel
{
    public partial class _default : System.Web.UI.Page
    {
        protected string _travel = string.Empty;
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
                // 조회
                using (Business.Member biz = new Business.Member(Common.User.AppSetting.Connection))
                {
                    Model.Member member = biz.Detail(Common.User.Identify.Id);
                    if (member != null)
                    {
                        _travel = member.FkTravel;
                        Element.Set(this.travel, member.FkTravel);
                    }
                }

                using (Business.Travel biz = new Business.Travel(Common.User.AppSetting.Connection))
                {
                    List<Model.Travel> list = biz.UserLine();
                    if (list != null)
                    {
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                string travel = Element.Get(this.travel);
                using (Business.Member biz = new Business.Member(Common.User.AppSetting.Connection))
                {
                    bool result = biz.UserTravel(Common.User.Identify.Id, travel);
                    if (result)
                        JS.Move("여행지가 변경되었습니다.", "/achieve/");
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
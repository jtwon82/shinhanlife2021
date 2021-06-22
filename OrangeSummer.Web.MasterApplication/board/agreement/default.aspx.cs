using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Data;
using MLib.Util;

namespace OrangeSummer.Web.MasterApplication.board.agreement
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
                using (Business.Agreement biz = new Business.Agreement(Common.Master.AppSetting.Connection))
                {
                    Model.Agreement notice = biz.Detail();
                    if (notice != null)
                    {
                        Element.Set(this.service, notice.Service);
                        Element.Set(this.person, notice.Person);
                        Element.Set(this.marketing, notice.Marketing);
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
                Model.Agreement agreement = new Model.Agreement();
                agreement.Service = Element.Get(this.service);
                agreement.Person = Element.Get(this.person);
                agreement.Marketing = Element.Get(this.marketing);
                using (Business.Agreement biz = new Business.Agreement(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Modify(agreement);
                    if (result)
                        JS.Move("수정되었습니다.", "./");
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
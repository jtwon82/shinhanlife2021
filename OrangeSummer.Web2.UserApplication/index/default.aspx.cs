using MLib.Data;
using MLib.Util;
using OrangeSummer.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web2.UserApplication.index
{
    public partial class _default : System.Web.UI.Page
    {
        public Model.Member _member = null;

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
                if (!Check.IsNone(Common.User.Identify.Id))
                {
                    Model.Member member = null;
                    using (Business.Member biz = new Business.Member(Common.Master.AppSetting.Connection))
                    {
                        member = biz.UserDetail(Common.User.Identify.Id);
                        if (member != null)
                        {
                            _member = member;
                        }
                        else
                        {
                            Tool.RR("/member/login");
                        }
                    }

                    #region [ 이벤트 배너 ]
                    using (Business.Banner biz = new Business.Banner(Common.User.AppSetting.Connection))
                    {
                        List<Model.Banner> list = biz.UserList("MAIN");
                        if (list != null)
                        {
                            this.rptBannerList.DataSource = list;
                            this.rptBannerList.DataBind();
                        }
                    }
                    #endregion
                }
                else
                {
                    _member = new Model.Member();
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

    }
}
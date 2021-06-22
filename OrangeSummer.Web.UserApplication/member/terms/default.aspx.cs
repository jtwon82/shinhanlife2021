using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web.UserApplication.member.terms
{
    public partial class _default : System.Web.UI.Page
    {
        protected string _service = string.Empty;
        protected string _person = string.Empty;
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
                using (Business.Agreement biz = new Business.Agreement(Common.User.AppSetting.Connection))
                {
                    Model.Agreement notice = biz.Detail();
                    if (notice != null)
                    {
                        _service = notice.Service;
                        _person = notice.Person;
                    }
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
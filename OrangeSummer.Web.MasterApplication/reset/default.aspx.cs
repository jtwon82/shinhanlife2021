using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication.reset
{
    public partial class _default : System.Web.UI.Page
    {
        protected string _pwd = string.Empty;
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
                string id = Check.IsNone(Request["id"], true);
                string pwd = Check.IsNone(Request["pwd"], true);

                using (Business.Admin biz = new Business.Admin(Common.Master.AppSetting.Connection))
                {
                    Model.Admin admin = biz.Login(id, pwd);
                    if (admin != null)
                    {
                        _pwd = admin.Pwd;
                        Element.Set(this.id, admin.Id);
                    }
                    else
                        JS.Back("로그인 정보를 확인해주세요.");
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
                string id = Element.Get(this.id);
                string pwd = Element.Get(this.upwd1);
                using (Business.Admin biz = new Business.Admin(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Pwd(id, pwd);
                    if (result)
                        JS.Move("비밀번호가 수정되었습니다.\\n로그인 후 이용해주세요.", "/");
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
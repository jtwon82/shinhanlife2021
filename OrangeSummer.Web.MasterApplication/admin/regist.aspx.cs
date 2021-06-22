using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication.admin
{
    public partial class regist : System.Web.UI.Page
    {
        protected string _command = string.Empty;
        protected string _usr = string.Empty;
        protected string _admName = string.Empty;
        protected string _registDate = string.Empty;
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
                string id = Check.IsNone(Request["id"], false);
                if (!Check.IsNone(id))
                {
                    Model.Admin admin = null;
                    using (Business.Admin biz = new Business.Admin(Common.Master.AppSetting.Connection))
                    {
                        admin = biz.Detail(id);
                        if (admin != null)
                        {
                            _command = "mod";
                            _usr = admin.Usr;
                            _registDate = admin.RegistDate;
                            _admName = admin.Adm.Name;

                            Element.Set(this.useyn, admin.UseYn);
                            Element.Set(this.name, admin.Name);
                            Element.Set(this.usr, admin.Usr);
                            Element.Set(this.phone, admin.Phone);
                            Element.Set(this.email, admin.Email);

                            this.btnModify.Visible = true;
                            this.btnDelete.Visible = true;

                            if (admin.Reset == "Y")
                                this.btnReset.Visible = false;
                        }
                        else
                        {
                            _command = "add";
                            this.btnRegist.Visible = true;
                        }
                    }
                }
                else
                {
                    _command = "add";
                    this.btnRegist.Visible = true;
                }
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
                Model.Admin admin = new Model.Admin();
                admin.Id = Tool.UniqueNewGuid;
                admin.FkAdmin = Common.Master.Identify.Id;
                admin.Usr = Element.Get(this.usr);
                admin.Pwd = Element.Get(this.pwd1);
                admin.Name = Element.Get(this.name);
                admin.Reset = "N";
                admin.Phone = Element.Get(this.phone);
                admin.Email = Element.Get(this.email);
                admin.UseYn = Element.Get(this.useyn);
                admin.DelYn = "N";
                using (Business.Admin biz = new Business.Admin(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Regist(admin);
                    if (result)
                        Tool.RR("./");
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
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
                string id = Check.IsNone(Request["id"], true);
                Model.Admin admin = new Model.Admin();
                admin.Id = id;
                admin.Name = Element.Get(this.name);
                admin.Phone = Element.Get(this.phone);
                admin.Email = Element.Get(this.email);
                admin.UseYn = Element.Get(this.useyn);
                using (Business.Admin biz = new Business.Admin(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Modify(admin);
                    if (result)
                        JS.Move("수정되었습니다.", $"regist.aspx?id={id}{Parameters()}");
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = Check.IsNone(Request["id"], true);
                using (Business.Admin biz = new Business.Admin(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Delete(id);
                    if (result)
                        Tool.RR($"./?command=list{Parameters()}");
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                string id = Check.IsNone(Request["id"], true);
                using (Business.Admin biz = new Business.Admin(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Reset(id);
                    if (result)
                        Tool.RR($"regist.aspx?id={id}{Parameters()}");
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        /// <summary>
        /// 파라메터 조합
        /// </summary>
        /// <returns></returns>
        protected string Parameters()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("&page=" + Check.IsNone(Request["page"], "1"));

            return sb.ToString();
        }
    }
}
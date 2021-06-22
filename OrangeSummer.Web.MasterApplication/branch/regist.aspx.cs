using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication.branch
{
    public partial class regist : System.Web.UI.Page
    {
        protected string _command = string.Empty;
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
                using (Business.Travel biz = new Business.Travel(Common.Master.AppSetting.Connection))
                {
                    List<Model.Travel> list = biz.Line();
                    if (list != null)
                    {
                        this.fktravel.DataSource = list;
                        this.fktravel.DataTextField = "Name";
                        this.fktravel.DataValueField = "Id";
                        this.fktravel.DataBind();
                    }

                    this.fktravel.Items.Insert(0, new ListItem("선택", ""));
                }

                if (!Check.IsNone(id))
                {
                    Model.Branch branch = null;
                    using (Business.Branch biz = new Business.Branch(Common.Master.AppSetting.Connection))
                    {
                        branch = biz.Detail(id);
                        if (branch != null)
                        {
                            _command = "mod";
                            _registDate = branch.RegistDate;
                            _admName = branch.Admin.Name;

                            Element.Set(this.name, branch.Name);
                            Element.Set(this.fktravel, branch.FkTravel);
                            Element.Set(this.delyn, branch.DelYn);

                            this.btnModify.Visible = true;
                            this.name.Enabled = false;
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
                Model.Branch branch = new Model.Branch();
                branch.Id = Tool.UniqueNewGuid;
                branch.FkAdmin = Common.Master.Identify.Id;
                branch.FkTravel = Element.Get(this.fktravel);
                branch.Name = Element.Get(this.name);
                using (Business.Branch biz = new Business.Branch(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Regist(branch);
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
                Model.Branch branch = new Model.Branch();
                branch.Id = id;
                branch.FkTravel = Element.Get(this.fktravel);
                branch.DelYn = Element.Get(this.delyn);
                using (Business.Branch biz = new Business.Branch(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Modify(branch);
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
                using (Business.Branch biz = new Business.Branch(Common.Master.AppSetting.Connection))
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

        /// <summary>
        /// 파라메터 조합
        /// </summary>
        /// <returns></returns>
        protected string Parameters()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("&page=" + Check.IsNone(Request["page"], "1"));
            sb.Append("&branch=" + Check.IsNone(Request["branch"], ""));
            sb.Append("&travel=" + Check.IsNone(Request["travel"], ""));

            return sb.ToString();
        }
    }
}
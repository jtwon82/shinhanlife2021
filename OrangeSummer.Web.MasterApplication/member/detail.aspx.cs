using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication.member
{
    public partial class detail : System.Web.UI.Page
    {
        protected string _branch = string.Empty;
        protected string _travel = string.Empty;
        protected string _code = string.Empty;
        protected string _pwd = string.Empty;
        protected string _reset = string.Empty;
        protected string _level = string.Empty;
        protected string _name = string.Empty;
        protected string _mobile = string.Empty;
        protected string _advert = string.Empty; 
        protected string _registDate = string.Empty;
        protected string _profileimg = string.Empty;
        protected string _backbroundimg = string.Empty;

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
                using (Business.Member biz = new Business.Member(Common.Master.AppSetting.Connection))
                {
                    Model.Member member = biz.Detail(id);
                    if (member != null)
                    {
                        // 지점
                        using (Business.Branch bbiz = new Business.Branch(Common.Master.AppSetting.Connection))
                        {
                            List<Model.Branch> list = bbiz.Line();
                            if (list != null)
                            {
                                this.branch.DataSource = list;
                                this.branch.DataTextField = "Name";
                                this.branch.DataValueField = "Id";
                                this.branch.DataBind();
                            }

                            this.branch.Items.Insert(0, new ListItem("선택", ""));
                        }

                        // 신분
                        Dictionary<string, string> dic = Code.MemberLevel;
                        this.level.DataSource = dic;
                        this.level.DataTextField = "Value";
                        this.level.DataValueField = "Key";
                        this.level.DataBind();
                        this.level.Items.Insert(0, new ListItem("선택", ""));

                        Element.Set(this.branch, member.FkBranch);
                        Element.Set(this.level, member.Level);
                        Element.Set(this.delyn, member.DelYn);
                        _travel = member.Travel.Name;
                        _code = member.Code;
                        _pwd = member.Pwd;
                        _reset = member.Reset;
                        _name = member.Name;
                        _mobile = member.Mobile;
                        _advert = member.AdvertYn == "Y" ? "수신함" : "수신안함";
                        _registDate = member.RegistDate;
                        _profileimg = member.ProfileImg;
                        _backbroundimg = member.BackgroundImg;
                    }
                    else
                        JS.Back("잘못된 접근입니다.");
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
            sb.Append("&level=" + Check.IsNone(Request["level"], ""));
            sb.Append("&code=" + Check.IsNone(Request["code"], ""));
            sb.Append("&mobile=" + Check.IsNone(Request["mobile"], ""));
            sb.Append("&sdate=" + Check.IsNone(Request["sdate"], ""));
            sb.Append("&edate=" + Check.IsNone(Request["edate"], ""));
            sb.Append("&change_pwd=" + Check.IsNone(Request["change_pwd"], ""));

            return sb.ToString();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = Check.IsNone(Request["id"], true);
                using (Business.Member biz = new Business.Member(Common.Master.AppSetting.Connection))
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
                string change_pwd = Element.Get(this.change_pwd);

                using (Business.Member biz = new Business.Member(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Reset(id, change_pwd);
                    if (result)
                    {
                        Tool.RR($"detail.aspx?id={id}{Parameters()}");
                    }
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
                Model.Member member = new Model.Member();
                member.Id = id;
                member.FkBranch = Element.Get(this.branch);
                member.Level = Element.Get(this.level);
                member.DelYn = Element.Get(this.delyn);
                using (Business.Member biz = new Business.Member(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Modify(member);
                    if (result)
                        JS.Move("수정되었습니다.", $"detail.aspx?id={id}{Parameters()}");
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
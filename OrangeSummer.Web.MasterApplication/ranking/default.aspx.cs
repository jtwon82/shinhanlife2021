using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDataReader;
using MLib.Attach;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication.ranking
{
    public partial class _default : System.Web.UI.Page
    {
        protected int _total = 0;
        protected string _paging = string.Empty;
        protected string _orderby = string.Empty;
        private int _size = 10;
        private int _block = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageLoad();
            }
        }

        /// <summary>
        /// 관리자 리스트
        /// </summary>
        private void PageLoad()
        {
            try
            {
                int page = Check.IsNone(Request["page"], 1);
                string branch = Check.IsNone(Request["branch"], "");
                string level = Check.IsNone(Request["level"], "");
                string code = Check.IsNone(Request["code"], "");
                string name = Check.IsNone(Request["name"], "");
                _orderby = Check.IsNone(Request["orderby"], "PERSON_RANK");

                // 지점
                using (Business.Branch biz = new Business.Branch(Common.Master.AppSetting.Connection))
                {
                    List<Model.Branch> list = biz.Line();
                    if (list != null)
                    {
                        this.branch.DataSource = list;
                        this.branch.DataTextField = "Name";
                        this.branch.DataValueField = "Id";
                        this.branch.DataBind();
                    }

                    this.branch.Items.Insert(0, new ListItem("전체", ""));
                }

                // 신분
                Dictionary<string, string> dic = Code.MemberLevel;
                this.level.DataSource = dic;
                this.level.DataTextField = "Value";
                this.level.DataValueField = "Key";
                this.level.DataBind();
                this.level.Items.Insert(0, new ListItem("전체", ""));

                Element.Set(this.branch, branch);
                Element.Set(this.level, level);
                Element.Set(this.code, code);
                Element.Set(this.name, name);

                using (Business.Achievement biz = new Business.Achievement(Common.Master.AppSetting.Connection))
                {
                    List<Model.Achievement> list = biz.List(page, _size, _orderby, branch, level, code, name);
                    if (list != null)
                    {
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                        this.noData.Visible = false;
                        _total = list[0].Total;
                    }

                    Common.Master.Paging paging = new Common.Master.Paging("./", page, _size, _block, _total);
                    paging.AddParams("branch", branch);
                    paging.AddParams("level", level);
                    paging.AddParams("code", code);
                    paging.AddParams("name", name);
                    paging.AddParams("orderby", _orderby);
                    _paging = paging.ToString();
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Url url = new Url("./");
            url.AddParams("branch", Element.Get(this.branch));
            url.AddParams("level", Element.Get(this.level));
            url.AddParams("code", Element.Get(this.code));
            url.AddParams("name", Element.Get(this.name));
            url.AddParams("orderby", Check.IsNone(Request["orderby"], "PERSON_RANK"));
            
            url.Redirect();
        }

        protected string ListNumber(object obj, int index)
        {
            int page = Check.IsNone(Request["page"], 1);
            int number = (Convert.ToInt32(obj) - _size * (page - 1) - index);
            return number.ToString();
        }
    }
}
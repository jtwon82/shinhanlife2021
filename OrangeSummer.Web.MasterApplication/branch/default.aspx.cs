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
    public partial class _default : System.Web.UI.Page
    {
        protected int _total = 0;
        protected string _paging = string.Empty;
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
                string travel = Check.IsNone(Request["travel"], "");

                using (Business.Travel biz = new Business.Travel(Common.Master.AppSetting.Connection))
                {
                    List<Model.Travel> list = biz.Line();
                    if (list != null)
                    {
                        this.travel.DataSource = list;
                        this.travel.DataTextField = "Name";
                        this.travel.DataValueField = "Id";
                        this.travel.DataBind();
                    }

                    this.travel.Items.Insert(0, new ListItem("선택", ""));
                }
                Element.Set(this.branch, branch);
                Element.Set(this.travel, travel);

                using (Business.Branch biz = new Business.Branch(Common.Master.AppSetting.Connection))
                {
                    List<Model.Branch> list = biz.List(page, _size, branch, travel);
                    if (list != null)
                    {
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                        this.noData.Visible = false;
                        _total = list[0].Total;
                    }

                    Common.Master.Paging paging = new Common.Master.Paging("./", page, _size, _block, _total);
                    paging.AddParams("branch", branch);
                    paging.AddParams("travel", travel);
                    _paging = paging.ToString();
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        /// <summary>
        /// 관리자 리스트 번호
        /// </summary>
        protected string ListNumber(object obj, int index)
        {
            int page = Check.IsNone(Request["page"], 1);
            int number = (Convert.ToInt32(obj) - _size * (page - 1) - index);
            return number.ToString();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Url url = new Url("./");
            url.AddParams("branch", Element.Get(this.branch));
            url.AddParams("travel", Element.Get(this.travel));
            url.Redirect();
        }
    }
}
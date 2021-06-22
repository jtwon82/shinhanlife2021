using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication.board.notice
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
                string type = Check.IsNone(Request["type"], "");
                string title = Check.IsNone(Request["title"], "");
                string use = Check.IsNone(Request["use"], "");
                string sdate = Check.IsNone(Request["sdate"], "");
                string edate = Check.IsNone(Request["edate"], "");

                Element.Set(this.type, type);
                Element.Set(this.title, title);
                Element.Set(this.use, use);
                Element.Set(this.sdate, sdate);
                Element.Set(this.edate, edate);

                using (Business.Notice biz = new Business.Notice(Common.Master.AppSetting.Connection))
                {
                    List<Model.Notice> list = biz.List(page, _size, type, title, use, sdate, edate);
                    if (list != null)
                    {
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                        this.noData.Visible = false;
                        _total = list[0].Total;
                    }

                    Common.Master.Paging paging = new Common.Master.Paging("./", page, _size, _block, _total);
                    paging.AddParams("type", type);
                    paging.AddParams("title", title);
                    paging.AddParams("use", use);
                    paging.AddParams("sdate", sdate);
                    paging.AddParams("edate", edate);
                    _paging = paging.ToString();
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected string ListNumber(object obj, int index)
        {
            int page = Check.IsNone(Request["page"], 1);
            int number = (Convert.ToInt32(obj) - _size * (page - 1) - index);
            return number.ToString();
        }

        protected string Parameters()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("&page=" + Check.IsNone(Request["page"], "1"));
            sb.Append("&type=" + Check.IsNone(Request["type"], ""));
            sb.Append("&title=" + Check.IsNone(Request["title"], ""));
            sb.Append("&use=" + Check.IsNone(Request["use"], ""));
            sb.Append("&sdate=" + Check.IsNone(Request["sdate"], ""));
            sb.Append("&edate=" + Check.IsNone(Request["edate"], ""));

            return sb.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Url url = new Url("./");
            url.AddParams("type", Element.Get(this.type));
            url.AddParams("title", Element.Get(this.title));
            url.AddParams("use", Element.Get(this.use));
            url.AddParams("sdate", Element.Get(this.sdate));
            url.AddParams("edate", Element.Get(this.edate));
            url.Redirect();
        }
    }
}
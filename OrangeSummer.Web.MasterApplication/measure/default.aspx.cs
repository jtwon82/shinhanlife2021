using MLib.Data;
using MLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web.MasterApplication.measure
{
    public partial class _default : System.Web.UI.Page
    {
        public int _total = 0;
        public string _paging = string.Empty;
        public int _size = 10;
        public int _block = 10;

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
                string gubun = Check.IsNone(Request["gubun"], "");
                string title = Check.IsNone(Request["title"], "");
                string useYn = Check.IsNone(Request["useYn"], "");
                string sdate = Check.IsNone(Request["sdate"], "");
                string edate = Check.IsNone(Request["edate"], "");

                // 지점
                //using (Business.Branch biz = new Business.Branch(Common.Master.AppSetting.Connection))
                //{
                //    List<Model.Branch> list = biz.Line();
                //    if (list != null)
                //    {
                //        this.branch.DataSource = list;
                //        this.branch.DataTextField = "Name";
                //        this.branch.DataValueField = "Id";
                //        this.branch.DataBind();
                //    }

                //    this.branch.Items.Insert(0, new ListItem("전체", ""));
                //}

                // 신분
                //Dictionary<string, string> dic = Code.MemberLevel;
                //this.level.DataSource = dic;
                //this.level.DataTextField = "Value";
                //this.level.DataValueField = "Key";
                //this.level.DataBind();
                //this.level.Items.Insert(0, new ListItem("전체", ""));

                Element.Set(this.gubun, gubun);
                Element.Set(this.title, title);
                Element.Set(this.useYn, useYn);
                Element.Set(this.sdate, sdate);
                Element.Set(this.edate, edate);

                using (Business.Measure biz = new Business.Measure(Common.Master.AppSetting.Connection))
                {
                    List<Model.Measure> list = biz.List(page, _size, gubun, title, useYn, sdate, edate);
                    if (list != null)
                    {
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                        this.noData.Visible = false;
                        _total = list[0].Total;
                    }

                    Common.Master.Paging paging = new Common.Master.Paging("./", page, _size, _block, _total);
                    paging.AddParams("gubun", gubun);
                    paging.AddParams("useYn", useYn);
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
            sb.Append("&gubun=" + Check.IsNone(Request["gubun"], ""));
            sb.Append("&useYn=" + Check.IsNone(Request["useYn"], ""));
            sb.Append("&sdate=" + Check.IsNone(Request["sdate"], ""));
            sb.Append("&edate=" + Check.IsNone(Request["edate"], ""));

            return sb.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Url url = new Url("./");
            url.AddParams("gubun", Element.Get(this.gubun));
            url.AddParams("useYn", Element.Get(this.useYn));
            url.AddParams("sdate", Element.Get(this.sdate));
            url.AddParams("edate", Element.Get(this.edate));
            url.Redirect();
        }

    }
}
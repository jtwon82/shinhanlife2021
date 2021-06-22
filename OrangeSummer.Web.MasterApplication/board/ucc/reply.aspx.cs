using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Util;

namespace OrangeSummer.Web.MasterApplication.board.ucc
{
    public partial class reply : System.Web.UI.Page
    {
        protected string _paging = string.Empty;
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
                int page = Check.IsNone(Request["page"], 1);
                int size = 10;
                using (Business.UccReply biz = new Business.UccReply(Common.Master.AppSetting.Connection))
                {
                    List<Model.UccReply> list = biz.List(page, size);
                    if (list != null)
                    {
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                        this.noData.Visible = false;
                        int total = list[0].Total;

                        Common.Master.Paging paging = new Common.Master.Paging("reply.aspx", page, size, 10, total);

                        _paging = paging.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnReplyDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                string page = Check.IsNone(Request["page"], "1");
                using (Business.UccReply biz = new Business.UccReply(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Delete(btn.CommandArgument);
                    if (result)
                        Tool.RR($"reply.aspx?page={page}");
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
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
            int number = (Convert.ToInt32(obj) - 10 * (page - 1) - index);
            return number.ToString();
        }

        protected string Depth(string depth)
        {
            int deep = Convert.ToInt32(depth);
            if (deep > 0)
                return new System.Text.StringBuilder().Insert(0, "&nbsp;", deep).ToString() + "<span class=\"material-icons\" style=\"font-size:1rem;\">subdirectory_arrow_right</span>";
            else
                return "";
        }
    }
}
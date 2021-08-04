using MLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web2.UserApplication.board.word
{
    public partial class _default : System.Web.UI.Page
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
                int subpage = Check.IsNone(Request["subpage"], 1);
                int size = 10;
                //using (Business.WordReply biz = new Business.WordReply(Common.User.AppSetting.Connection))
                //{
                //    List<Model.WordReply> list = biz.UserList(1, (size * subpage), Common.User.Identify.Id);
                //    if (list != null)
                //    {
                //        this.rptList.DataSource = list;
                //        this.rptList.DataBind();

                //        int total = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(list[0].Total) / Convert.ToDouble(size)));
                //        if (total > subpage)
                //        {
                //            StringBuilder sb = new StringBuilder();
                //            sb.Append("<div class=\"btn_area page_more\">");
                //            sb.Append($" <a href=\"./?subpage={subpage + 1}#subpage{subpage + 1}\" class=\"btn_more\">더보기</a>");
                //            sb.Append("</div>");
                //            _paging = sb.ToString();
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected string AnchorPage()
        {
            string subpage = Check.IsNone(Request["subpage"], "1");
            return $"<a name=\"subpage{subpage}\"></a>";
        }

        protected string Like(string id, string like, string count, string delete)
        {
            StringBuilder sb = new StringBuilder();
            if (delete == "N")
            {
                sb.AppendFormat("<div class=\"like\" id=\"like_{0}\">", id);
                sb.AppendFormat("   <a href=\"javascript:reply.like('{0}');\"{1}>", id, (like != "0") ? " class=\"on\"" : "");
                sb.AppendFormat("       <img src=\"/resources/img/ico_like{0}.png\" alt=\"\">", like != "0" ? "_chk" : "");
                sb.AppendFormat("   </a>");
                sb.AppendFormat("   <div class=\"number\">LIKE <span>{0}</span></div>", count);
                sb.AppendFormat("</div>");
            }

            return sb.ToString();
        }
    }
}
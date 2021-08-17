using MLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web2.UserApplication.board.roulette
{
    public partial class _default : System.Web.UI.Page
    {
        protected string _contents = string.Empty;
        protected string _adminName = string.Empty;
        protected string _registDate = string.Empty;
        protected string _before = string.Empty;
        protected string _next = string.Empty;
        protected string _paging = string.Empty;
        protected string _result = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //PageLoad();
                RouletteLoad();
            }
        }

        private void RouletteLoad()
        {
            try
            {
                if (",19888,25932,35537,35726,39954,41929,44443,44496,56142,56153,57363,60914,64776,65232,66213,66247,66325,66483".Contains(","+Common.User.Identify.Code))
                {
                    _result = "FAIL";
                }
                else
                {
                    using (Business.Roulette biz = new Business.Roulette(Common.User.AppSetting.Connection))
                    {
                        Random random = new Random();
                        //int count = biz.UserSuccess();
                        int ran = 0;
                        bool check = true;
                        if (biz.UserSuccess())
                        {
                            check = biz.UserCheck(Common.User.Identify.Id);
                            if (check)
                                _result = "EXISTS";
                            else
                            {
                                ran = random.Next(1, 100);

                                if (ran <= 20)
                                    _result = "SUCCESS";
                                else
                                    _result = "FAIL";
                            }
                        }
                        else
                            _result = "FAIL";
                    }
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        //private void PageLoad()
        //{
        //    try
        //    {
        //        string id = Check.IsNone(Request["id"], true);
        //        string type = Check.IsNone(Request["type"], true);

        //        Model.Event notice = null;
        //        using (Business.Event biz = new Business.Event(Common.User.AppSetting.Connection))
        //        {
        //            notice = biz.UserDetail(id);
        //            if (notice != null)
        //            {
        //                _contents = HttpUtility.HtmlDecode(notice.Contents);
        //                _registDate = notice.RegistDate.Substring(2, 8);
        //                _adminName = notice.Admin.Name;

        //                notice = biz.UserBefore(id, type);
        //                if (notice != null)
        //                    _before = $"<a href=\"detail.aspx?id={notice.Id}&type={notice.Type}\">{notice.Title}</a>";
        //                else
        //                    _before = "&nbsp;";

        //                notice = biz.UserNext(id, type);
        //                if (notice != null)
        //                    _next = $"<a href=\"detail.aspx?id={notice.Id}&type={notice.Type}\">{notice.Title}</a>";
        //                else
        //                    _next = "&nbsp;";

        //                #region [ 댓글 ]
        //                int subpage = Check.IsNone(Request["subpage"], 1);
        //                int size = 10;
        //                using (Business.EventReply bizNotice = new Business.EventReply(Common.User.AppSetting.Connection))
        //                {
        //                    List<Model.EventReply> list = bizNotice.UserList(1, (size * subpage), id, Common.User.Identify.Id);
        //                    if (list != null)
        //                    {
        //                        this.rptList.DataSource = list;
        //                        this.rptList.DataBind();

        //                        int total = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(list[0].Total) / Convert.ToDouble(size)));
        //                        if (total > subpage)
        //                        {
        //                            StringBuilder sb = new StringBuilder();
        //                            sb.Append("<div class=\"btn_area page_more\">");
        //                            sb.Append($" <a href=\"detail.aspx?id={id}&type={type}{Parameters()}&subpage={subpage + 1}#subpage{subpage + 1}\" class=\"btn_more\">더보기</a>");
        //                            sb.Append("</div>");
        //                            _paging = sb.ToString();
        //                        }
        //                    }
        //                }
        //                #endregion
        //            }
        //            else
        //                JS.Back("잘못된 접근입니다.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MLib.Util.Error.WebHandler(ex);
        //    }
        //}

        protected string Parameters()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("&page=" + Check.IsNone(Request["page"], "1"));

            return sb.ToString();
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
                sb.AppendFormat("       <img src=\"/resources/img/sub/board/{0}.png\" alt=\"\">", like != "0" ? "likeIcon2" : "unlike");
                sb.AppendFormat("   </a>");
                sb.AppendFormat("   <div class=\"number\">LIKE <span>{0}</span></div>", count);
                sb.AppendFormat("</div>");
            }

            return sb.ToString();
        }
    }
}
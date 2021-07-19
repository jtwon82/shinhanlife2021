using MLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web2.UserApplication.ranking.point
{
    public partial class _default : System.Web.UI.Page
    {
        protected int _total = 0;
        protected string _date = string.Empty;
        protected string _person = string.Empty;
        protected string _sl = string.Empty;
        protected string _branch = string.Empty;
        protected string _paging = string.Empty;
        protected string _tab = string.Empty;

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
                _tab = Check.IsNone(Request["tab"], "");
                StringBuilder sb = new StringBuilder();

                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();
                StringBuilder sb4 = new StringBuilder();
                StringBuilder uniqueChk = new StringBuilder();
                using (Business.Achievement biz = new Business.Achievement(Common.User.AppSetting.Connection))
                {

                    #region [ 지점 ]
                    sb.Clear();
                    sb1.Clear();
                    sb2.Clear();
                    sb3.Clear();
                    sb4.Clear();

                    List<Model.Achievement> branchs = biz.UserRanking(page, 10, "BRANCH");
                    if (branchs != null)
                    {
                        DateTime dt = DateTime.Parse(branchs[0].Date);
                        _date = $"{dt.ToString("yyyy")}년 {dt.ToString("MM")}월 {dt.ToString("dd")}일";
                        _total = branchs[0].Total;
                        int index = 1;
                        foreach (Model.Achievement item in branchs)
                        {
                            string key = $"{item.BranchRank}|{item.BranchCmip}";
                            if (uniqueChk.ToString().Contains(key))
                            {
                                continue;
                            }
                            uniqueChk.Append(key);

                            if (item.BranchRank == "2")
                            {
                                sb2.Append("	<dl class=''>");
                                sb2.Append($"		<dt>{item.BranchRank}위</dt>");
                                sb2.Append($"		<dd><span class='pointer'>{item.Branch.Name}</span>{item.BranchCmip}</dd>");
                                sb2.Append("	</dl>");
                            }
                            else if (item.BranchRank == "1")
                            {
                                sb1.Append("	<dl class='centerBox'>");
                                sb1.Append($"		<dt>{item.BranchRank}위</dt>");
                                sb1.Append($"		<dd><span class='pointer'>{item.Branch.Name}</span>{item.BranchCmip}</dd>");
                                sb1.Append("	</dl>");
                            }
                            else if (item.BranchRank == "3")
                            {
                                sb3.Append("	<dl>");
                                sb3.Append($"		<dt>{item.BranchRank}위</dt>");
                                sb3.Append($"		<dd><span class='pointer'>{item.Branch.Name}</span>{item.BranchCmip}</dd>");
                                sb3.Append("	</dl>");
                            }
                            else
                            {
                                //sb4.Append("<dl>");
                                //sb4.Append($"    <dt>{item.BranchRank}위 | {item.Branch.Name}</dt>");
                                //sb4.Append($"    <dd>{item.BranchCmip}</dd>");
                                //sb4.Append("</dl>");

                                sb4.Append("<dl>");
                                sb4.Append($"	<dt>{item.BranchRank}위  |  {item.Branch.Name}</dt>");
                                sb4.Append($"	<dd>{item.BranchCmip}</dd>");
                                sb4.Append("</dl>");
                            }
                            
                            index++;
                        }

                        sb.Append("<!-- 지점부문 -->");
                        sb.Append("<ul class='rankingUnit'>");
                        sb.Append("	<li>[날짜 기준] " + _date + "</li>");
                        sb.Append("	<li>[ 단위 ]  캠페인 환산 CMIP</li>");
                        sb.Append("</ul>");
                        if (sb1.ToString() != "" || sb2.ToString() != "" || sb3.ToString() != "")
                        {
                            sb.Append("<div class=\"rankingBox point\">");
                            sb.Append(sb2.ToString() + sb1.ToString() + sb3.ToString());
                            sb.Append("</div>");
                        }
                        if (sb4.ToString() != "")
                        {
                            sb.Append("<div class=\"rankingList point\">");
                            sb.Append(sb4.ToString());
                            sb.Append("</div>");
                        }
                        _branch = sb.ToString();

                        Common.User.Paging paging = new Common.User.Paging("./", page, 10, 5, _total, "#tab6-move");
                        //paging.AddParams("tab", "branch");
                        _paging = paging.ToString();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
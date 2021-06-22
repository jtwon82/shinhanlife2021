using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Util;

namespace OrangeSummer.Web.UserApplication.ranking
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
                using (Business.Achievement biz = new Business.Achievement(Common.User.AppSetting.Connection))
                {
                    #region [ 개인 ]
                    sb.Clear();
                    List<Model.Achievement> persons = biz.UserRanking(1, 100, "PERSON");
                    if (persons != null)
                    {
                        DateTime dt = DateTime.Parse(persons[0].Date);
                        _date = $"{dt.ToString("yyyy")}년 {dt.ToString("MM")}월 {dt.ToString("dd")}일";
                        int index = 1;
                        foreach (Model.Achievement item in persons)
                        {
                            if (item.PersonRank == "1" || item.PersonRank == "2" || item.PersonRank == "3")
                            {
                                sb.AppendFormat("<div class=\"item_box ranker{0}\">", (item.PersonRank == "3") ? " last" : "");
                                sb.Append($"    <em>{item.PersonRank}위</em>");
                                sb.Append($"    <strong>{item.Branch.Name}</strong>");
                                sb.Append($"    <span class=\"user\">{item.Name}</span>");
                                sb.Append($"    <span class=\"number\">{item.PersonCmip}</span>");
                                sb.Append("</div>");
                            }
                            else
                            {
                                sb.Append("<div class=\"item_box\">");
                                sb.Append($"    <em>{item.PersonRank}위</em>");
                                sb.Append($"    <span class=\"number\">{item.PersonCmip}</span>");
                                sb.Append("</div>");
                            }

                            index++;
                        }
                    }
                    _person = sb.ToString();
                    #endregion

                    #region [ SL ]
                    sb.Clear();
                    List<Model.Achievement> sls = biz.UserRanking(1, 100, "SL");
                    if (sls != null)
                    {
                        int index = 1;
                        foreach (Model.Achievement item in sls)
                        {
                            if (item.SlRank == "1" || item.SlRank == "2" || item.SlRank == "3")
                            {
                                sb.AppendFormat("<div class=\"item_box ranker{0}\">", (item.SlRank == "3") ? " last" : "");
                                sb.Append($"    <em>{item.SlRank}위</em>");
                                sb.Append($"    <strong>{item.Branch.Name}</strong>");
                                sb.Append($"    <span class=\"user\">{item.Name}</span>");
                                sb.Append($"    <span class=\"number\">{item.SlCmip}</span>");
                                sb.Append("</div>");
                            }
                            else
                            {
                                sb.Append("<div class=\"item_box\">");
                                sb.Append($"    <em>{item.SlRank}위</em>");
                                sb.Append($"    <span class=\"number\">{item.SlCmip}</span>");
                                sb.Append("</div>");
                            }

                            index++;
                        }
                    }
                    _sl = sb.ToString();
                    #endregion

                    #region [ 지점 ]
                    sb.Clear();
                    List<Model.Achievement> branchs = biz.UserRanking(page, 10, "BRANCH");
                    if (branchs != null)
                    {
                        _total = branchs[0].Total;
                        int index = 1;
                        foreach (Model.Achievement item in branchs)
                        {
                            if (item.BranchRank == "1" || item.BranchRank == "2" || item.BranchRank == "3")
                            {
                                sb.AppendFormat("<div class=\"item_box ranker{0}\">", (item.BranchRank == "3") ? " last" : "");
                                sb.Append($"    <em>{item.BranchRank}위</em>");
                                sb.Append($"    <strong>{item.Branch.Name}</strong>");
                                sb.Append($"    <span class=\"number\">{item.BranchCmip}</span>");
                                sb.Append("</div>");
                            }
                            else
                            {
                                sb.Append("<div class=\"item_box\">");
                                sb.Append($"    <em>{item.BranchRank}위</em>");
                                sb.Append($"    <strong>{item.Branch.Name}</strong>");
                                sb.Append($"    <span class=\"number\">{item.BranchCmip}</span>");
                                sb.Append("</div>");
                            }

                            index++;
                        }

                        Common.User.Paging paging = new Common.User.Paging("./", page, 10, 5, _total);
                        paging.AddParams("tab", "branch");
                        _paging = paging.ToString();
                    }
                    _branch = sb.ToString();
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
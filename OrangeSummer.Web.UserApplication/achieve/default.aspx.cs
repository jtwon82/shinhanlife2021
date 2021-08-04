using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Util;

namespace OrangeSummer.Web.UserApplication.achieve
{
    public partial class _default : System.Web.UI.Page
    {
        protected string _pc = string.Empty;
        protected string _mobile = string.Empty;
        protected string _title = string.Empty;
        protected string _contents = string.Empty;
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
                #region [ 여행지 ]
                using (Business.Member biz = new Business.Member(Common.User.AppSetting.Connection))
                {
                    Model.Member member = biz.UserDetail(Common.User.Identify.Id);
                    if (member != null)
                    {
                        _pc = member.Travel.AttPc;
                        _mobile = member.Travel.AttMobile;
                    }
                }
                #endregion

                #region [ 업적 ]
                using (Business.Achievement biz = new Business.Achievement(Common.User.AppSetting.Connection))
                {
                    StringBuilder title = new StringBuilder();
                    StringBuilder contents = new StringBuilder();
                    Model.Achievement achievement = biz.UserList(Common.User.Identify.Code);
                    if (achievement != null)
                    {
                        DateTime dt = DateTime.Parse(achievement.Date);

                        if (Common.User.Identify.Level == "FC" || Common.User.Identify.Level == "신인FC")
                        {
                            title.AppendLine("<div class=\"swiper-container tab_list type01\">");
                            title.AppendLine("  <div class=\"swiper-wrapper\">");
                            title.AppendLine("      <div class=\"swiper-slide\"><a href=\"javascript:;\">개인 부문</a></div>");
                            title.AppendLine("  </div>");
                            title.AppendLine("</div>");


                            contents.AppendLine("<div class=\"swiper-container tab_cont_wrap type01\">");
                            contents.AppendLine("    <div class=\"swiper-wrapper\">");
                            contents.AppendLine("       <div class=\"swiper-slide tab_content01\">");
                            contents.AppendLine("           <div class=\"rank_number\">");
                            contents.AppendLine($"               <div class=\"date\">{dt.ToString("yyyy")}년 {dt.ToString("MM")}월 {dt.ToString("dd")}일 기준</div>");
                            contents.AppendLine($"               <div class=\"user\"><strong>{Common.User.Identify.Name}</strong> 님의 <span>썸머 순위</span></div>");
                            contents.AppendLine($"               <div class=\"number\">{achievement.PersonRank}</div>");
                            contents.AppendLine("           </div>");
                            contents.AppendLine("           <div class=\"rank_cmp\">");
                            contents.AppendLine("               <p>캠페인환산 CMIP</p>");
                            contents.AppendLine($"               <div class=\"cmp\"><strong>{achievement.PersonCmip}</strong>원</div>");
                            contents.AppendLine("           </div>");
                            contents.AppendLine("           <div class=\"rank_cmp\">");
                            contents.AppendLine("               <p>합산 건수</p>");
                            contents.AppendLine($"              <div class=\"cmp\"><strong>{achievement.PersonSum}</strong>건</div>");
                            contents.AppendLine("           </div>");
                            contents.AppendLine("       </div>");
                            contents.AppendLine("   </div>");
                            contents.AppendLine("</div>");
                        }
                        else if (Common.User.Identify.Level == "SL")
                        {
                            title.AppendLine("<div class=\"swiper-container tab_list type02\">");
                            title.AppendLine("  <div class=\"swiper-wrapper\">");
                            title.AppendLine("      <div class=\"swiper-slide\"><a href=\"javascript:;\">개인 부문</a></div>");
                            title.AppendLine("      <div class=\"swiper-slide\"><a href=\"javascript:;\">SL 부문</a></div>");
                            title.AppendLine("  </div>");
                            title.AppendLine("</div>");

                            contents.AppendLine("<div class=\"swiper-container tab_cont_wrap type02\">");
                            contents.AppendLine("    <div class=\"swiper-wrapper\">");
                            contents.AppendLine("       <div class=\"swiper-slide tab_content01\">");
                            contents.AppendLine("           <div class=\"rank_number\">");
                            contents.AppendLine($"               <div class=\"date\">{dt.ToString("yyyy")}년 {dt.ToString("MM")}월 {dt.ToString("dd")}일 기준</div>");
                            contents.AppendLine($"               <div class=\"user\"><strong>{Common.User.Identify.Name}</strong> 님의 <span>썸머 순위</span></div>");
                            contents.AppendLine($"               <div class=\"number\">{achievement.PersonRank}</div>");
                            contents.AppendLine("           </div>");
                            contents.AppendLine("           <div class=\"rank_cmp\">");
                            contents.AppendLine("               <p>캠페인환산 CMIP</p>");
                            contents.AppendLine($"               <div class=\"cmp\"><strong>{achievement.PersonCmip}</strong>원</div>");
                            contents.AppendLine("           </div>");
                            contents.AppendLine("           <div class=\"rank_cmp\">");
                            contents.AppendLine("               <p>합산 건수</p>");
                            contents.AppendLine($"              <div class=\"cmp\"><strong>{achievement.PersonSum}</strong>건</div>");
                            contents.AppendLine("           </div>");
                            contents.AppendLine("       </div>");

                            contents.AppendLine("       <div class=\"swiper-slide tab_content02\">");
                            contents.AppendLine("           <div class=\"rank_number\">");
                            contents.AppendLine($"              <div class=\"date\">{dt.ToString("yyyy")}년 {dt.ToString("MM")}월 {dt.ToString("dd")}일 기준</div>");
                            contents.AppendLine($"              <div class=\"user\"><strong>{Common.User.Identify.Name}</strong> 님의 <span>썸머 순위</span></div>");
                            contents.AppendLine($"              <div class=\"number\">{achievement.SlRank}</div>");
                            contents.AppendLine("           </div>");
                            contents.AppendLine("           <div class=\"rank_cmp\">");
                            contents.AppendLine("               <p>캠페인환산 CMIP</p>");
                            contents.AppendLine($"              <div class=\"cmp\"><strong>{achievement.SlCmip}</strong>원</div>");
                            contents.AppendLine("           </div>");
                            contents.AppendLine("       </div>");
                            contents.AppendLine("   </div>");
                            contents.AppendLine("</div>");


                        }
                        else if (Common.User.Identify.Level == "BM" || Common.User.Identify.Level == "EM" || Common.User.Identify.Level == "ERM")
                        {
                            title.AppendLine("<div class=\"swiper-container tab_list type01\">");
                            title.AppendLine("  <div class=\"swiper-wrapper\">");
                            title.AppendLine("      <div class=\"swiper-slide\"><a href=\"javascript:;\">지점 부문</a></div>");
                            title.AppendLine("  </div>");
                            title.AppendLine("</div>");

                            contents.AppendLine("<div class=\"swiper-container tab_cont_wrap type01\">");
                            contents.AppendLine("    <div class=\"swiper-wrapper\">");
                            contents.AppendLine("       <div class=\"swiper-slide tab_content01\">");
                            contents.AppendLine("           <div class=\"rank_number\">");
                            contents.AppendLine($"              <div class=\"date\">{dt.ToString("yyyy")}년 {dt.ToString("MM")}월 {dt.ToString("dd")}일 기준</div>");
                            contents.AppendLine($"              <div class=\"user\"><strong>{Common.User.Identify.BranchName.Replace("지점", "")}</strong> 지점의 <span>썸머 순위</span></div>");
                            contents.AppendLine($"              <div class=\"number\">{achievement.BranchRank}</div>");
                            contents.AppendLine("           </div>");
                            contents.AppendLine("           <div class=\"rank_cmp\">");
                            contents.AppendLine("               <p>캠페인환산 CMIP</p>");
                            contents.AppendLine($"              <div class=\"cmp\"><strong>{achievement.BranchCmip}</strong>원</div>");
                            contents.AppendLine("           </div>");
                            contents.AppendLine("       </div>");
                            contents.AppendLine("   </div>");
                            contents.AppendLine("</div>");
                        }

                        _title = title.ToString();
                        _contents = contents.ToString();
                    }
                }
                #endregion

                #region [ 이벤트 배너 ]
                using (Business.Banner biz = new Business.Banner(Common.User.AppSetting.Connection))
                {
                    List<Model.Banner> list = biz.UserList("MAIN");
                    if (list != null)
                    {
                        this.rptBannerList.DataSource = list;
                        this.rptBannerList.DataBind();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
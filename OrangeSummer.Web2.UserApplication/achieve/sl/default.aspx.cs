using MLib.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web2.UserApplication.achieve.sl
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
                #region [ 업적 ]
                using (Business.Achievement biz = new Business.Achievement(Common.User.AppSetting.Connection))
                {
                    StringBuilder title = new StringBuilder();
                    StringBuilder contents = new StringBuilder();
                    List<Model.Achievement> achievement = biz.UserList2(Common.User.Identify.Code, OrangeSummer.Common.User.Identify.LevelName);

                    if (achievement != null)
                    {
                        foreach (Model.Achievement item in achievement)
                        {
                            DateTime dt = DateTime.Parse(item.Date);
                            string cdate = $"{dt.ToString("yyyy")}년 {dt.ToString("MM")}월 {dt.ToString("dd")}";

                            if (",SL,E SL,G SL,S SL".Contains("," + OrangeSummer.Common.User.Identify.Level))
                            {
                                string itsMe = item.ItsMe == "0" ? "전 순위 업적" : item.ItsMe == "1" ? "나의 썸머순위" : item.ItsMe == "2" ? "후 순위 업적" : "";

                                contents.AppendLine($"<div class='swiper-slide slide{item.ItsMe}'>");
                                contents.AppendLine("	<div class='bmRanking_box'>");
                                //contents.AppendLine($"          썸머순위<em>-</em></p>");
                                if(OrangeSummer.Common.User.Identify.LevelName=="E SL")
                                    contents.AppendLine($"		<p><span>{cdate}일 기준</span>{itsMe}<em>{item.SlRank}</em></p>");
                                if (OrangeSummer.Common.User.Identify.LevelName == "S SL")
                                    contents.AppendLine($"		<p><span>{cdate}일 기준</span>{itsMe}<em>{item.SlRank2}</em></p>");
                                if (OrangeSummer.Common.User.Identify.LevelName == "G SL")
                                    contents.AppendLine($"		<p><span>{cdate}일 기준</span>{itsMe}<em>{item.SlRank3}</em></p>");
                                contents.AppendLine("		<dl>");
                                contents.AppendLine("			<dt><span>캠페인환산</span>CMIP</dt>");
                                if (OrangeSummer.Common.User.Identify.LevelName == "E SL")
                                    contents.AppendLine($"			<dd>{item.SlCmip}</dd>");
                                if (OrangeSummer.Common.User.Identify.LevelName == "S SL")
                                    contents.AppendLine($"			<dd>{item.SlCmip2}</dd>");
                                if (OrangeSummer.Common.User.Identify.LevelName == "G SL")
                                    contents.AppendLine($"			<dd>{item.SlCmip3}</dd>");
                                //contents.AppendLine($"			<dd>-</dd>");
                                contents.AppendLine("		</dl>");
                                contents.AppendLine("	</div>");
                                contents.AppendLine("</div>");
                            }
                        }

                        _title = title.ToString();
                        _contents = contents.ToString();
                    }
                }
                #endregion

                //#region [ 여행지 ]
                //using (Business.Member biz = new Business.Member(Common.User.AppSetting.Connection))
                //{
                //    Model.Member member = biz.UserDetail(Common.User.Identify.Id);
                //    if (member != null)
                //    {
                //        _pc = member.Travel.AttPc;
                //        _mobile = member.Travel.AttMobile;
                //    }
                //}
                //#endregion

                #region [ 이벤트 배너 ]
                //using (Business.Banner biz = new Business.Banner(Common.User.AppSetting.Connection))
                //{
                //    List<Model.Banner> list = biz.UserList("MAIN");
                //    if (list != null)
                //    {
                //        this.rptBannerList.DataSource = list;
                //        this.rptBannerList.DataBind();
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
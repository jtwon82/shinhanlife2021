﻿using MLib.Logger;
using MLib.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web2.UserApplication.achieve.bm
{
    public partial class _default : System.Web.UI.Page
    {
        protected string _pc = string.Empty;
        protected string _mobile = string.Empty;
        protected string _title = string.Empty;
        protected string _contents = string.Empty;
        protected string _contents2 = string.Empty;

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
                    StringBuilder contents2 = new StringBuilder();
                    List<Model.Achievement> achievement = biz.UserList2(Common.User.Identify.Code, "FC");

                    if (achievement != null)
                    {
                        foreach (Model.Achievement item in achievement)
                        {
                            DateTime dt = DateTime.Parse(item.Date);
                            if (",FC,SL,NEWFC".Contains("," + OrangeSummer.Common.User.Identify.Level))
                            {
                                contents.AppendLine("<div class='swiper-slide'>");
                                contents.AppendLine("	<div class='bmRanking_box'>");
                                contents.AppendLine($"		<p><span>{dt.ToString("yyyy")}년 {dt.ToString("MM")}월 {dt.ToString("dd")}일 기준</span>");
                                contents.AppendLine($"          썸머순위<em>{item.PersonRank}</em></p>");
                                contents.AppendLine("		<dl>");
                                contents.AppendLine("			<dt><span>캠페인환산</span>CMIP</dt>");
                                contents.AppendLine($"			<dd>{item.PersonCmip}</dd>");
                                contents.AppendLine("		</dl>");
                                contents.AppendLine("	</div>");
                                contents.AppendLine("</div>");

                                contents2.AppendLine("<div class='swiper-slide'>");
                                contents2.AppendLine("	<div class='bmRanking_box'>");
                                contents2.AppendLine($"		<p><span>{dt.ToString("yyyy")}년 {dt.ToString("MM")}월 {dt.ToString("dd")}일 기준</span>");
                                contents2.AppendLine($"          썸머순위<em>{item.PersonRank2}</em></p>");
                                contents2.AppendLine("		<dl>");
                                contents2.AppendLine("			<dt><span>캠페인환산</span>CAMP</dt>");
                                contents2.AppendLine($"			<dd>{item.PersonCamp}</dd>");
                                contents2.AppendLine("		</dl>");
                                contents2.AppendLine("	</div>");
                                contents2.AppendLine("</div>");
                            }
                            //else if (",BM,EM,ERM".Contains("," + OrangeSummer.Common.User.Identify.Level))
                            //{
                            //    contents.AppendLine("<div class='swiper-slide'>");
                            //    contents.AppendLine("	<div class='bmRanking_box'>");
                            //    contents.AppendLine($"		<p><span>{dt.ToString("yyyy")}년 {dt.ToString("MM")}월 {dt.ToString("dd")}일 기준</span>썸머순위<em>{item.BranchRank}</em></p>");
                            //    contents.AppendLine("		<dl>");
                            //    contents.AppendLine("			<dt><span>캠페인환산</span>CMIP</dt>");
                            //    contents.AppendLine($"			<dd>{item.PersonCmip}</dd>");
                            //    contents.AppendLine("		</dl>");
                            //    contents.AppendLine("	</div>");
                            //    contents.AppendLine("</div>");
                            //}
                        }

                        _title = title.ToString();
                        _contents = contents.ToString();
                        _contents2 = contents2.ToString();
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
                Tool.RR("/manager/login");
            }
        }
    }
}
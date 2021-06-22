<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.UserApplication.achieve._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="page_achieve" style="background: url(<%= OrangeSummer.Common.User.AppSetting.AwsUrl(_pc) %>) no-repeat center top;">
        <div id="wrap" class="wrapper" style="background: url(<%= OrangeSummer.Common.User.AppSetting.AwsUrl(_mobile) %>) no-repeat center top;">
            <div class="title_wrap">
                <div class="btn_allmenu">
                    <a href="#"><img src="/resources/img/allmenu.png" alt="전체메뉴"></a>
                </div>
                <div class="page_title achieve_title">
                    업적 페이지
                    <a href="/travel/" class="btn_desti">여행지 변경하기</a>
                </div>
            </div>

            <uc1:menu runat="server" id="menu" />

            <div class="rank_wrap">
                <%= _title %>

                <%= _contents %>
            </div>

            <div class="achieve_list">
                <ul>
                    <li>
                        본 데이터는 2020 Summer Contest 진도관리를 위한 보조자료이며, 
                        <br>
                        달성 결과가 아님을 알려드립니다.
                    </li>
                    <li>자세한 내용은 해당 공문을 반드시 참고하시기 바랍니다.</li>
                    <li>2020년 08월 27일 부터 업적 페이지와 썸머 랭킹 데이터를<br />제공하지 않습니다.</li>
                </ul>
            </div>

            <!-- 이벤트 배너 -->
            <div class="swiper-container swiper_event_bottom">
                <div class="swiper-wrapper">
                    <asp:Repeater ID="rptBannerList" runat="server">
                        <ItemTemplate>
                            <div class="swiper-slide">
                                <div class="event_banner_bottom">
                                    <a href="<%# MLib.Util.Check.IsNone(Eval("Link").ToString()) ? "javascript:;" : Eval("Link").ToString() %>">
                                        <img src="<%# OrangeSummer.Common.User.AppSetting.AwsUrl(Eval("AttPc").ToString()) %>" alt="">
                                    </a>
                                &nbsp;</div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <!-- //이벤트 배너 -->
        </div>
        <!-- //wrap -->

        <div class="w_intro_bottom w_only">
            <img src="/resources/img/flogo.png" alt="">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">

</asp:Content>

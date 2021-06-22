<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.UserApplication.ranking._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sub_page page_ranking">
        <div id="wrap" class="wrapper">
            <div class="title_wrap">
                <div class="btn_allmenu">
                    <a href="#"><img src="/resources/img/allmenu.png" alt="전체메뉴"></a>
                </div>
                <div class="page_title">
                    Summer Ranking
                </div>

                <a href="/achieve/" class="btn-home">
                    <img src="/resources/img/hub.png" alt="홈으로">
                </a>
            </div>

            <uc1:menu runat="server" id="menu" />
            <!-- //nav -->

            <div class="rank_list_wrap">
                <div class="swiper-container tab_list">
                    <div class="swiper-wrapper">
                        <div class="swiper-slide"><a href="javascript:;">개인 부문</a></div>
                        <div class="swiper-slide"><a href="javascript:;">SL 부문</a></div>
                        <div class="swiper-slide"><a href="javascript:;">지점 부문</a></div>
                    </div>
                </div>

                <div class="swiper-container tab_cont_wrap">
                    <div class="swiper-wrapper">
                        <!-- 개인 부문 -->
                        <div class="swiper-slide tab_content01">
                            <ul class="desc">
                                <li>
                                    <span>날짜 기준</span>
                                    <%= _date %>
                                </li>
                                <li>
                                    <span>단위</span>
                                    캠페인 환산 CMIP
                                </li>
                                <li class="lh_area">
                                    <span>공지</span>
                                    <div class="txt">2020년 8월 27일 부터 업척 페이지와 썸머 랭킹 <br>데이터를 제공하지 않습니다.</div>
                                </li>
                            </ul>

                            <div class="list_area">
                                <%= _person %>
                            </div>
                        </div>
                        <!-- //개인 부문 -->

                        <!-- SL 부문 -->
                        <div class="swiper-slide tab_content02">
                            <ul class="desc">
                                <li>
                                    <span>날짜 기준</span>
                                    <%= _date %>
                                </li>
                                <li>
                                    <span>단위</span>
                                    캠페인 환산 CMIP
                                </li>
                                <li class="lh_area">
                                    <span>공지</span>
                                    <div class="txt">2020년 8월 27일 부터 업척 페이지와 썸머 랭킹 <br>데이터를 제공하지 않습니다.</div>
                                </li>
                            </ul>

                            <div class="list_area">
                                <%= _sl %>
                            </div>
                        </div>
                        <!-- //SL 부문 -->

                        <!-- 지점 부문 -->
                        <div class="swiper-slide tab_content03">
                            <ul class="desc">
                                <li>
                                    <span>날짜 기준</span>
                                    <%= _date %>
                                </li>
                                <li>
                                    <span>단위</span>
                                    캠페인 환산 CMIP
                                </li>
                                <li class="lh_area">
                                    <span>공지</span>
                                    <div class="txt">2020년 8월 27일 부터 업척 페이지와 썸머 랭킹 <br>데이터를 제공하지 않습니다.</div>
                                </li>
                            </ul>

                            <div class="list_area">
                                <%= _branch %>

                                <%= _paging %>
                            </div>
                        </div>
                        <!-- //지점 부문 -->
                    </div>
                </div>
            </div>

            <div class="flogo">
                <img src="/resources/img/flogo.png" alt="">
            </div>
        </div>
        <!-- //wrap -->

        <div class="w_intro_bottom w_only">
            <img src="/resources/img/flogo.png" alt="">
        </div>
    </div>

    <input type="hidden" id="tab" name="tab" value="<%= _tab %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
</asp:Content>

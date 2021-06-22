<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.UserApplication.board.evt._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page_event_detail">
        <div id="wrap" class="wrapper">
            <div class="title_wrap">
                <div class="btn_allmenu">
                    <a href="#"><img src="/resources/img/allmenu.png" alt="전체메뉴"></a>
                </div>
                <div class="page_title board_title">
                    이벤트
                </div>

                <a href="/achieve/" class="btn-home">
                    <img src="/resources/img/hub.png" alt="홈으로">
                </a>
            </div>

            <uc1:menu runat="server" ID="menu" />
            <!-- //nav -->

            <div class="event_banner">
                <div class="swiper-container swiper-event">
                    <div class="swiper-wrapper">
                        <asp:Repeater ID="rptBannerList" runat="server">
                            <ItemTemplate>
                                <div class="swiper-slide slide0<%# (Container.ItemIndex + 1) %>">
                                    <a href="<%# MLib.Util.Check.IsNone(Eval("Link").ToString()) ? "javascript:;" : Eval("Link").ToString() %>">
                                        <img src="<%# OrangeSummer.Common.User.AppSetting.AwsUrl(Eval("AttPc").ToString()) %>" alt="">
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <div class="pagination"></div>
                </div>
            </div>

            <div class="event_wrap">
                <asp:Repeater ID="rptNoticeList" runat="server">
                    <ItemTemplate>
                        <p class="top_box">
                            <a href="detail.aspx?id=<%# Eval("Id").ToString() + "&type=" + Eval("Type").ToString() %>">
                                <em>NOTICE</em>
                                <span><%# Eval("Title") %></span>
                            </a>
                        </p>
                    </ItemTemplate>
                </asp:Repeater>

                <div class="notice_area">
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <div class="content">
                                <a href="detail.aspx?id=<%# Eval("Id").ToString() + "&type=" + Eval("Type").ToString() + Parameters() %>">
                                    <span class="number"><%# ListNumber(Eval("Total"), Container.ItemIndex) %></span>

                                    <div class="info">
                                        <p class="txt"><%# Eval("Title") %></p>
                                        <span class="writer"><%# Eval("Admin.Name") %></span>
                                        <span class="view">view <%# Eval("ViewCount") %></span>
                                        <span class="data"><%# Eval("RegistDate").ToString().Substring(2, 8) %></span>
                                    </div>

                                    <div class="comment">
                                        <div class="img">
                                            <img src="/resources/img/ico_comment.png" alt="">
                                        </div>
                                        <div class="txt"><%# Eval("ReplyCount") %></div>
                                    </div>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <%= _paging %>
            </div>
        </div>
        <!-- //wrap -->

        <div class="w_intro_bottom w_only">
            <img src="/resources/img/flogo.png" alt="">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
</asp:Content>

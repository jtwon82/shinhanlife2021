﻿<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.board.notice._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />
    <link rel="stylesheet" href="/resources/css/board.css" />

    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <body>
        <div id="sub_wrap" class="subMeta05">
            <uc1:menu runat="server" ID="menu" />
            <div class="subContainer">
                <p class="subTitle">공지 & 게시판</p>
                <p class="boardTxt">모두가 우리의 썸머를 응원합니다</p>


                <asp:Repeater ID="rptNoticeList" runat="server">
                    <ItemTemplate>


                        <div class="listBox">
                            <a href="detail.aspx?id=<%# Eval("Id").ToString() + "&type=" + Eval("Type").ToString() %>">
                                <p class="title">[공지] <%# Eval("Title") %></p>
                                <%--<p class="replyNum">26</p>
                                <ul class="info">
                                    <li class="name">관리자<span>ㅣ</span></li>
                                    <li class="view">view <em>23</em><span>ㅣ</span></li>
                                    <li class="date">21-07-23</li>
                                </ul>--%>
                            </a>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>


                <div class="notice_area">
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <div class="listBox">
                                <a href="detail.aspx?id=<%# Eval("Id").ToString() + "&type=" + Eval("Type").ToString() %>">
                                    <p class="title"><span><%# ListNumber(Eval("Total"), Container.ItemIndex) %></span> <%# Eval("Title") %></p>
                                    <p class="replyNum"><%# Eval("ReplyCount") %></p>
                                    <ul class="info">
                                        <li class="name"><%# Eval("Admin.Name") %><span>ㅣ</span></li>
                                        <li class="view">view <em><%# Eval("ViewCount") %></em><span>ㅣ</span></li>
                                        <li class="date"><%# Eval("RegistDate").ToString().Substring(0, 10).Replace("-","." ) %></li>
                                    </ul>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>


                <%=_paging %>
            </div>

        </div>
    </body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>

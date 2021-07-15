<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.board.notice._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
    <link rel="stylesheet" href="/resources/css/board.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />

    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <body>
        <div id="sub_wrap" class="subMeta09">
            <uc1:menu runat="server" ID="menu" />



            <div class="subContainer">
                <p class="subTitle"><img src="/resources/img/sub/noticeTitle.png" alt="공지&게시판" /></p>
                <p class="boardTxt">모두가 우리의 썸머를 응원합니다</p>

                <div class="noticeList">
                    <asp:Repeater ID="rptNoticeList" runat="server">
                        <ItemTemplate>
                            <div class="listBox notice">
                                <a href="detail.aspx?id=<%# Eval("Id").ToString() + "&type=" + Eval("Type").ToString() %>">
                                    <p class="title">[공지] <%# Eval("Title") %></p>
                                    <p class="replyNum"><%# Eval("ReplyCount") %></p>
                                    <ul class="info">
                                        <li class="name"><%# Eval("Admin.Name") %><span>ㅣ</span></li>
                                        <li class="view">view <em><%# Eval("ViewCount") %></em><span>ㅣ</span></li>
                                        <li class="date"><%# Eval("RegistDate").ToString().Substring(2) %></li>
                                    </ul>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <div class="listBox">
                                <a href="detail.aspx?id=<%# Eval("Id").ToString() + "&type=" + Eval("Type").ToString() %>">
                                    <p class="title"><span><%# ListNumber(Eval("Total"), Container.ItemIndex) %></span> <%# Eval("Title") %></p>
                                    <p class="replyNum"><%# Eval("ReplyCount") %></p>
                                    <ul class="info">
                                        <li class="name"><%# Eval("Admin.Name") %><span>ㅣ</span></li>
                                        <li class="view">view <em><%# Eval("ViewCount") %></em><span>ㅣ</span></li>
                                        <li class="date"><%# Eval("RegistDate").ToString().Substring(2) %></li>
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

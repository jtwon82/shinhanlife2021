<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.board.evt._default" %>

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
		<div class="subContainer notice">
			<p class="subTitle">이벤트</p>
			<div class="eventPage">
				<div class="ingEventList">
					<p class="ingEventTitle">진행 중 이벤트</p>
					<div class="list">
						<dl><a href="/board/roulette">
							<dt>썸머 출석이벤트 / 2021년 07월 01일 ~ 15일까지</dt>
							<dd><img src="/resources/img/sub/event/eventImg01.png" alt=""/></dd></a>
						</dl>
					</div>
				</div>
<%--				<div class="commingEventList">
					<p class="commingEventTitle">커밍순 이벤트</p>
					<div class="list">
						<dl>
							<dt>복권 이벤트 / 2021년 08월 01일 ~ 14일까지</dt>
							<dd><img src="/resources/img/sub/event/eventImg02.png" alt=""/></dd>
						</dl>
					</div>
				</div>--%>
			</div>
		</div>

        </div>
    </body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>

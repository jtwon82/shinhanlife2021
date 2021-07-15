<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.board.evt._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />
    <link rel="stylesheet" href="/resources/css/board.css" />
    <link rel="stylesheet" href="/resources/css/swiper-bundle.css">

    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="/resources/js/swiper-bundle2.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <body>
        <div id="sub_wrap" class="subMeta08">
            <uc1:menu runat="server" ID="menu" />
		<div class="subContainer notice">
			<p class="subTitle"><img src="/resources/img/sub/eventTitle.png" alt="이벤트" /></p>
			<div class="eventPage">



                
				<!--p class="ingEventTitle">진행 중 이벤트</p-->
				<div class="list">
					<div class="event_rolling">
						<!-- Swiper -->
						<div class="swiper-container2">
							<div class="swiper-wrapper">
                        <asp:Repeater ID="rptBannerList" runat="server">
                            <ItemTemplate>
								<div class="swiper-slide">
									<a href="<%# MLib.Util.Check.IsNone(Eval("Link").ToString()) ? "javascript:;" : Eval("Link").ToString() %>">
                                        <img src="<%# OrangeSummer.Common.User.AppSetting.AwsUrl(Eval("AttMobile").ToString()) %>"/>
									</a>
								</div>
                            </ItemTemplate>
                        </asp:Repeater>
							</div>
							<!-- Add Pagination -->
							<div class="swiper-pagination2"></div>
						</div>
					</div>
				</div>
				
				<div class="event_board">
                        <asp:Repeater ID="rptList" runat="server">
                            <ItemTemplate>
					<div class="listBox"><a href="<%# Eval("Url").ToString()!=""? Eval("Url").ToString():"detail.aspx?id="+Eval("Id") +"&type="+ Eval("Type").ToString()%>">
						<p class="title"><span><%# ListNumber(Eval("Total"), Container.ItemIndex) %></span> <%# Eval("Title") %></p>
						<p class="replyNum"><%# Eval("ReplyCount") %></p>
						<ul class="info">
							<li class="name"><%# Eval("Admin.Name") %><span>ㅣ</span></li>
							<li class="view">view <em><%# Eval("ViewCount") %></em><span>ㅣ</span></li>
							<li class="date"><%# Eval("RegistDate").ToString().Substring(2, 8) %></li>
						</ul></a>
					</div>
                            </ItemTemplate>
                        </asp:Repeater>

                    <%=_paging %>

				</div>




				<div class="ingEventList" style="display:none;">
					<p class="ingEventTitle">진행 중 이벤트</p>
					<div class="list">
<%--						<dl><a href="/board/roulette">
							<dt>썸머 출석이벤트 / 2021년 07월 01일 ~ 15일까지</dt>
							<dd><img src="/resources/img/sub/event/eventImg01.png" alt=""/></dd></a>
						</dl>--%>

						<div class="event_rolling">
							<!-- Swiper -->
							<div class="swiper-container2">
								<div class="swiper-wrapper">

                                 
                        <%--<asp:Repeater ID="rptList" runat="server">
                            <ItemTemplate>
								  <div class="swiper-slide">
									<a href="<%# Eval("Url").ToString()!=""? Eval("Url").ToString():"detail.aspx?id="+Eval("Id") +"&type="+ Eval("Type").ToString()%>">
										<dl>
											<dt><%# Eval("Title") %> / <%# Eval("Sdate") %> ~ <%# Eval("Edate") %>까지</dt>
										</dl>
										<img src="<%# Eval("attImage") %>" alt="<%# Eval("Title") %>"/>
									</a></div>
                            </ItemTemplate>
                        </asp:Repeater>--%>
								</div>
								<!-- Add Pagination -->
								<div class="swiper-pagination2"></div>
							</div>
						</div>
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
						<!-- Initialize Swiper -->
							<script>
							    var swiper = new Swiper('.swiper-container2', {
							        e: '',
							        autoplay: {
							            delay: 5000,
							            disableOnInteraction: false,
							        },
								  pagination: {
									el: '.swiper-pagination2',
								  },
								});
							</script>
</asp:Content>

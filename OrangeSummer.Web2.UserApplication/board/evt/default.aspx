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
        <div id="sub_wrap" class="subMeta05">
            <uc1:menu runat="server" ID="menu" />
		<div class="subContainer notice">
			<p class="subTitle"><img src="/resources/img/sub/eventTitle.png" alt="이벤트" /></p>
			<div class="eventPage">
				<div class="ingEventList">
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

                                 
                        <asp:Repeater ID="rptList" runat="server">
                            <ItemTemplate>
								  <div class="swiper-slide">
									<a href="<%# Eval("Url").ToString()!=""? Eval("Url").ToString():"detail.aspx?id="+Eval("Id") +"&type="+ Eval("Type").ToString()%>">
										<dl>
											<dt><%# Eval("Title") %> / <%# Eval("Sdate") %> ~ <%# Eval("Edate") %>까지</dt>
										</dl>
										<img src="<%# Eval("attImage") %>" alt="<%# Eval("Title") %>"/>
									</a></div>
                            </ItemTemplate>
                        </asp:Repeater>
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
								  pagination: {
									el: '.swiper-pagination2',
								  },
								});
							</script>

							<script type="text/javascript">
								var mySwiper = new Swiper('.swiper-container2', {
								slidesPerView: 1, //슬라이드를 한번에 3개를 보여준다
								spaceBetween: 30, //슬라이드간 padding 값 30px 씩 떨어뜨려줌
								loop: false, //loop 를 true 로 할경우 무한반복 슬라이드 false 로 할경우 슬라이드의 끝에서 더보여지지 않음
								});
							</script>
</asp:Content>

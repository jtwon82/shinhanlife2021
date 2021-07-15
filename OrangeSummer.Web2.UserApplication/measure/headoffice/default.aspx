<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.measure.headoffice._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
    <link rel="stylesheet" href="/resources/css/swiper.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>

    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="/resources/js/swiper.min.4.3.5.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<body>
	<div id="sub_wrap" class="subMeta05">
            <uc1:menu runat="server" ID="menu" />
		<div class="subContainer guide">
			<p class="subTitle"><img src="/resources/img/sub/measureTitle.png" alt="시책안내" /></p>
			<div class="measure_guide">
				<ul class="bmTabs measure">
                        <li><a href="/measure/individual/">개인 부문</a></li>
                        <li><a href="/measure/sl/">E SL 부문</a></li>
                        <li><a href="/measure/point/">지점 부문</a></li>
					<li><a href="/measure/headoffice/" class="current">본부 부문</a></li>
				</ul>
				
				<!-- 지점부문 -->
				<div class="titleBox">
					<dl>
						<dt>본부 부문 I</dt>
						<dd>썸머 시책 안내</dd>
					</dl>
					<!--ul>
						<li>관리자<span>ㅣ</span></li>
						<li>view 43<span>ㅣ</span></li>
						<li>21-07-11</li>
					</ul-->
				</div>
				<div class="evaluationInfo">
					<dl>
						<dt>평가 대상<span>|</span></dt>
						<dd>본부</dd>
					</dl>
					<dl>
						<dt>평가 기준<span>|</span></dt>
						<dd>상위 달성 GSL+SSL 인원 수</dd>
					</dl>
					<dl>
						<dt>특&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 징<span>|</span></dt>
						<dd>본부 티켓 지급</dd>
					</dl>
					<dl>
						<dt>필수 기준<span>|</span></dt>
						<dd>(7+8월) 합산BP 100% 달성</dd>
					</dl>
				</div>
				<div class="measureCon sl first">
					<p class="Con_t">본부 여행 티켓</p>
					<p class="bpTxt">G SL / S SL 상위자 인당 1개 지급</p>
				</div>
				
				<div class="measureCon first headoffice">
					<p class="Con_t">G SL부문</p>
					<div class="Con">
						<dl>
							<dt>업적 기준</dt>
							<dd>본인+피도입자<br/>업적</dd>
						</dl>
						<dl>
							<dt>최저 기준</dt>
							<dd>환산 CMIP <br/><em>500</em>만</dd>
						</dl>
						<dl>
							<dt>순위</dt>
							<dd>상위 <em>30</em> 위</dd>
						</dl>
					</div>
				</div>

				<div class="measureCon headoffice">
					<p class="Con_t">S SL부문</p>
					<div class="Con">
						<dl>
							<dt>업적 기준</dt>
							<dd>본인+피도입자<br/>업적</dd>
						</dl>
						<dl>
							<dt>최저 기준</dt>
							<dd>환산 CMIP <br/><em>750</em>만</dd>
						</dl>
						<dl>
							<dt>순위</dt>
							<dd>상위 <em>10</em> 위</dd>
						</dl>
					</div>
				</div>
				
			</div>
		</div>
				
	</div>
</body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>

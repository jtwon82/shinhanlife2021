<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.measure.sl._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />
    <link rel="stylesheet" href="/resources/css/swiper.css" />

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
                        <li><a href="/measure/sl/" class="current">E SL 부문</a></li>
                        <li><a href="/measure/point" >지점 부문</a></li>
                    </ul>
                    
				<!-- SL부문 -->
				<div class="titleBox">
					<dl>
						<dt>SL부문 I</dt>
						<dd>썸머 시책 안내</dd>
					</dl>
					<!--ul>
						<li>관리자<span>ㅣ</span></li>
						<li>view 43<span>ㅣ</span></li>
						<li>21-07-11</li>
					</ul-->
				</div>
				<div class="measureCon sl first">
					<p class="Con_t">E SL부문</p>
					<div class="Con">
						<dl>
							<dt>선발 순위</dt>
							<dd>상위 <em>1-10</em> 위</dd>
						</dl>
						
						<dl>
							<dt>시상 포인트</dt>
							<dd> <em>250</em> 만</dd>
						</dl>
					</div>
					<dl class="achievement_standards">
						<dt>업적순위<span>|</span></dt>
						<dd>피도입자 합산 <span>캠페인환산</span> <em>CMIP 200백만↑</em></dd>
					</dl>
				</div>

				<div class="measureCon sl">
					<div class="Con">
						<dl>
							<dt>선발 순위</dt>
							<dd>상위 <em>11-80</em> 위</dd>
						</dl>
						<dl>
							<dt>시상 포인트</dt>
							<dd> <em>250</em> 만</dd>
						</dl>
					</div>
					<dl class="achievement_standards">
						<dt>업적순위<span>|</span></dt>
						<dd>피도입자 합산 <span>캠페인환산</span> <em>CMIP 7백만↑</em></dd>
					</dl>
				</div>

				<dl class="evaluation">
					<dt>평가기준</dt>
					<dd><strong>순수 피도입자업적</strong> 합산 캠페인 환산 CMIP</dd>
				</dl>

				<dl class="necessaryGuidance">
					<dt>필수 기준</dt>
					<dd>피도입자 개인부문 <strong>달성인원 2명↑</strong> <span>(도입자 본인달성인정)</span><br/>
					본인 <strong>개인부문 필수 기준 달성</strong><br/><span>(7월&8월 각 월별 캠페인 환산 CMIP 30만↑)</span></dd>
				</dl>
			</div>
		</div>
				
	</div>
</body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>

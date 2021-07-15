<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.measure.individual._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />

    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <!-- script type="text/javascript" src="/resources/js/swiper.min.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></!-->
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
                        <li><a href="/measure/individual/" class="current">개인 부문</a></li>
                        <li><a href="/measure/sl/">SL 부문</a></li>
                        <li><a href="/measure/point/">지점 부문</a></li>
                    </ul>
				<!-- 개인부문 -->
				<div class="titleBox">
					<dl>
						<dt>개인부문 I</dt>
						<dd>썸머 시책 안내</dd>
					</dl>
				</div>
				<div class="evaluationInfo">
					<dl>
						<dt>평가 대상<span>|</span></dt>
						<dd>FC, G SL, S SL, E SL</dd>
					</dl>
					<dl>
						<dt>평가 기준<span>|</span></dt>
						<dd>시책 기간 내 환산CMIP</dd>
					</dl>
					<dl>
						<dt>선발 인원<span>|</span></dt>
						<dd>상위 800명</dd>
					</dl>
				</div>
				<div class="mandatory_standards">
					<dl>
						<dt>필수기준 01</dt>
						<dd><strong>(7~8월) 월별 최소 기준</strong>각 월 환산CMIP 30만 ↑<br/>or 원 CANP 150만</dd>
					</dl>
					<dl class="two">
						<dt>필수기준 02</dt>
						<dd><strong>(7~8월) 합산 기준</strong>원 보장CANP 400만 ↑</dd>
					</dl>
				</div>
				<!--ul>
					<li>관리자<span>ㅣ</span></li>
					<li>view 43<span>ㅣ</span></li>
					<li>21-07-11</li>
				</ul-->
				<div class="measureCon first">
					<p class="Con_t">TRIPLE</p>
					<div class="Con">
						<dl>
							<dt>순위</dt>
							<dd><em>1-30</em> 위<br/>(30명)</dd>
						</dl>
						<dl>
							<dt>최소 기준</dt>
							<dd>환산 CMIP <br/><em>750</em>만 &amp; <em>5</em>건 <em>↑</em></dd>
						</dl>
						<dl>
							<dt>① Trip</dt>
							<dd class="trip_area">사이판 <em class="jeju">+ 제주</em></dd>
						</dl>
						<dl>
							<dt>② 현금</dt>
							<dd><em>100만</em></dd>
						</dl>
					</div>
				</div>

				<div class="measureCon">
					<p class="Con_t">DOUBLE</p>
					<div class="Con">
						<dl>
							<dt>순위</dt>
							<dd><em>31-300</em> 위<br/>(270명)</dd>
						</dl>
						<dl>
							<dt>업적 기준</dt>
							<dd>환산 CMIP <br/><em>500</em>만 &amp; <em>5</em>건 <em>↑</em></dd>
						</dl>
						<dl>
							<dt>① Trip</dt>
							<dd class="trip_area">사이판 <em class="jeju">+ 제주</em></dd>
						</dl>
						<dl>
							<dt>② 현금</dt>
							<dd class="blank">-</dd>
						</dl>
					</div>
				</div>

				<div class="measureCon last">
					<p class="Con_t">일반</p>
					<div class="Con">
						<dl>
							<dt>순위</dt>
							<dd><em>301-800</em> 위<br/>(500명)</dd>
						</dl>
						<dl>
							<dt>업적 기준</dt>
							<dd>환산 CMIP <br/><em>250</em>만 &amp; <em>5</em>건 <em>↑</em></dd>
						</dl>
						<dl>
							<dt>① Trip</dt>
							<dd class="trip_area one">사이판</dd>
						</dl>
						<dl>
							<dt>② 현금</dt>
							<dd class="blank">-</dd>
						</dl>
					</div>
				</div>

				<!--dl class="measureGuidance">
					<dt>썸머 시책 안내</dt>
					<dd>7월 & 8월 각 월별 캠페인 환산 <strong>CMIP 30만↑</strong></dd>
				</dl-->
				<dl class="achievement_standards">
					<dt>신인 부문(8월 위촉자)<span>|</span></dt>
					<dd>상위 30명 선발 <span>환산</span> <em>200만 &amp; 4건 ↑</em></dd>
				</dl>

				<div class="commissionerBox">
					<p>개인부문 놀라운 SPECIAL CANP!</p>
					<ul>
						<li>원CANP 2,200만 이상 시<br/>
						사이판 무조건 확정<span>* SPECIAL CANP 부문 : 절대 기준 (순위 평가X)</span></li>
					</ul>
				</div>
                    
				<dl class="product_price">
					<dt>상품가중치  <span>|</span></dt>
					<dd>추후 안내</dd>
					
				</dl>
			</div>
		</div>
				
	</div>
</body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>

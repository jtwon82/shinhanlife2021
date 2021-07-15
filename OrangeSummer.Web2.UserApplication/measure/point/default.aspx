<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.measure.point._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"" />
    <link rel="stylesheet" href="/resources/css/swiper.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>""/>

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
                        <li><a href="/measure/sl/">SL 부문</a></li>
                        <li><a href="/measure/point/" class="current">지점 부문</a></li>
                    </ul>
                    
				<!-- 지점부문 -->
				<div class="titleBox">
					<dl>
						<dt>지점 부문 I</dt>
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
						<dd>지점, 사업단, 사업본부</dd>
					</dl>
					<dl>
						<dt>평가 기준<span>|</span></dt>
						<dd>지점 합산 환산CMIP 순위</dd>
					</dl>
					<dl>
						<dt>특징&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1<span>|</span></dt>
						<dd>BP부문 &amp; 순위 부문 중복 도전 가능</dd>
					</dl>
					<dl>
						<dt>특징&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2<span>|</span></dt>
						<dd>지점BP 달성 시 바로 달성 확정</dd>
					</dl>
				</div>
				<div class="measureCon sl first">
					<p class="Con_t">BP부문</p>
					<p class="bpTxt">7+8월 합산BP 100% 달성 시, <strong>사이판 여행</strong></p>
				</div>
				
				<div class="measureCon first esl point">
					<p class="Con_t">순위부문</p>
					<div class="Con">
						<dl>
							<dt>구분</dt>
							<dd>상위</dd>
						</dl>
						<dl>
							<dt>순위</dt>
							<dd><em>1-10</em> 위<br/>(10명)</dd>
						</dl>
						<dl>
							<dt>최소 기준</dt>
							<dd>환산 CMIP <br/><em>8,000</em>만<em>↑</em></dd>
						</dl>
						<dl>
							<dt>보상</dt>
							<dd class="trip_area">사이판 <em class="jeju">+ 제주</em></dd>
						</dl>
					</div>
				</div>

				<div class="measureCon esl point">
					<div class="Con">
						<dl>
							<dt>구분</dt>
							<dd>일반</dd>
						</dl>
						<dl>
							<dt>순위</dt>
							<dd><em>11-40</em> 위<br/>(30명)</dd>
						</dl>
						<dl>
							<dt>최소 기준</dt>
							<dd>환산 CMIP <br/><em>4,000</em>만<em>↑</em></dd>
						</dl>
						<dl>
							<dt>보상</dt>
							<dd class="trip_area one">사이판</dd>
						</dl>
					</div>
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

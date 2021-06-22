<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.measure.point._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />
    <link rel="stylesheet" href="/resources/css/swiper.css"/>

    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="/resources/js/swiper.min.4.3.5.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <body>
        <div id="sub_wrap" class="subMeta05">
            <uc1:menu runat="server" ID="menu" />
            <div class="subContainer guide">
                <p class="subTitle">시책 안내</p>
                <div class="measure_guide">
                    <ul class="bmTabs measure">
                        <li><a href="/measure/individual/">개인 부문</a></li>
                        <li><a href="/measure/sl/">SL 부문</a></li>
                        <li><a href="/measure/point" class="current">지점 부문</a></li>
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
				<div class="measureCon sl first">
					<p class="Con_t">BP부문</p>
					<p class="bpTxt">7~8월 지점 합산 BP 달성시, <strong>전원 150만 포인트 지급</strong></p>
				</div>
				<div class="measureCon sl">
					<p class="Con_t">순위부문</p>
					<div class="Con">
						<dl>
							<dt>선발 순위</dt>
							<dd>상위 <em>1-10</em> 위</dd>
						</dl>
						
						<dl>
							<dt>시상 포인트</dt>
							<dd> <em>550</em> 만</dd>
						</dl>
					</div>
					<dl class="achievement_standards">
						<dt>업적순위<span>|</span></dt>
						<dd>피도입자 합산 <span>캠페인환산</span> <em>CMIP 100백만↑</em></dd>
					</dl>
				</div>

				<div class="measureCon sl">
					<div class="Con">
						<dl>
							<dt>선발 순위</dt>
							<dd>상위 <em>11-20</em> 위</dd>
						</dl>
						<dl>
							<dt>시상 포인트</dt>
							<dd> <em>250</em> 만</dd>
						</dl>
					</div>
					<dl class="achievement_standards">
						<dt>업적순위<span>|</span></dt>
						<dd>피도입자 합산 <span>캠페인환산</span> <em>CMIP 50백만↑</em></dd>
					</dl>
				</div>

				<div class="measureCon sl">
					<div class="Con">
						<dl>
							<dt>선발 순위</dt>
							<dd>상위 <em>21-30</em> 위</dd>
						</dl>
						<dl>
							<dt>시상 포인트</dt>
							<dd> <em>150</em> 만</dd>
						</dl>
					</div>
					<dl class="achievement_standards">
						<dt>업적순위<span>|</span></dt>
						<dd>피도입자 합산 <span>캠페인환산</span> <em>CMIP 50백만↑</em></dd>
					</dl>
				</div>

				<dl class="evaluation point">
					<dt>평가기준</dt>
					<dd><strong>지점 합산</strong> 캠페인 환산 CMIP 순위</dd>
				</dl>

				<dl class="point_bonus">
					<dt>우수 업적 달성 추가 BONUS</dt>
					<dd><strong>지점 합산 캠페인</strong> 환산 <strong>CMIP 20,000만원</strong> 달성 시,<br/>
					<strong>1,000만 POINT 추가 지급</strong></dd>
				</dl>

				<dl class="necessaryGuidance">
					<dt>필수 기준</dt>
					<dd>지점 내 개인부 <strong>달성인원 5명↑</strong></dd>
				</dl>
				
			</div>
		</div>
				
	</div>
</body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>

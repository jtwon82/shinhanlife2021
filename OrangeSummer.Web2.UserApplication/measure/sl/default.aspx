<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.measure.sl._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"" />
    <link rel="stylesheet" href="/resources/css/swiper.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"" />

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
                        <li><a href="/measure/sl/" class="current">SL 부문</a></li>
                        <li><a href="/measure/point" >지점 부문</a></li>
                    </ul>
                    
				<!-- E SL부문 -->
				<div class="titleBox">
					<p class="titleBox_t first">E SL부문</p>
					<!--dl>
						<dt>SL부문 I</dt>
						<dd>썸머 시책 안내</dd>
					</dl>
					<ul>
						<li>관리자<span>ㅣ</span></li>
						<li>view 43<span>ㅣ</span></li>
						<li>21-07-11</li>
					</ul-->
				</div>
				<div class="evaluationInfo">
					<dl>
						<dt>평가 대상<span class="sl_blank">|</span></dt>
						<dd>E SL</dd>
					</dl>
					<dl>
						<dt>평가 기준<span class="sl_blank">|</span></dt>
						<dd>팀 합산 업적 평가 ( E SL업적 포함) 순위</dd>
					</dl>
					<dl>
						<dt>선발 인원<span class="sl_blank">|</span></dt>
						<dd>상위 100명</dd>
					</dl>
					<dl>
						<dt>놀라운특징<span>|</span></dt>
						<dd>‘개인 부문’ & ‘E SL 부문’ 중복 달성 가능</dd>
					</dl>
				</div>
				<div class="measureCon first esl">
					<div class="Con">
						<dl>
							<dt>순위</dt>
							<dd><em>1-20</em> 위</dd>
						</dl>
						<dl>
							<dt>최소 기준</dt>
							<dd>환산 CMIP <br/><em>2,000</em>만 <em>↑</em></dd>
						</dl>
						<dl>
							<dt>보상</dt>
							<dd class="trip_area">사이판 <em class="jeju">+ 제주</em></dd>
						</dl>
						<dl>
							<dt>기타</dt>
							<dd class="small">개인부문<br/>
							중복 달성 시,<br/>
							100만원 또는<br/>
							팀 산하 1명 Trip</dd>
						</dl>
					</div>
				</div>

				<div class="measureCon esl">
					<div class="Con">
						<dl>
							<dt>순위</dt>
							<dd><em>21-100</em> 위</dd>
						</dl>
						<dl>
							<dt>최소 기준</dt>
							<dd>환산 CMIP <br/><em>1,000</em>만 <em>↑</em></dd>
						</dl>
						<dl>
							<dt>보상</dt>
							<dd class="trip_area one">사이판</dd>
						</dl>
						<dl>
							<dt>기타</dt>
							<dd class="small">개인부문<br/>
							중복 달성 시,<br/>
							100만원 또는<br/>
							팀 산하 1명 Trip</dd>
						</dl>
					</div>
				</div>

				<dl class="necessaryGuidance">
					<dt>필수 기준</dt>
					<dd>개인부문 달성자 2명 이상 (E SL 본인 포함)</dd>
				</dl>


				<!-- S SL부문 -->
				<div class="titleBox">
					<p class="titleBox_t">S SL부문</p>
					<!--dl>
						<dt>SL부문 I</dt>
						<dd>썸머 시책 안내</dd>
					</dl>
					<ul>
						<li>관리자<span>ㅣ</span></li>
						<li>view 43<span>ㅣ</span></li>
						<li>21-07-11</li>
					</ul-->
				</div>
				<div class="evaluationInfo">
					<dl>
						<dt>평가 대상<span class="sl_blank">|</span></dt>
						<dd>S SL</dd>
					</dl>
					<dl>
						<dt>평가 기준<span class="sl_blank">|</span></dt>
						<dd>피도입자 합산 업적 평가 (SL업적 포함) 순위</dd>
					</dl>
					<dl>
						<dt>선발 인원<span class="sl_blank">|</span></dt>
						<dd>S SL상위 10명</dd>
					</dl>
					<dl>
						<dt>놀라운특징<span>|</span></dt>
						<dd>‘개인 부문’ & ‘S SL 부문’ 중복 달성 가능</dd>
					</dl>
				</div>
				<div class="measureCon first esl">
					<div class="Con">
						<dl>
							<dt>순위</dt>
							<dd><em>1-10</em> 위</dd>
						</dl>
						<dl>
							<dt>최소 기준</dt>
							<dd>환산 CMIP <br/><em>750</em>만 <em>↑</em></dd>
						</dl>
						<dl>
							<dt>보상</dt>
							<dd class="trip_area one">사이판</dd>
						</dl>
						<dl>
							<dt>기타</dt>
							<dd class="small">개인부문<br/>
							중복 달성 시,<br/>
							100만원 또는<br/>
							팀 산하 1명 Trip</dd>
						</dl>
					</div>
				</div>

				<dl class="necessaryGuidance">
					<dt>필수 기준</dt>
					<dd>해당 본부 합산 BP 100% 달성</dd>
				</dl>

				<!-- G SL부문 -->
				<div class="titleBox">
					<p class="titleBox_t">G SL부문</p>
					<!--dl>
						<dt>SL부문 I</dt>
						<dd>썸머 시책 안내</dd>
					</dl>
					<ul>
						<li>관리자<span>ㅣ</span></li>
						<li>view 43<span>ㅣ</span></li>
						<li>21-07-11</li>
					</ul-->
				</div>
				<div class="evaluationInfo">
					<dl>
						<dt>평가 대상<span class="sl_blank">|</span></dt>
						<dd>G SL</dd>
					</dl>
					<dl>
						<dt>평가 기준<span class="sl_blank">|</span></dt>
						<dd>피도입자 합산 업적 평가 (SL업적 포함) 순위</dd>
					</dl>
					<dl>
						<dt>선발 인원<span class="sl_blank">|</span></dt>
						<dd>S SL 상위 30명</dd>
					</dl>
					<dl>
						<dt>놀라운특징<span>|</span></dt>
						<dd>‘개인 부문’ & ‘S SL 부문’ 중복 달성 가능</dd>
					</dl>
				</div>
				<div class="measureCon first esl">
					<div class="Con">
						<dl>
							<dt>순위</dt>
							<dd><em>1-30</em> 위</dd>
						</dl>
						<dl>
							<dt>최소 기준</dt>
							<dd>환산 CMIP <br/><em>500</em>만 <em>↑</em></dd>
						</dl>
						<dl>
							<dt>보상</dt>
							<dd class="trip_area one">사이판</dd>
						</dl>
						<dl>
							<dt>기타</dt>
							<dd class="small">개인부문<br/>
							중복 달성 시,<br/>
							100만원 또는<br/>
							팀 산하 1명 Trip</dd>
						</dl>
					</div>
				</div>

				<dl class="necessaryGuidance">
					<dt>필수 기준</dt>
					<dd>해당 본부 합산 BP 100% 달성</dd>
				</dl>

				<!--dl class="product_price esl">
					<dt>상품가중치  <span>|</span></dt>
					<dd>추후안내</dd>
				</dl-->
				
			</div>
		</div>
				
	</div>
</body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>

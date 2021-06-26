<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.measure.individual._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />
    <link rel="stylesheet" href="/resources/css/swiper.css" />

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
                        <li><a href="/measure/sl/">E SL 부문</a></li>
                        <li><a href="/measure/point">지점 부문</a></li>
                    </ul>
                    <!-- 개인부문 -->
                    <div class="titleBox">
                        <dl>
                            <dt>개인부문 I</dt>
                            <dd>썸머 시책 안내</dd>
                        </dl>
                        <!--ul>
						<li>관리자<span>ㅣ</span></li>
						<li>view 43<span>ㅣ</span></li>
						<li>21-07-11</li>
					</ul-->
                    </div>
                    <div class="measureCon first">
                        <p class="Con_t">TRIPLE</p>
                        <div class="Con">
                            <dl>
                                <dt>선발 순위</dt>
                                <dd>상위 <em>30</em> 위</dd>
                            </dl>
                            <dl>
                                <dt>업적 기준</dt>
                                <dd>환산 CMIP
                                    <br />
                                    <em>800</em>만 &amp; <em>800</em>건 <em>↑</em></dd>
                            </dl>
                            <dl>
                                <dt>시상 포인트</dt>
                                <dd><em>400</em> 만</dd>
                            </dl>
                        </div>
                    </div>

                    <div class="measureCon">
                        <p class="Con_t">DOUBLE</p>
                        <div class="Con">
                            <dl>
                                <dt>선발 순위</dt>
                                <dd>상위 <em>130</em> 위</dd>
                            </dl>
                            <dl>
                                <dt>업적 기준</dt>
                                <dd>환산 CMIP
                                    <br />
                                    <em>500</em>만 &amp; <em>5</em>건 <em>↑</em></dd>
                            </dl>
                            <dl>
                                <dt>시상 포인트</dt>
                                <dd><em>250</em> 만</dd>
                            </dl>
                        </div>
                    </div>

                    <div class="measureCon last">
                        <p class="Con_t">일반</p>
                        <div class="Con">
                            <dl>
                                <dt>선발 순위</dt>
                                <dd>상위 <em>800</em> 위</dd>
                            </dl>
                            <dl>
                                <dt>업적 기준</dt>
                                <dd>환산 CMIP
                                    <br />
                                    <em>270</em>만 &amp; <em>8</em>건 <em>↑</em></dd>
                            </dl>
                            <dl>
                                <dt>시상 포인트</dt>
                                <dd><em>150</em> 만</dd>
                            </dl>
                        </div>
                    </div>

                    <dl class="measureGuidance">
                        <dt>썸머 시책 안내</dt>
                        <dd>7월 & 8월 각 월별 캠페인 환산 <strong>CMIP 30만↑</strong></dd>
                    </dl>

                    <div class="commissionerBox">
                        <p>신인 FC부문 (8월 위촉자)</p>
                        <ul>
                            <li>캠페인 환산 CMIP <span>상위 30명 선발</span></li>
                            <li>캠페인 환산 CMIP <span>230만↑& 5건↑</span></li>
                        </ul>
                    </div>
                </div>
            </div>

        </div>

    </body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>

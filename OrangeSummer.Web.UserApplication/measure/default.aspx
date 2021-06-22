<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.UserApplication.measure._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sub_page page_measure">
        <div id="wrap" class="wrapper">
            <div class="title_wrap">
                <div class="btn_allmenu">
                    <a href="#">
                        <img src="/resources/img/allmenu.png" alt="전체메뉴"></a>
                </div>
                <div class="page_title">
                    시책안내
                </div>

                <a href="/achieve/" class="btn-home">
                    <img src="/resources/img/hub.png" alt="홈으로">
                </a>
            </div>

            <uc1:menu runat="server" ID="menu" />
            <!-- //nav -->

            <div class="measure_wrap">
                <div class="swiper-container tab_list">
                    <div class="swiper-wrapper">
                        <div class="swiper-slide"><a href="javascript:;">개인 부문</a></div>
                        <div class="swiper-slide"><a href="javascript:;">SL 부문</a></div>
                        <!-- //200629 수정 -->
                        <div class="swiper-slide"><a href="javascript:;">지점 부문</a></div>
                    </div>
                </div>

                <!-- 200702 수정 -->
                <div class="swiper-container tab_cont_wrap">
                    <div class="swiper-wrapper">
                        <!-- 개인 부문 -->
                        <div class="swiper-slide tab_content01">
                            <img src="/resources/img/img_measure01.png?v=20200729-1" alt="">

                            <div class="hidden">
                                <div class="item_box">
                                    <p class="title">TRIPLE</p>
                                    <div class="list">
                                        <dl>
                                            <dt>선발 순위</dt>
                                            <dd>
                                                <div>
                                                    상위<br>
                                                    <strong>30</strong>위
                                                </div>
                                            </dd>
                                        </dl>
                                        <dl>
                                            <dt>업적 기준</dt>
                                            <dd class="fnt-s">
                                                <div>
                                                    환산 CMIP<br>
                                                    <strong>800</strong> 만 &amp;<br>
                                                    <strong>5</strong>건 <strong>↑</strong>
                                                </div>
                                            </dd>
                                        </dl>
                                        <dl>
                                            <dt>시상 포인트</dt>
                                            <dd>
                                                <div>
                                                    <strong>400</strong> 만
                                                </div>
                                            </dd>
                                        </dl>
                                    </div>
                                </div>
                                <!-- //item_box -->

                                <div class="item_box">
                                    <p class="title">DOUBLE</p>
                                    <div class="list">
                                        <dl>
                                            <dt>선발 순위</dt>
                                            <dd>
                                                <div>
                                                    상위<br>
                                                    <strong>130</strong>위
                                                </div>
                                            </dd>
                                        </dl>
                                        <dl>
                                            <dt>업적 기준</dt>
                                            <dd class="fnt-s">
                                                <div>
                                                    환산 CMIP<br>
                                                    <strong>500</strong> 만 &amp;<br>
                                                    <strong>5</strong>건 <strong>↑</strong>
                                                </div>
                                            </dd>
                                        </dl>
                                        <dl>
                                            <dt>시상 포인트</dt>
                                            <dd>
                                                <div>
                                                    <strong>250</strong> 만
                                                </div>
                                            </dd>
                                        </dl>
                                    </div>
                                </div>
                                <!-- //item_box -->

                                <div class="item_box">
                                    <p class="title">일반</p>
                                    <div class="list">
                                        <dl>
                                            <dt>선발 순위</dt>
                                            <dd>
                                                <div>
                                                    상위<br>
                                                    <strong>800</strong>위
                                                </div>
                                            </dd>
                                        </dl>
                                        <dl>
                                            <dt>업적 기준</dt>
                                            <dd class="fnt-s">
                                                <div>
                                                    환산 CMIP<br>
                                                    <strong>270</strong> 만 &amp;<br>
                                                    <strong>8</strong>건 <strong>↑</strong>
                                                </div>
                                            </dd>
                                        </dl>
                                        <dl>
                                            <dt>시상 포인트</dt>
                                            <dd>
                                                <div>
                                                    <strong>150</strong> 만
                                                </div>
                                            </dd>
                                        </dl>
                                    </div>

                                    <p class="desc">
                                        <span>필수 기준</span>
                                        7월 &amp; 8월 각 월별 캠페인 환산 <strong>CMIP 30만↑</strong>
                                    </p>
                                </div>
                                <!-- //item_box -->

                                <div class="refer_box">
                                    <p class="title">신인 FC 부문 (8월 위촉자)</p>
                                    <ul>
                                        <li>캠페인 환산 CMIP <span>상위 30명 선발</span></li>
                                        <li>캠페인 환산 CMIP <span>230만 ↑ &amp; 5건 ↑</span></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!-- //개인 부문 -->

                        <!-- SL 부문 -->
                        <div class="swiper-slide tab_content02">
                            <img src="/resources/img/img_measure02.png" alt="">

                            <div class="hidden">
                                <div class="item_box">
                                    <p class="title">SL 부문</p>
                                    <div class="list">
                                        <div>
                                            <dl>
                                                <dt>선발 순위</dt>
                                                <dd>
                                                    <div>
                                                        상위 <strong>1 - 10</strong> 위
                                                    </div>
                                                </dd>
                                            </dl>
                                            <dl>
                                                <dt>시상 포인트</dt>
                                                <dd>
                                                    <div>
                                                        <strong>250</strong> 포인트
                                                    </div>
                                                </dd>
                                            </dl>
                                            <div class="sub-desc">
                                                <span class="sub-title">업적기준&nbsp;&nbsp;|&nbsp;</span>
                                                <span class="sub-txt"><strong>피도입자 합산</strong> 캠페인환산 <strong class="fnt-c">CMIP 20백만↑</strong></span>
                                            </div>
                                        </div>
                                        <div>
                                            <dl>
                                                <dt>선발 순위</dt>
                                                <dd>
                                                    <div>
                                                        상위 <strong>11 - 80</strong> 위
                                                    </div>
                                                </dd>
                                            </dl>
                                            <dl>
                                                <dt>시상 포인트</dt>
                                                <dd>
                                                    <div>
                                                        <strong>150</strong> 포인트
                                                    </div>
                                                </dd>
                                            </dl>
                                            <div class="sub-desc">
                                                <span class="sub-title">업적기준&nbsp;&nbsp;|&nbsp;</span>
                                                <span class="sub-txt"><strong>피도입자 합산</strong> 캠페인환산 <strong class="fnt-c">CMIP 7백만↑</strong></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="noti-box">
                                        <span class="title">평가기준</span>
                                        <span class="txt"><em>순수 직도입자 업적 </em>합산 캠페인 환산 CMIP 순위</span>
                                    </div>

                                    <div class="desc-box">
                                        <span>필수 기준</span>
                                        <p class="txt">
                                            <em>피도입자 개인부문 <strong>달성 인원 2명 ↑</strong></em> (도입자 본인 달성 인정)<br>
                                            <em>본인 <strong>개인부문 필수 기준 달성</strong></em><br>
                                            (7월 &amp; 8월 각 월별 캠페인 환산 CMIP 30만 ↑) 
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <!-- //item_box -->
                        </div>
                        <!-- //SL 부문 -->

                        <!-- 지점 부문 -->
                        <div class="swiper-slide tab_content03">
                            <img src="/resources/img/img_measure03.png" alt="">

                            <div class="hidden">
                                <div class="item_box">
                                    <p class="title">BP 부문 <span>7~8월 합산 지점 BP 달성 시,<span class="fnt-c">150만 포인트 지급</span></span></p>
                                    <p class="title">순위 부문</p>
                                    <div class="list">
                                        <div>
                                            <dl>
                                                <dt>선발 순위</dt>
                                                <dd>
                                                    <div>
                                                        <strong>1 - 10</strong> 위
                                                    </div>
                                                </dd>
                                            </dl>
                                            <dl>
                                                <dt>시상 포인트</dt>
                                                <dd>
                                                    <div>
                                                        <strong>500</strong> 포인트
                                                    </div>
                                                </dd>
                                            </dl>
                                            <div class="sub-desc">
                                                <span class="sub-title">최소달성업적&nbsp;&nbsp;|&nbsp;</span>
                                                <span class="sub-txt"><strong>지점 합산</strong> 캠페인환산 <strong class="fnt-c">CMIP 100백만↑</strong></span>
                                            </div>
                                        </div>
                                        <div>
                                            <dl>
                                                <dt>선발 순위</dt>
                                                <dd>
                                                    <div>
                                                        <strong>11 - 20</strong> 위
                                                    </div>
                                                </dd>
                                            </dl>
                                            <dl>
                                                <dt>시상 포인트</dt>
                                                <dd>
                                                    <div>
                                                        <strong>250</strong> 포인트
                                                    </div>
                                                </dd>
                                            </dl>
                                            <div class="sub-desc">
                                                <span class="sub-title">최소달성업적&nbsp;&nbsp;|&nbsp;</span>
                                                <span class="sub-txt"><strong>지점 합산</strong> 캠페인환산 <strong class="fnt-c">CMIP 50백만↑</strong></span>
                                            </div>
                                        </div>
                                        <div>
                                            <dl>
                                                <dt>선발 순위</dt>
                                                <dd>
                                                    <div>
                                                        <strong>21 - 30</strong> 위
                                                    </div>
                                                </dd>
                                            </dl>
                                            <dl>
                                                <dt>시상 포인트</dt>
                                                <dd>
                                                    <div>
                                                        <strong>150</strong> 포인트
                                                    </div>
                                                </dd>
                                            </dl>
                                            <div class="sub-desc">
                                                <span class="sub-title">최소달성업적&nbsp;&nbsp;|&nbsp;</span>
                                                <span class="sub-txt"><strong>지점 합산</strong> 캠페인환산 <strong class="fnt-c">CMIP 50백만↑</strong></span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="noti-box">
                                        <span class="title">평가기준</span>
                                        <span class="txt"><em>지점 합산 </em>캠페인환산 CMIP 순위</span>
                                    </div>

                                    <div class="noti-box">
                                        <span class="title">우수 업적 달성 추가 Bonus</span>
                                        <span class="txt">지점 합산 캠페인 환산 CMIP 20,000만원 달성 시, 1,000만 Point 추가 지급</span>
                                    </div>

                                    <div class="desc-box">
                                        <span>필수 기준</span>
                                        <p class="txt">
                                            <em>지점 내 개인부문 <strong>달성 인원 5명 ↑</strong></em>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <!-- //item_box -->
                        </div>
                        <!-- //지점 부문 -->
                    </div>
                </div>
                <!-- //200702 수정 -->

            </div>

            <div class="flogo">
                <img src="/resources/img/flogo.png" alt="">
            </div>
        </div>
        <!-- //wrap -->

        <div class="w_intro_bottom w_only">
            <img src="/resources/img/flogo.png" alt="">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
</asp:Content>

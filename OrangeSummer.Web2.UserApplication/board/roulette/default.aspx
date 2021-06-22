<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.board.roulette._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />
    <link rel="stylesheet" href="/resources/css/board.css" />
    <script type="text/javascript" src="/resources/js/common.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <body>
        <div id="sub_wrap" class="subMeta06" style="height:1789px;">
            <uc1:menu runat="server" ID="menu" />
            <div class="subContainer">
                <div class="eventPage">
                    <div class="event_kv event_roulette">
                        <div class="roulette_box">
                            <div class="roulette obj">
                                <img src="/resources/img/sub/event/event_kv02_coupon1.png" alt="꽝"/>
                                <img src="/resources/img/sub/event/event_kv02_coupon2.png" alt="당첨"/>
                                <img src="/resources/img/sub/event/event_kv02_coupon1.png" alt="꽝"/>
                                <img src="/resources/img/sub/event/event_kv02_coupon2.png" alt="당첨"/>
                                <img src="/resources/img/sub/event/event_kv02_coupon1.png" alt="꽝"/>
                                <img src="/resources/img/sub/event/event_kv02_coupon2.png" alt="당첨"/>
                            </div>

                            <div class="item">
                                <img src="/resources/img/sub/event/event_kv02_img.png" alt=""/>
                            </div>

                            <a href="javascript:;" class="btn_roulette">
                                <img src="/resources/img/sub/event/event_kv02_btn.png" alt="룰렛 돌리기"/>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- popup -->
        <div class="popup_wrap popup_winning">
            <div class="popup_inner">
                <div class="popup_container">
                    <div class="popup_title">
                        <p class="main_txt">당첨되셨습니다!</p>
                        <p class="sub_txt">
                            축하드립니다.
                            <br>
                            <span>커피쿠폰</span>에 당첨되셨습니다.
                        </p>
                    </div>
                </div>
                <button class="popup_close">
                    <img src="/resources/img/sub/event/btn_close.png" alt="닫기">
                </button>
            </div>
        </div>

        <div class="popup_wrap popup_fail">
            <div class="popup_inner">
                <div class="popup_container">
                    <div class="popup_title">
                        <p class="main_txt">꽝</p>
                        <p class="sub_txt">다음 기회에 도전하세요.</p>
                    </div>

                    <button class="popup_close">
                        <img src="/resources/img/sub/event/btn_close.png" alt="닫기">
                    </button>
                </div>
            </div>
            <!-- //popup -->
        </div>
        <input type="hidden" id="result" name="result" value="<%= _result %>" />
    </body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/gsap/3.3.3/gsap.min.js"></script>
    <script src="/resources/js/common.js"></script>
</asp:Content>

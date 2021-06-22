<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.UserApplication.board.roulette._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page_event02">
        <div id="wrap" class="wrapper">
            <div class="title_wrap">
                <div class="btn_allmenu">
                    <a href="#"><img src="/resources/img/allmenu.png" alt="전체메뉴"></a>
                </div>
                <div class="page_title ft-color">
                    올 여름엔 매일매일 룰렛 돌리자! EVENT
                </div>

                <a href="/achieve/" class="btn-home">
                    <img src="/resources/img/hub.png" alt="홈으로">
                </a>
            </div>

            <uc1:menu runat="server" ID="menu" />
            <!-- //nav -->

            <div class="event_kv event_roulette">
                <img src="/resources/img/event_kv02.png" alt="">

                <div class="roulette_box">
                    <div class="roulette obj">
                        <img src="/resources/img/event_kv02_coupon1.png" alt="꽝">
                        <img src="/resources/img/event_kv02_coupon2.png" alt="당첨">
                        <img src="/resources/img/event_kv02_coupon1.png" alt="꽝">
                        <img src="/resources/img/event_kv02_coupon2.png" alt="당첨">
                        <img src="/resources/img/event_kv02_coupon1.png" alt="꽝">
                        <img src="/resources/img/event_kv02_coupon2.png" alt="당첨">
                    </div>

                    <div class="item">
                        <img src="/resources/img/event_kv02_img.png" alt="">
                    </div>

                    <a href="javascript:;" class="btn_roulette">
                        <img src="/resources/img/event_kv02_btn.png" alt="룰렛 돌리기">
                    </a>
                </div>
            </div>

            <!-- popup -->
            <div class="popup_wrap popup_winning">
                <div class="popup_inner">
                    <div class="popup_container" style="height:400px;">
                        <div class="popup_title">
                            <p>
                                <img src="/resources/img/btn_winning_title01.png" alt="당첨되셨습니다!">
                            </p>
                        </div>
                        <div class="popup_cont">
                            <a href="javascript:;" class="btn btn_close">
                                <img src="/resources/img/btn_winning_close.png" alt="확인">
                            </a>
                        </div>
                    </div>
                    <button class="popup_close">
                        <img src="/resources/img/btn_close.png" alt="닫기">
                    </button>
                </div>
            </div>

            <div class="popup_wrap popup_fail">
                <div class="popup_inner">
                    <div class="popup_container">
                        <div class="popup_title">
                            <p>
                                <img src="/resources/img/btn_winning_title02.png" alt="다음 기회에 도전하세요.">
                            </p>
                        </div>
                        <div class="popup_cont">
                            <a href="javascript:;" class="btn btn_close">
                                <img src="/resources/img/btn_winning_close.png" alt="확인">
                            </a>
                        </div>
                    </div>

                    <button class="popup_close">
                        <img src="/resources/img/btn_close.png" alt="닫기">
                    </button>
                </div>
            </div>
            <!-- //popup -->
        </div>
        <!-- //wrap -->

        <div class="w_intro_bottom w_only">
            <img src="/resources/img/flogo.png" alt="">
        </div>
    </div>

    <input type="hidden" id="result" name="result" value="<%= _result %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
    <script>
        $(document).ready(function () {
            $('.btn_roulette').on('click', function (e) {
                var result = $("#result").val();
                if (result !== "EXISTS") {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/api/roulette/play",
                        data: JSON.stringify({ "result": result }),
                        dataType: "json",
                        async: false,
                        success: function (json) {
                            if (json.result !== "SUCCESS") {
                                alert("정상적으로 참여되지 않았습니다.");
                                return false;
                            }
                        },
                        error: function (jqxhr, status, error) {
                            var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                            alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                        }
                    });
                }
            });
        });
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.board.evt.detail" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />
    <link rel="stylesheet" href="/resources/css/board.css" />
    <script type="text/javascript" src="/resources/js/common.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <body>
        <div id="sub_wrap" class="subMeta06">
            <uc1:menu runat="server" ID="menu" />
            <div class="subContainer">
                <div class="eventPage">
                    <div class="event_kv event_roulette">

                        <div class="roulette_box">
                            <div class="roulette obj">
                                <img src="/resources/img/sub/event/event_kv02_coupon1.png" alt="꽝">
                                <img src="/resources/img/sub/event/event_kv02_coupon2.png" alt="당첨">
                                <img src="/resources/img/sub/event/event_kv02_coupon1.png" alt="꽝">
                                <img src="/resources/img/sub/event/event_kv02_coupon2.png" alt="당첨">
                                <img src="/resources/img/sub/event/event_kv02_coupon1.png" alt="꽝">
                                <img src="/resources/img/sub/event/event_kv02_coupon2.png" alt="당첨">
                            </div>

                            <div class="item">
                                <img src="/resources/img/sub/event/event_kv02_img.png" alt="">
                            </div>

                            <a href="javascript:;" class="btn_roulette">
                                <img src="/resources/img/sub/event/event_kv02_btn.png" alt="룰렛 돌리기">
                            </a>
                        </div>
                    </div>


                    <div class="board_write_wrap">
                        <div class="write_box">
                            <textarea id="contents" placeholder="댓글을 작성해주세요."></textarea>
                            <button class="btn_comment" onclick="if (!reply.regist()) { return false; }">
                                <img src="/resources/img/sub/board/replyBt.png" alt="댓글 달기" /></button>
                        </div>

                        <div class="view_area">
                            <asp:Repeater ID="rptList" runat="server">
                                <ItemTemplate>
                                    <%--<div class="content<%# Eval("DepthSeq").ToString() != "1" ? " view_reply" : "" %>" id="content_<%# Eval("Id") %>">
                                    <div class="info">
                                        <%# Eval("DepthSeq").ToString() != "1" ? "<span class=\"ico\"><img src=\"/resources/img/ico_reply.png\" alt=\"\"></span>" : "" %>
                                        <span class="place"></span><span class="user"></span>
                                        
                                        <span class="date"></span>
                                    </div>

                                    <div class="txt">
                                        
                                    </div>

                                    <%#Like(Eval("Id").ToString(), Eval("Like").ToString(), Eval("LikeCount").ToString(), Eval("DelYn").ToString()) %>
                                </div>--%>





                                    <div class="content<%# Eval("DepthSeq").ToString() != "1" ? " view_reply" : "" %>" id="content_<%# Eval("Id") %>">
                                        <div class="view_area_info">
                                            <%# Eval("DepthSeq").ToString() != "1" ? "<span class=\"ico\"><img src=\"/resources/img/sub/board/replyIcon3.png\" alt=\"\"></span>" : " <span class=\"img\"><img src=\"/resources/img/sub/board/writerImg.png\" alt=\"\" /></span>" %>

                                            <span class="place"><%# Eval("Branch.Name").ToString() %></span>
                                            <span class="user"><%# Eval("Member.Name") %></span>
                                            <div class="editBtnList">
                                                <%# Eval("DelYn").ToString() == "N" ? "<a href=\"javascript:reply.answer('"+ Eval("Id").ToString() +"');\" class=\"btn_comment_re\">답글<span>|</span></a>" : "" %>
                                                <%# (Eval("DelYn").ToString() == "N" && Eval("Fkmember").ToString() == OrangeSummer.Common.User.Identify.Id) ? "<a href=\"javascript:reply.show('"+ Eval("Id") +"');\" class=\"btn_comment_mod\">수정<span>|</span></a>" : "" %>
                                                <%# (Eval("DelYn").ToString() == "N" && Eval("Fkmember").ToString() == OrangeSummer.Common.User.Identify.Id) ? "<a href=\"javascript:reply.delete('"+ Eval("Id") +"');\" class=\"btn_comment_del\">삭제</a>" : "" %>
                                            </div>
                                        </div>

                                        <div class="txt">
                                            <%# Eval("DelYn").ToString() == "Y" ? "<del>삭제된 글입니다.</del>": Eval("Contents").ToString().Replace(Environment.NewLine, "<br />") %>
                                        </div>

                                        <div class="iconList">
                                            <div class="reply">
                                                <a href="#" style="cursor:default;" class="on">
                                                    <img src="/resources/img/sub/board/replyIcon2.png" alt="">
                                                </a>
                                                <div class="number">댓글 <span>2</span></div>
                                            </div>

                                            <%#Like(Eval("Id").ToString(), Eval("Like").ToString(), Eval("LikeCount").ToString(), Eval("DelYn").ToString()) %>

                                            <span class="date"><%# Eval("RegistDate").ToString().Substring(0, 10).Replace("-","." )%></span>
                                        </div>
                                    </div>


                                </ItemTemplate>
                                <FooterTemplate>
                                    <%# AnchorPage() %>
                                </FooterTemplate>
                            </asp:Repeater>




                        </div>

                        <%=_paging%>
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

    </body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/gsap/3.3.3/gsap.min.js"></script>
    <script src="/resources/js/common.js"></script>
    <script>
        var reply = {
            regist: function () {
                var literal = {
                    contents: { selector: $("#contents"), required: { message: "내용을 입력해주세요." } }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/api/event/regist",
                        data: JSON.stringify({ "id": "<%= Request["id"] %>", "contents": $("#contents").val() }),
                        dataType: "json",
                        async: false,
                        success: function (json) {
                            if (json != null) {
                                if (json.result === "SUCCESS") {
                                    location.reload();
                                } else {
                                    alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.");
                                    return false;
                                }
                            } else {
                                alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.");
                                return false;
                            }
                        },
                        error: function (jqxhr, status, error) {
                            var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                            alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                        }
                    });
                }
            },
            like: function (id) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/api/event/like",
                    data: JSON.stringify({ "id": id }),
                    dataType: "json",
                    async: false,
                    success: function (json) {
                        if (json != null) {
                            if (json.result === "PLUS") {
                                $("#like_" + id + " > a").addClass("on");
                                $("#like_" + id + " > a > img").attr("src", "/resources//resources//resources/img/ico_like_chk.png");
                            } else {
                                $("#like_" + id + " > a").removeClass("on");
                                $("#like_" + id + " > a > img").attr("src", "/resources//resources//resources/img/ico_like.png");
                            }

                            $("#like_" + id + " > div > span").text(json.count);
                        } else {
                            alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.");
                            return false;
                        }
                    },
                    error: function (jqxhr, status, error) {
                        var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                        alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                    }
                });
            },
            show: function (id) {
                var $area = $("#content_" + id);
                var html = "";
                html += "<div class=\"write_reply\">";
                html += "    <textarea placeholder=\"수정내용\">" + $area.find("div.txt").html().trim().replace(/<br>/gi, "\n") + "</textarea>";
                html += "    <div class=\"btn_area\">";
                html += "        <button type=\"button\" onclick=\"reply.hide('" + id + "');\" class=\"btn btn_reply_del\">취소하기</button>";
                html += "        <button type=\"button\" onclick=\"reply.modify('" + id + "');\" class=\"btn btn_reply_mod\">수정하기</button>";
                html += "    </div>";
                html += "</div>";

                $area.find("div.txt").hide();
                $area.find("div.like").hide();
                $area.find("div.info").hide();
                $area.find("div.info").after(html);

                return false;
            },
            hide: function (id) {
                var $area = $("#content_" + id);
                $area.find("div.txt").show();
                $area.find("div.like").show();
                $area.find("div.info").show();
                $area.find("div.info").next().remove();
            },
            modify: function (id) {
                var $area = $("#content_" + id);
                var $content = $area.find("div.write_reply > textarea");

                if ($content.val() === "") {
                    alert("내용을 입력해주세요.");
                    $content.focus();
                    return false;
                } else {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/api/event/modify",
                        data: JSON.stringify({ "id": id, "contents": $content.val() }),
                        dataType: "json",
                        async: false,
                        success: function (json) {
                            if (json != null) {
                                if (json.result === "SUCCESS") {
                                    $area.find("div.txt").html(json.message).show();
                                    $area.find("div.like").show();
                                    $area.find("div.info").show();
                                    $area.find("div.info").next().remove();
                                } else {
                                    alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.");
                                    return false;
                                }
                            } else {
                                alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.");
                                return false;
                            }
                        },
                        error: function (jqxhr, status, error) {
                            var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                            alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                        }
                    });
                }
            },
            delete: function (id) {
                if (confirm("삭제하시겠습니까?")) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/api/event/delete",
                        data: JSON.stringify({ "id": id }),
                        dataType: "json",
                        async: false,
                        success: function (json) {
                            if (json != null) {
                                if (json.result === "SUCCESS") {
                                    location.reload();
                                } else {
                                    alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.");
                                    return false;
                                }
                            } else {
                                alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.");
                                return false;
                            }
                        },
                        error: function (jqxhr, status, error) {
                            var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                            alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                        }
                    });
                }
            },
            answer: function (id) {
                var $area = $("#content_" + id);
                var length = $area.next("div.write_box").length;

                if (length === 0) {
                    var html = "";
                    $area.find("a.btn_comment_re").text("답글 취소");
                    html += "<div class=\"write_box\">";
                    html += "    <textarea placeholder=\"답글을 작성해주세요.\"></textarea>";
                    html += "    <button type=\"button\" onclick=\"reply.add('" + id + "');\" class=\"btn_comment\"><img src=\"/resources/img/sub/board/replyBt.png\"  /></button>";
                    html += "</div>";
                    $area.after(html);
                } else {
                    $area.find("a.btn_comment_re").text("답글");
                    $area.next("div.write_box").remove();
                }
            },
            add: function (id) {
                var $area = $("#content_" + id);
                var $content = $area.next("div.write_box").find("textarea");
                if ($content.val() === "") {
                    alert("내용을 입력해주세요.");
                    $content.focus();
                    return false;
                } else {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/api/event/answer",
                        data: JSON.stringify({ "id": id, "contents": $content.val() }),
                        dataType: "json",
                        async: false,
                        success: function (json) {
                            console.log(json);
                            if (json != null) {
                                if (json.result === "SUCCESS") {
                                    location.reload();
                                } else {
                                    alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.");
                                    return false;
                                }
                            } else {
                                alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.");
                                return false;
                            }
                        },
                        error: function (jqxhr, status, error) {
                            var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                            alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                        }
                    });
                }
            }
        };
    </script>
</asp:Content>

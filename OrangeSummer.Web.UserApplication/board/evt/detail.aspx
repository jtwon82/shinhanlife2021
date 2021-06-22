<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="OrangeSummer.Web.UserApplication.board.evt.detail" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page_event_detail">
        <div id="wrap" class="wrapper">
            <div class="title_wrap">
                <div class="btn_allmenu">
                    <a href="#"><img src="/resources/img/allmenu.png" alt="전체메뉴"></a>
                </div>
                <div class="page_title board_title">
                    이벤트
                </div>

                <a href="/achieve/" class="btn-home">
                    <img src="/resources/img/hub.png" alt="홈으로">
                </a>
            </div>

            <uc1:menu runat="server" ID="menu" />
            <!-- //nav -->

            <div class="board_view_wrap">
                <div class="title">
                    <ul>
                        <li>작성일 <span><%= _registDate %></span></li>
                        <li><%= _adminName %></li>
                    </ul>
                </div>

                <div class="contents">
                    <%= _contents %>
                </div>

                <div class="index_list">
                    <ul>
                        <li class="prev">
                            <%= _before %>
                        </li>
                        <li class="next">
                            <%= _next %>
                        </li>
                    </ul>
                </div>

                <div class="btn_area">
                    <a href="./?command=list<%= Parameters() %>" class="btn btn_board">목록보기</a>
                </div>
            </div>

            <div class="board_write_wrap">
                <div class="write_box">
                    <textarea id="contents" placeholder="댓글을 작성해주세요."></textarea>
                    <button type="button" onclick="if (!reply.regist()) { return false; }" class="btn_comment">댓글 달기</button>
                </div>

                <a name="anchor-list"></a>
                <div class="view_area">
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <div class="content<%# Eval("DepthSeq").ToString() != "1" ? " view_reply" : "" %>" id="content_<%# Eval("Id") %>">
                                <div class="info">
                                    <%# Eval("DepthSeq").ToString() != "1" ? "<span class=\"ico\"><img src=\"/resources/img/ico_reply.png\" alt=\"\"></span>" : "" %>
                                    <span class="place"><%# Eval("Branch.Name").ToString() %></span><span class="user"><%# Eval("Member.Name") %></span>
                                    <%# Eval("DelYn").ToString() == "N" ? "<a href=\"javascript:reply.answer('"+ Eval("Id").ToString() +"');\" class=\"btn btn_comment_re\">답글</a>" : "" %>
                                    <%# (Eval("DelYn").ToString() == "N" && Eval("Fkmember").ToString() == OrangeSummer.Common.User.Identify.Id) ? "<a href=\"javascript:reply.show('"+ Eval("Id") +"');\" class=\"btn btn_comment_mod\">수정</a>" : "" %>
                                    <%# (Eval("DelYn").ToString() == "N" && Eval("Fkmember").ToString() == OrangeSummer.Common.User.Identify.Id) ? "<a href=\"javascript:reply.delete('"+ Eval("Id") +"');\" class=\"btn btn_comment_del\">삭제</a>" : "" %>
                                    <span class="date"><%# Eval("RegistDate").ToString().Substring(2, 8) %></span>
                                </div>

                                <div class="txt">
                                    <%# Eval("DelYn").ToString() == "Y" ? "<del>삭제된 글입니다.</del>": Eval("Contents").ToString().Replace(Environment.NewLine, "<br />") %>
                                </div>


                                <%# Like(Eval("Id").ToString(), Eval("Like").ToString(), Eval("LikeCount").ToString(), Eval("DelYn").ToString()) %>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <%# AnchorPage() %>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

                <%= _paging %>
            </div>
        </div>
        <!-- //wrap -->

        <div class="w_intro_bottom w_only">
            <img src="/resources/img/flogo.png" alt="">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
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
                                $("#like_" + id + " > a > img").attr("src", "/resources/img/ico_like_chk.png");
                            } else {
                                $("#like_" + id + " > a").removeClass("on");
                                $("#like_" + id + " > a > img").attr("src", "/resources/img/ico_like.png");
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
                    html += "    <button type=\"button\" onclick=\"reply.add('"+ id +"');\" class=\"btn_comment\">답글 달기</button>";
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

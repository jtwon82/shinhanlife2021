<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.board.evt.detail" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
    <link rel="stylesheet" href="/resources/css/board.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />

    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <body>
        <div id="sub_wrap" class="subMeta08">

            <uc1:menu runat="server" ID="menu" />

            <div class="subContainer notice">
                <p class="subTitle"><img src="/resources/img/sub/eventTitle.png" alt="이벤트" /></p>

                <div class="board_view_wrap">
                    <div class="viewtitle">
                        <p class="title"><%=_title%></p>
                        <p class="replyNum"><%=_replyCoun%></p>
                        <ul class="info">
                            <li class="name"><%=_adminName%><span>I</span></li>
                            <li class="view">view <em><%=_viewCount%></em><span>I</span></li>
                            <li class="date"><%=_registDate%></li>
                        </ul>
                    </div>

                    <div class="contents">
                        <%=_contents %>
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
                        <a href="./?command=list<%=Parameters()%>" class="btn_boardList">목록보기</a>
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
                                <div class="content<%# Eval("DepthSeq").ToString() != "1" ? " view_reply" : "" %>" id="content_<%# Eval("Id") %>">
                                    <div class="view_area_info">
                                        <%# Eval("DepthSeq").ToString() != "1" ? "<span class=\"ico\"><img src='/resources/img/sub/board/replyIcon3.png'></span>" : " <span class=\"img\"><img src='"+Eval("Member.ProfileImg")+"' onerror=\"this.src='/resources/img/index/main_person_img2.png'\" style='width:83px;height:83px;' /></span>" %>
                                        <span class="place"><%# Eval("Branch.Name").ToString() %></span>
                                        <span class="user"><%# Eval("Member.Name") %></span>
                                        <div class="editBtnList">
                                            <%# Eval("DelYn").ToString() == "N" ? "<a href=\"javascript:reply.answer('"+ Eval("Id").ToString() +"');\" class=\"btn_comment_re\">답글</a>" : "" %>
                                            <%# (Eval("DelYn").ToString() == "N" && Eval("Fkmember").ToString() == OrangeSummer.Common.User.Identify.Id) ? "<a href=\"javascript:reply.show('"+ Eval("Id") +"');\" class=\"btn_comment_mod\"><span>|</span>수정</a><a href=\"javascript:reply.delete('"+ Eval("Id") +"');\" class=\"btn_comment_del\"><span>|</span>삭제</a>" : "" %>
                                        </div>
                                    </div>

                                    <div class="txt">
                                        <%# Eval("DelYn").ToString() == "Y" ? "<del>삭제된 글입니다.</del>": Eval("Contents").ToString().Replace(Environment.NewLine, "<br />") %>
                                    </div>

                                    <div class="iconList">
                                        <div class="reply">
                                            <a href="javascript:;" style="cursor:default;" class="on">
                                                <img src="/resources/img/sub/board/replyIcon2.png" alt="">
                                            </a>
                                            <div class="number">댓글 <span><%# Eval("ReplyCount") %></span></div>
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
    </body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script>
        var reply = {e: { },
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
                        data: JSON.stringify({ "id": "<%=Request["id"] %>", "contents": $("#contents").val() }),
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
                                $("#like_" + id + " > a > img").attr("src", "/resources/img/sub/board/likeIcon2.png");
                            } else {
                                $("#like_" + id + " > a").removeClass("on");
                                $("#like_" + id + " > a > img").attr("src", "/resources/img/sub/board/unlike.png");
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
                html += "    <div class=\"btn_area rere\">";
                html += "        <button type=\"button\" onclick=\"reply.hide('" + id + "');\" class=\"btn_reply_del\"><img src=\"/resources/img/sub/board/btn_reply_delBtn.png\" alt=\"삭제하기\" /></button>";
                html += "        <button type=\"button\" onclick=\"reply.modify('" + id + "');\" class=\"btn_reply_mod\"><img src=\"/resources/img/sub/board/btn_reply_modBtn.png\" alt=\"수정하기\" /></button>";
                html += "    </div>";
                html += "</div>";


                $area.find("div.txt").hide();
                $area.find("div.iconList").hide();
                $area.find("div.view_area_info").hide();
                $area.find("div.view_area_info").after(html);

                return false;
            },
            hide: function (id) {
                var $area = $("#content_" + id);
                $area.find("div.txt").show();
                $area.find("div.iconList").show();
                $area.find("div.view_area_info").show();
                $area.find("div.view_area_info").next().remove();
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
                                    $area.find("div.iconList").show();
                                    $area.find("div.view_area_info").show();
                                    $area.find("div.view_area_info").next().remove();
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
                    $area.find("a.btn_comment_re").html("취소");
                    html += "<div class=\"write_box\" style='float:left;'>";
                    html += "    <textarea placeholder=\"답글을 작성해주세요.\"></textarea>";
                    html += "    <button type=\"button\" onclick=\"reply.add('" + id + "');\" ><img src=\"/resources/img/sub/board/replyBt.png\"  /></button>";
                    html += "</div>";
                    $area.after(html);
                } else {
                    $area.find("a.btn_comment_re").html("답글");
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

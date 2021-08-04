<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.board.word._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />
    <link rel="stylesheet" href="/resources/css/board.css" />
    <script type="text/javascript" src="/resources/js/common.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/gsap/3.3.3/gsap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<body>
	<div id="sub_wrap" class="event_vote">
            <uc1:menu runat="server" ID="menu" />
		<div class="subContainer">
			<div class="eventPage">
				<div class="event_kv event_wc">
					<img class="top" src="/resources/img/sub/event/event_vote_top.png" alt="">
                <div class="wc_box">
                    <ul class="wc_list">
                        <li>
                            <label for="ucc_list01">
                            	<div class="img">
                                	<img src="/resources/img/sub/event/event_writing_1.png" alt="">
                           	 	</div>
                           	 <input type="radio" name="ucc_list" id="ucc_list01" value="1"><em></em>
                        	</label>
                        </li>
                         <li>
                           <label for="ucc_list02">
	                           	<div class="img">
	                                <img src="/resources/img/sub/event/event_writing_2.png" alt="">
	                            </div>
                          	  <input type="radio" name="ucc_list" id="ucc_list02" value="2"><em></em>
                            </label>
                        </li>
                         <li>
                           
                            <label for="ucc_list03">
                            	<div class="img">
	                                <img src="/resources/img/sub/event/event_writing_3.png" alt="">
	                            </div>
                            	<input type="radio" name="ucc_list" id="ucc_list03" value="3"><em></em>
                        		 
                        	</label>
                        </li>
                         <li>
                            
                            <label for="ucc_list04">
                            	<div class="img">
		                                <img src="/resources/img/sub/event/event_writing_4.png" alt="">
		                            </div>
                            	<input type="radio" name="ucc_list" id="ucc_list04" value="4"><em></em>
                        	</label>
                        </li>
                         <li>
                         	<label for="ucc_list05">
	                            <div class="img">
	                                <img src="/resources/img/sub/event/event_writing_5.png" alt="">
	                            </div>
	                           <input type="radio" name="ucc_list" id="ucc_list05" value="5"><em></em>
                           </label>
                        </li>
                         <li>
                         	 <label for="ucc_list06">
	                            <div class="img">
	                                <img src="/resources/img/sub/event/event_writing_6.png" alt="">
	                            </div>
	                           <input type="radio" name="ucc_list" id="ucc_list06" value="6"><em></em>
                           </label>
                        </li>
                         <li>
                         	 <label for="ucc_list07">
	                            <div class="img">
	                                <img src="/resources/img/sub/event/event_writing_7.png" alt="">
	                            </div>
	                           <input type="radio" name="ucc_list" id="ucc_list07" value="7"><em></em>
                           </label>
                        </li>
                         <li>
                         	<label for="ucc_list08">
	                            <div class="img">
	                                <img src="/resources/img/sub/event/event_writing_8.png" alt="">
	                            </div>
	                            <input type="radio" name="ucc_list" id="ucc_list08" value="8"><em></em>
                            </label>
                        </li>
                         <li>
                         	 <label for="ucc_list09">
	                            <div class="img">
	                                <img src="/resources/img/sub/event/event_writing_9.png" alt="">
	                            </div>
	                          <input type="radio" name="ucc_list" id="ucc_list09" value="9"><em></em>
                          </label>
                        </li>
                         <li>
                         	<label for="ucc_list10">
	                            <div class="img">
	                                <img src="/resources/img/sub/event/event_writing_10.png" alt="">
	                            </div>
	                            <input type="radio" name="ucc_list" id="ucc_list10" value="10"><em></em>
                            </label>
                        </li>
                         <li>
                         	<label for="ucc_list11">
	                            <div class="img">
	                                <img src="/resources/img/sub/event/event_writing_11.png" alt="">
	                            </div>
	                            <input type="radio" name="ucc_list" id="ucc_list11" value="11"><em></em>
                            </label>
                        </li>
                         <li>
                         	<label for="ucc_list12">
	                            <div class="img">
	                                <img src="/resources/img/sub/event/event_writing_12.png" alt="">
	                            </div>
	                           <input type="radio" name="ucc_list" id="ucc_list12" value="12"><em></em>
                           </label>
                        </li>
                         <li>
                         	<label for="ucc_list13">
	                            <div class="img">
	                                <img src="/resources/img/sub/event/event_writing_13.png" alt="">
	                            </div>
	                           <input type="radio" name="ucc_list" id="ucc_list13" value="13"><em></em>
                           </label>
                        </li>
                         <li>
                         	<label for="ucc_list14">
	                            <div class="img">
	                                <img src="/resources/img/sub/event/event_writing_14.png" alt="">
	                            </div>
	                           <input type="radio" name="ucc_list" id="ucc_list14" value="14"><em></em>
	                           </label>
                        </li>
                         <li>
                         	 <label for="ucc_list15">
	                            <div class="img">
	                                <img src="/resources/img/sub/event/event_writing_15.png" alt="">
	                            </div>
	                          <input type="radio" name="ucc_list" id="ucc_list15" value="15"><em></em>
                          </label>
                        </li>
                    </ul>

                    <a href="javascript:word.vote();" class="btn_ucc">
                        <img src="/resources/img/sub/event/event_vote_btn.png" alt="투표하기">
                    </a>
                     <a href="" class="logo_bottom">
                        <img src="/resources/img/sub/event/event_vote_logo.png" alt="신한라이프">
                    </a>
                </div>
            </div>
			</div>
		</div>
	</div>
</body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script>
        var word = {
            vote: function () {
                var literal = {
                    usr: { selector: $(":input:radio[name='ucc_list']"), required: { message: "투표할 사행시를 고르고 투표버튼을 눌러주세요." } }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/api/word/vote",
                        data: JSON.stringify({ "vote": $(":input:radio[name='ucc_list']:checked").val() }),
                        dataType: "json",
                        async: false,
                        success: function (json) {
                            if (json != null) {
                                alert(json.message);
                                if (json.result === "SUCCESS") {
                                    $(":input:radio[name='ucc_list']:checked").prop("checked", false);
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
                        url: "/api/word/regist",
                        data: JSON.stringify({ "contents": $("#contents").val() }),
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
                    url: "/api/word/like",
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
                        url: "/api/word/modify",
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
                        url: "/api/word/delete",
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
                    html += "    <button type=\"button\" onclick=\"reply.add('" + id + "');\" class=\"btn_comment\">답글 달기</button>";
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
                        url: "/api/word/answer",
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

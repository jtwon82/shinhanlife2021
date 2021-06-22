<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.UserApplication.member.regist._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page_signup">
        <div id="wrap" class="wrapper">
            <div class="title_wrap">
                <div class="btn_allmenu">
                    <a href="#"><img src="/resources/img/allmenu.png" alt="전체메뉴"></a>
                </div>
                <div class="page_title">
                    회원가입
                </div>
            </div>
            <uc1:menu runat="server" id="menu" />

            <div class="signup_banner">
                <img src="/resources/img/signup_banner.png" alt="">
            </div>

            <div class="signup_wrap">
                <p class="title">회원정보 기입</p>

                <div class="form_group select_wrap">
                    <label for="branch">지점</label>
                    <asp:DropDownList ID="branch" runat="server" ClientIDMode="Static" CssClass="form_control"></asp:DropDownList>
                </div>

                <div class="form_group select_wrap">
                    <label for="level">신분</label>
                    <asp:DropDownList ID="level" runat="server" ClientIDMode="Static" CssClass="form_control"></asp:DropDownList>
                </div>

                <div class="input_wrap">
                    <div class="form_group">
                        <label for="code">FC 코드</label>
                        <asp:TextBox ID="code" runat="server" ClientIDMode="Static" MaxLength="5" placeholder="숫자 5자리 필수" CssClass="form_control"></asp:TextBox>
                        <input type="hidden" name="check" id="check" value="N" />
                        <a href="javascript:member.check();" class="btn_check">중복확인</a>
                    </div>
                </div>

                <div class="form_group">
                    <label for="pwd1">비밀번호</label>
                    <asp:TextBox ID="pwd1" runat="server" ClientIDMode="Static" TextMode="Password" MaxLength="12" placeholder="숫자 4자리 이상 입력해주세요." CssClass="form_control"></asp:TextBox>
                </div>

                <div class="form_group">
                    <label for="pwd2">비밀번호 확인</label>
                    <asp:TextBox ID="pwd2" runat="server" ClientIDMode="Static" TextMode="Password" MaxLength="12" placeholder="비밀번호를 재입력해주세요." CssClass="form_control"></asp:TextBox>
                </div>

                <div class="form_group">
                    <label for="name">이름</label>
                    <asp:TextBox ID="name" runat="server" ClientIDMode="Static" MaxLength="10" placeholder="실명으로 등록해주세요." CssClass="form_control"></asp:TextBox>
                </div>

                <p class="desc">
                    ※ 반드시 실명으로 등록하여 가입해주세요.<br>
                    이벤트 참여 시 이름이 노출됩니다.
                </p>

                <div class="form_group">
                    <label for="mobile">전화번호</label>
                    <asp:TextBox ID="mobile" runat="server" ClientIDMode="Static" MaxLength="11" placeholder="숫자만 입력" CssClass="form_control"></asp:TextBox>
                </div>

                <div class="form_group form_checkbox">
                    <div class="form_check">
                        <input type="checkbox" name="term_agree_mail" id="term_agree_mail" class="form_check_input" value="Y">
                        <asp:HiddenField ID="advert" runat="server" ClientIDMode="Static" Value="N" />
                        <label for="term_agree_mail" class="form_check_label">
                            <span>선택 )</span>  참여자 모집 및 이벤트 진행 안내 등의 문자 수신에<br>
                            동의합니다.
                        </label>
                    </div>

                    <div class="form_check">
                        <input type="checkbox" name="term_agree_user" id="term_agree_user" class="form_check_input">
                        <label for="term_agree_user" class="form_check_label">
                            <span>필수 )</span>  서비스 이용약관에 동의합니다. 
                        </label>
                        <a href="/member/terms/" class="btn_more">내용확인</a>
                    </div>

                    <div class="form_check">
                        <input type="checkbox" name="term_agree_privacy" id="term_agree_privacy" class="form_check_input">
                        <label for="term_agree_privacy" class="form_check_label">
                            <span>필수 )</span>  개인정보 수집 및 이용에 동의합니다.  
                        </label>
                        <a href="/member/terms/" class="btn_more">내용확인</a>
                    </div>
                </div>

                <button id="btnResult" runat="server" onserverclick="btnRegist_Click" onclick="if (!member.regist()) { return false; }" class="btn_signup">회원가입</button>
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
        $(document).ready(function () {
            $("#term_agree_mail").click(function () {
                if ($(this).is(":checked")) {
                    $("#advert").val("Y");
                } else {
                    $("#advert").val("N");
                }
            });
        });

        var member = {
            common: function () {
                var literal = {
                    branch: { selector: $("#branch"), required: { message: "지점을 선택해주세요." } },
                    level: { selector: $("#level"), required: { message: "신분을 선택해주세요." } },
                    code: {
                        selector: $("#code"), required: { message: "FC 코드를 입력해주세요." },
                        length: { value: 5, message: "FC 코드는 숫자 5자리로 입력해주세요." },
                        digit: { message: "FC 코드는 숫자 5자리로 입력해주세요." }
                    }
                };

                return $.validate.rules(literal, { mode: "alert" });
            },
            regist: function () {
                var literal = {
                    code: function () {
                        return member.common();
                    },
                    checker: function () {
                        if ($("#check").val() === "N") {
                            alert("FC 코드를 중복확인을 해주세요.");
                            return false;
                        }
                        return true;
                    },
                    pwd1: {
                        selector: $("#pwd1"), required: { message: "비밀번호를 입력해주세요." },
                        min: { value: 4, message: '비밀번호를 4자 이상으로 입력하세요.' }
                    },
                    pwd2: { selector: $("#pwd2"), required: { message: "비밀번호를 재입력해주세요." } },
                    confirm: { selector: $("#pwd2"), compare: { value: $("#pwd1").val(), message: "비밀번호가 일치하지 않습니다.\n다시 확인 후 입력해주세요." } },
                    name: { selector: $("#name"), required: { message: "이름을 입력해주세요." } },
                    //mobile: {
                    //    selector: $("#mobile"), required: { message: "전화번호를 입력해주세요." },
                    //    digit: { message: "전화번호는 숫자만 입력해주세요." }
                    //},
                    user: { selector: $("#term_agree_user"), required: { message: "서비스 이용약관에 동의해주세요." } },
                    privacy: { selector: $("#term_agree_privacy"), required: { message: "개인정보 수집 및 이용에 동의해주세요." } },
                    success: function () {
                        $("#branch, #level").prop("disabled", false);
                        return true;
                    }
                };

                var run = false;
                if (run) {
                    return false;
                }

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    run = true;
                    return true;
                }
            },
            check: function () {
                var checker = member.common();
                if ($("#check").val() === "Y") {
                    alert("이미 중복체크되었습니다.");
                    return false;
                }

                if (checker) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/api/member/check",
                        data: JSON.stringify({ "branch": $("#branch").val(), "level": $("#level").val(), "code": $("#code").val() }),
                        dataType: "json",
                        async: false,
                        success: function (json) {
                            if (json != null) {
                                alert(json.message);
                                if (json.result === "SUCCESS") {
                                    $("#code").prop("readonly", true).css("background-color", "#dcdcdc");
                                    $("#branch, #level").prop("disabled", true).css("background-color", "#dcdcdc");
                                    $("#check").val("Y");
                                } else {
                                    $("#branch, #level, #code").val("");
                                    $("#check").val("N");
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

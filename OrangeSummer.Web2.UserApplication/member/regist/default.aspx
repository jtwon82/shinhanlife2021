<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.member.regist._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />

    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <body>
        <div id="sub_wrap" class="subMeta05">
            <uc1:menu runat="server" ID="menu" />

            <div class="subContainer">
                <p class="subTitle">회원가입</p>
                <div class="signup_wrap">
                    <p class="title">회원정보 기입</p>

                    <div class="input_wrap">
                        <div class="form_group">
                            <label for="user_name">성명</label>
                            <asp:TextBox ID="user_name" runat="server" ClientIDMode="Static" MaxLength="10" placeholder="실명으로 등록해주세요." CssClass="form_control"></asp:TextBox>
                            <p class="warning">
                                ※ 반드시 실명으로 등록하여 가입해주세요.<br />
                                이벤트 참여 시 이름이 노출됩니다.
                            </p>
                        </div>

                        <div class="form_group">
                            <label for="user_tel">연락처</label>
                            <asp:TextBox ID="user_tel" type="number" runat="server" ClientIDMode="Static" placeholder="숫자만 입력" CssClass="form_control" MaxLength="11"></asp:TextBox><input type="hidden" name="check_tel" id="check_tel" value="N" /><!--
                                 --><a href="javascript:member.SendRndNo();" class="btn_check" id="certiBtn">인증하기</a>
                            <span class="blank"></span>
                            <p class="certiCheck" style="display: none;">
                                <input type="text" name="certi_num" id="certi_num" placeholder="인증번호 입력" class="form_control" maxlength="4"><a href="javascript:member.CompareRndNo()" class="btn_check">인증확인</a>
                            </p>
                        </div>

                        <div class="form_group">
                            <label for="user_fccode">FC 코드</label>
                            <asp:TextBox ID="user_fccode" type="number" runat="server" ClientIDMode="Static" MaxLength="5" placeholder="숫자 5자리 필수" CssClass="form_control"></asp:TextBox>
                            <input type="hidden" name="check" id="check" value="N" />
                            <a href="javascript:member.checkCode();" class="btn_check">중복확인</a>
                        </div>

                        <div class="form_group">
                            <label for="user_password">비밀번호</label>
                            <asp:TextBox ID="user_password" runat="server" ClientIDMode="Static" TextMode="Password" MaxLength="12" placeholder="숫자 4자리 이상 입력해주세요." CssClass="form_control"></asp:TextBox>
                        </div>

                        <div class="form_group">
                            <label for="user_password_check">비밀번호 확인</label>
                            <asp:TextBox ID="user_password_check" runat="server" ClientIDMode="Static" TextMode="Password" MaxLength="12" placeholder="비밀번호를 재입력해주세요." CssClass="form_control"></asp:TextBox>
                        </div>

                        <div class="form_group form_checkbox">
                            <div class="form_check">
                                <input type="checkbox" name="term_agree_mail" id="term_agree_mail" class="form_check_input" value="Y"/>
                                <asp:HiddenField ID="advert" runat="server" ClientIDMode="Static" Value="N" />
                                <label for="term_agree_mail" class="form_check_label">
                                    <span>선택 )</span>  참여자 모집 및 이벤트 진행 안내 등의 문자 수신에<br>
                                    동의합니다.
                                </label>
                            </div>

                            <div class="form_check">
                                <input type="checkbox" name="term_agree_user" id="term_agree_user" class="form_check_input" value="Y"/>
                                <label for="term_agree_user" class="form_check_label">
                                    <span>필수 )</span>  서비스 이용약관에 동의합니다. 
                                </label>
                                <a href="/member/terms" class="btn_more">내용확인 &gt;</a>
                            </div>

                            <div class="form_check">
                                <input type="checkbox" name="term_agree_privacy" id="term_agree_privacy" class="form_check_input" value="Y"/>
                                <label for="term_agree_privacy" class="form_check_label">
                                    <span>필수 )</span>  개인정보 수집 및 이용에 동의합니다.  
                                </label>
                                <a href="/member/terms" class="btn_more">내용확인 &gt;</a>
                            </div>
                        </div>
                    </div>
                    <button id="btnResult" runat="server" onserverclick="btnRegist_Click" onclick="if (!member.regist()) { return false; }" class="btn_signup">회원가입</button>
                </div>

            </div>
        </div>
    </body>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
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
                    //branch: { selector: $("#branch"), required: { message: "지점을 선택해주세요." } },
                    //level: { selector: $("#level"), required: { message: "신분을 선택해주세요." } },
                    code: {
                        selector: $("#user_fccode"), required: { message: "FC 코드를 입력해주세요." },
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
                            alert("FC코드 중복 확인 버튼을 선택해주세요.");
                            return false;
                        }
                        if ($("#check_tel").val() != 'Y') {
                            alert("연락처를 인증해주세요.");
                            return false;
                        }
                        return true;
                    },
                    user_password: {
                        selector: $("#user_password"), required: { message: "비밀번호를 입력해주세요." },
                        min: { value: 4, message: '비밀번호를 4자 이상으로 입력하세요.' }
                    },
                    user_password_check: { selector: $("#user_password_check"), required: { message: "비밀번호를 재입력해주세요." } },
                    confirm: { selector: $("#user_password_check"), compare: { value: $("#user_password").val(), message: "비밀번호가 일치하지 않습니다.\n다시 확인 후 입력해주세요." } },
                    user_name: { selector: $("#user_name"), required: { message: "이름을 입력해주세요." } },
                    user_tel: {
                        selector: $("#user_tel"), required: { message: "연락처를 입력해주세요." },
                        digit: { message: "전화번호는 숫자만 입력해주세요." }
                    },
                    term_agree_user: { selector: $("#term_agree_user"), required: { message: "서비스 이용약관에 동의해주세요." } },
                    term_agree_privacy: { selector: $("#term_agree_privacy"), required: { message: "개인정보 수집 및 이용에 동의해주세요." } },
                    success: function () {
                        //$("#branch, #level").prop("disabled", false);
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
            checkCode: function () {
                if (window.checkCode) return;

                var checker = member.common();
                if ($("#check").val() === "Y") {    alert("이미 중복체크되었습니다."); return false;   }

                if (checker) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/api/member/checkCode",
                        //data: JSON.stringify({ "branch": $("#branch").val(), "level": $("#level").val(), "code": $("#user_fccode").val() }),  58845
                        data: JSON.stringify({ "branch": "", "level": "", "code": $("#user_fccode").val(), "name":$("#user_name").val() }),
                        dataType: "json",
                        async: false,
                        success: function (json) {
                            if (json != null) {
                                if (json.result === "SUCCESS") {
                                    inputBoxLock("#user_fccode, #user_name");
                                    window.checkCode = true;

                                    $("#check").val("Y");
                                    alert("사용 가능한 FC코드 입니다.");

                                } else if(json.result=='EXISTS'){
                                    $("#branch, #level, #user_fccode").val("");
                                    $("#check").val("N");
                                    alert("이미 사용 중인 FC코드 입니다.");

                                } else {
                                    $("#branch, #level, #user_fccode").val("");
                                    $("#check").val("N");
                                    alert("코드를 다시 확인해주세요.");
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
            SendRndNo: function () {
                if (window.compareRndNo) return;

                var $user_tel = $("#user_tel");
                if ($user_tel.val() == '' || $user_tel.val().length<10) { alert("전화번호를 입력해주세요."); $user_tel.focus(); return false; }

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/api/member/checkPno",
                    data: JSON.stringify({ "Mobile": $user_tel.val() }),
                    dataType: "json",
                    async: false,
                    success: function (json) {
                        console.log(json);
                        if (json.result == 'EXISTS') {
                            alert("이미 등록된 연락처입니다.");

                        } else if (json.result == 'SUCCESS') {
                            $(".certiCheck").show();

                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: "/api/Sms/sendMsg",
                                data: JSON.stringify({ "Pno": $user_tel.val() }),
                                dataType: "json",
                                async: false,
                                success: function (json) {
                                    console.log(json);
                                    if (json.result == 'SUCCESS') {
                                        
                                        $("#check_tel").val('Y');
                                        //alert("문자로 전송된 인증번호를 입력하고 로그인을 진행해주세요.");
                                    }
                                    else {
                                        alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + json.message);
                                    }
                                },
                                error: function (jqxhr, status, error) {
                                    var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                                    alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                                }
                            })
                        }
                        else {
                            alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + json.message);
                        }
                    },
                    error: function (jqxhr, status, error) {
                        var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                        alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                    }
                })


            },
            CompareRndNo: function () {
                if (window.compareRndNo) return;

                var $certi_num = $("#certi_num");
                if ($certi_num.val() == '') { alert("인증번호를 입력해주세요."); $certi_num.focus(); return false; }
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/api/Sms/compareRndNo",
                    data: JSON.stringify({ "RndNo": $certi_num.val() }),
                    dataType: "json",
                    async: false,
                    success: function (json) {
                        console.log(json);
                        if (json.result == 'SUCCESS') {
                            inputBoxLock("#user_tel, #certi_num");
                            window.compareRndNo = true;

                            alert("인증이 완료되었습니다. ");
                        } else {
                            alert("인증번호가 틀립니다. 다시 확인해 주세요.");
                        }
                    },
                    error: function (jqxhr, status, error) {
                        var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                        alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                    }
                })
            }
        };
        function inputBoxLock(input) {
            $(input).prop("readonly", true).css("background-color", "#dcdcdc");
        }
    </script>
</asp:Content>

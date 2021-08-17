<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.member.find_pw._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/reset.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
    <link rel="stylesheet" href="/resources/css/layout.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
    <link rel="stylesheet" href="/resources/css/sub.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
    
    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<body>
	<div id="sub_wrap" class="subMeta10">
            <uc1:menu runat="server" ID="menu" />

		<div class="subContainer" runat="server" id="pageLoad">
			<p class="subTitle"><img src="/resources/img/sub/findpwTitle.png" alt="비밀번호 찾기" /></p>
			<div class="signup_wrap">
				<p class="title">비밀번호찾기</p>

				<div class="input_wrap">
					<div class="form_group">
						<label for="user_name">성명</label>
                        <asp:TextBox ID="user_name" runat="server" ClientIDMode="Static" MaxLength="10" placeholder="실명으로 등록해주세요." CssClass="form_control"></asp:TextBox>
					</div>

					<div class="form_group">
						<label for="user_tel">연락처</label>
						<asp:TextBox ID="user_tel" type="number" runat="server" ClientIDMode="Static" placeholder="숫자만 입력" pattern="\d*" CssClass="form_control" MaxLength="11"></asp:TextBox><!--
                             --><input type="hidden" name="check_tel" id="check_tel" value="N" /><a href="javascript:member.SendRndNo();" class="btn_check" id="certiBtn">인증하기</a>
						<span class="blank"></span>
						<p class="certiCheck" style="display:none;"><input type="text" name="certi_num" id="certi_num" placeholder="인증번호 입력" class="form_control"><a href="javascript:member.CompareRndNo()" class="btn_check">인증확인</a></p>
					</div>

					<div class="form_group">
						<label for="user_fccode">FC 코드</label>
						<asp:TextBox ID="user_fccode" type="number" runat="server" ClientIDMode="Static" MaxLength="5" placeholder="숫자 5자리 필수" pattern="\d*" CssClass="form_control"></asp:TextBox><!--
                             --><input type="hidden" name="check" id="check" value="N" />
						<a href="javascript:member.checkCode();" class="btn_check">중복확인</a>
						<p class="warning one">* 성명/연락처 인증 후 코드 중복확인 해주세요.</p>
					</div>
				</div>
			</div>
			<button class="btn_signup findpw" id="btnRegist" runat="server" onserverclick="btnRegist_Click" onclick="if (!member.regist()) { return false; }">비밀번호찾기</button>
		</div>

		<div class="subContainer" runat="server" id="result">
			<p class="subTitle"><img src="/resources/img/sub/pwcTitle.png" alt="비밀번호 확인" /></p>
			<div class="pwFind_wrap">
				<p class="find_result">회원님의 비밀번호는 <br/>
				<em class="pw"><asp:TextBox ID="find_pw" runat="server" placeholder="[1234567890]"></asp:TextBox>
				</em> 입니다.</p>
			</div>
			<button class="btn_signup confirm" onclick="location.href='/index'; return false;">확인</button>
		</div>

	</div>
</body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<script>

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
                user_name: { selector: $("#user_name"), required: { message: "이름을 입력해주세요." } },
                user_tel: {
                    selector: $("#user_tel"), required: { message: "연락처를 입력해주세요." },
                    digit: { message: "전화번호는 숫자만 입력해주세요." }
                },
                code: function () {
                    return member.common();
                },
                //user_password: {
                //    selector: $("#user_password"), required: { message: "비밀번호를 입력해주세요." },
                //    min: { value: 4, message: '비밀번호를 4자 이상으로 입력하세요.' }
                //},
                //confirm: { selector: $("#user_password_check"), compare: { value: $("#user_password").val(), message: "비밀번호가 일치하지 않습니다.\n다시 확인 후 입력해주세요." } },
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
                //user_password_check: { selector: $("#user_password_check"), required: { message: "비밀번호를 재입력해주세요." } },
                //term_agree_user: { selector: $("#term_agree_user"), required: { message: "서비스 이용약관에 동의해주세요." } },
                //term_agree_privacy: { selector: $("#term_agree_privacy"), required: { message: "개인정보 수집 및 이용에 동의해주세요." } },
                success: function () {
                    //$("#branch, #level").prop("disabled", false);
                    return true;
                }
            };

            var checker = $.validate.rules(literal, { mode: "alert" });
            if (checker) {
                run = true;
                $('.btn_signup').hide();
                return true;
            }
        },
        checkCode: function () {
            if (window.checkCode) return;

            var checker = member.common();
            if ($("#check").val() === "Y") { alert("이미 중복체크되었습니다."); return false; }

            if (checker) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/api/member/checkCodeV3",
                    //data: JSON.stringify({ "branch": $("#branch").val(), "level": $("#level").val(), "code": $("#user_fccode").val() }),  58845
                    data: JSON.stringify({ "branch": "", "level": "", "code": $("#user_fccode").val(), "name": $("#user_name").val() }),
                    dataType: "json",
                    async: false,
                    success: function (json) {
                        if (json != null) {
                            if (json.result == 'EXISTS') {
                                inputBoxLock("#user_fccode, #user_name");
                                window.checkCode = true;

                                $("#check").val("Y");

                                alert("본인인증을 완료하였습니다. 비밀번호찾기 버튼을 눌러주세요.");

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
            if ($user_tel.val() == '' || $user_tel.val().length < 10) { alert("전화번호를 입력해주세요."); $user_tel.focus(); return false; }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/api/member/checkPno",
                data: JSON.stringify({ "Mobile": $user_tel.val() }),
                dataType: "json",
                async: false,
                success: function (json) {
                    if (json.result == 'EXISTS') {
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
                                    $(".certiCheck").show();
                                    $("#check_tel").val('Y');
                                }
                                else {
                                    alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + json.message);
                                }
                            },
                            error: function (jqxhr, status, error) {
                                var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                                alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                            }
                        });
                    }
                    else {
                        alert("입력한 전화번호를 확인해주세요.");
                    }
                }
            });

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

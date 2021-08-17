<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.member.login._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/reset.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
    <link rel="stylesheet" href="/resources/css/layout.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<body class="login">
	<p class="login_txt"><img src="/resources/img/login_txt.png" alt="두번 떠나는 새롭고도 놀라운 신한라이프 첫 썸머! 놀라운 Summer"/></p>
	<div class="loginPage">
		<div class="fc_code">
            <asp:TextBox ID="code" runat="server" MaxLength="5" ClientIDMode="Static" placeholder="FC Code No."></asp:TextBox>
		</div>
		
		<div class="fc_pwd">
            <asp:TextBox ID="pwd" runat="server" TextMode="Password" MaxLength="12" ClientIDMode="Static" placeholder="Password"></asp:TextBox>
		</div>
         
		<div class="btn_login">
            <button id="btnLogin" runat="server" onserverclick="btnLogin_ServerClick" onclick="if (!member.login()) { return false; }"><img src="/resources/img/loginBtn.png" alt="로그인"/></button>
		</div>

		<div class="check_group">
			<div class="login_check">
                <input type="checkbox" name="loginChk" id="loginChk">
                <asp:HiddenField ID="remember" runat="server" ClientIDMode="Static" Value="Y" />
                <label for="loginChk">자동 로그인</label>
			</div>
            <ul class="btn_list">
				<li><a href="/member/find_pw/">비밀번호 찾기</a><span>|</span></li>
				<li><a href="/member/regist/">회원가입</a></li>
			</ul>
                
		</div>
	</div>
</body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script>
        $(document).ready(function () {
            $("#auto").on("click", function () {
                if ($(this).is(":checked")) {
                    $("#remember").val("Y");
                } else {
                    $("#remember").val("N");
                }
            });
        });

        var member = {
            login: function () {
                var literal = {
                    code: {
                        selector: $("#code"), required: { message: "FC 코드를 입력해주세요." },
                        length: { value: 5, message: "FC 코드는 숫자 5자리로 입력해주세요." },
                        digit: { message: "FC 코드는 숫자 5자리로 입력해주세요." }
                    },
                    pwd: { selector: $("#pwd"), required: { message: "비밀번호를 입력해주세요." } }
                };

                return $.validate.rules(literal, { mode: 'alert' });
            }
        };
    </script>
</asp:Content>

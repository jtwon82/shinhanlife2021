<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.UserApplication.member.login._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page_login">
        <div id="wrap" class="wrapper">
            <div class="login_title">
                <img src="/resources/img/login.png" alt="슬기로운 썸머생활">
            </div>

            <div class="login_area">
                <div class="fc_code">
                    <asp:TextBox ID="code" runat="server" MaxLength="5" ClientIDMode="Static" placeholder="FC Code No."></asp:TextBox>
                </div>

                <div class="fc_pwd">
                    <asp:TextBox ID="pwd" runat="server" TextMode="Password" MaxLength="12" ClientIDMode="Static" placeholder="Password"></asp:TextBox>
                </div>

                <div class="btn_login">
                    <button id="btnLogin" runat="server" onserverclick="btnLogin_ServerClick" onclick="if (!member.login()) { return false; }"><span>Login</span></button>
                </div>

                <div class="check_group">
                    <div class="login_check">
                        <input type="checkbox" name="auto" id="auto">
                        <asp:HiddenField ID="remember" runat="server" ClientIDMode="Static" Value="N" />
                        <label for="auto">자동 로그인</label>
                    </div>

                    <div class="btn_join">
                        <a href="/member/regist/">회원가입</a>
                    </div>
                </div>
            </div>

            <div class="flogo">
                <img src="/resources/img/flogo.png" alt="">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SAP.Master.WebApplication._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= OrangeSummer.Common.Master.AppSetting.SiteTitle %></title>
    <link href="/common/vendor/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="/common/vendor/jquery/css/jquery-ui.min.css" rel="stylesheet" />
    <link href="/common/css/site.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-image: url('common/images/img-main.jpg'); background-repeat: no-repeat;">
    <form name="form1" runat="server">
        <h3 class="container text-center" style="margin-top: 100px;">
            <!-- img src="common/images/logo.png" class="mb-5" / -->
            <div class="login-box mx-auto text-center">
                <h3><%= OrangeSummer.Common.Master.AppSetting.SiteTitle %></h3>
            </div>
            <br />


            <div class="login-box mx-auto text-center">
                <asp:TextBox ID="id" runat="server" ClientIDMode="Static" CssClass="form-control form-control-lg" placeholder="아이디를 입력해주세요."></asp:TextBox>
                <asp:TextBox ID="pwd" runat="server" ClientIDMode="Static" TextMode="Password" CssClass="form-control form-control-lg my-2" placeholder="비밀번호를 입력해주세요."></asp:TextBox>

                <asp:TextBox ID="pno" type='number' value="" maxlength="11" runat="server" ClientIDMode="Static" CssClass="form-control form-control-lg my-2" placeholder="전화번호를 입력해주세요."></asp:TextBox>
                <button onclick="return O.SendRndNo();" class="btn btn-lg btn-primary btn-block mb-1" value="">인증번호발송</button>
                <asp:TextBox ID="rndNo" type='number' maxlength="4" runat="server" ClientIDMode="Static" CssClass="form-control form-control-lg my-2" placeholder="인증번호를 입력해주세요."></asp:TextBox>
                
                <asp:LinkButton ID="btnLogin" runat="server" OnClick="btnLogin_Click" OnClientClick="if (!submitForm()) { return false; }" CssClass="btn btn-lg btn-primary btn-block mb-2" Text="로그인"></asp:LinkButton>
                <%--<a href="javascript:;" class="text-secondary">아이디 / 비밀번호 찾기</a>--%>
            </div>


            <footer class="footer">
                <div class="container text-center mt-4">
                    <span class="text-muted">Copyright &copy; All Rights Reserved.</span>
                </div>
            </footer>
    </form>
    
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="/common/vendor/jquery/jquery-ui.min.js"></script>
    <script src="/common/vendor/popper/popper.min.js"></script>
    <script src="/common/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="/common/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="/common/vendor/block/jquery.blockUI.min.js"></script>
    <script src="/common/js/jquery-library.js"></script>
    <script src="/common/js/site.js"></script>
    <script>
        $(document).ready(function () {
            $("#id, #pwd").on("keypress", function (e) {
                if (e.keyCode == 13) {
                    <%= this.Page.GetPostBackEventReference(this.btnLogin) %>;
                }
            });
        });

        var UTIL = function () {
        };
        UTIL.prototype = {
            test: function () {

            }, SendRndNo: function () {
                var $pno = $("#pno");
                if ($pno.val() == '') { alert("전화번호를 입력해주세요."); $pno.focus(); return false; }
                
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/api/admin/sendMsg",
                    data: JSON.stringify({ "id":$("#id").val(), "Pno": $pno.val() }),
                    dataType: "json",
                    async: false,
                    success: function (json) {
                        console.log(json);
                        if (json.result == 'SUCCESS') {
                            $pno.attr("readonly", true);
                            alert("문자로 전송된 인증번호를 입력하고 로그인을 진행해주세요.");
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
                return false;

            }, CompareRndNo: function () {
                var $rndNo = $("#rndNo");
                if ($rndNo.val() == '') { alert("인증번호를 입력해주세요."); $rndNo.focus(); return false; }
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/api/admin/compareRndNo",
                    data: JSON.stringify({ "RndNo": $rndNo.val() }),
                    dataType: "json",
                    async: false,
                    success: function (json) {
                        console.log(json);
                        if(json.result=='SUCCESS')
                        {
                            window.next = 'next';
                            alert("인증이 완료되었습니다. 로그인을 해주세요");
                        }
                    },
                    error: function (jqxhr, status, error) {
                        var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                        alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                    }
                })
            }

        }; var O = new UTIL();

        function submitForm() {
            var literal = {
                id: { selector: $("#id"), required: { message: "아이디를 입력해주세요." } },
                pwd: { selector: $("#pwd"), required: { message: "비밀번호를 입력해주세요." } },
                pno: { selector: $("#pno"), required: { message: "전화번호를 입력해주세요." } },
                rndNo: { selector: $("#rndNo"), required: { message: "인증번호를 입력해주세요." } }
                
            };
            return $.validate.rules(literal, { mode: 'alert' });
        }
    </script>
</body>
</html>

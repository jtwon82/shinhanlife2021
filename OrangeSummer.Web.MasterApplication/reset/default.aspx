<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.reset._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= OrangeSummer.Common.Master.AppSetting.SiteTitle %></title>
    <link href="/common/vendor/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="/common/vendor/jquery/css/jquery-ui.min.css" rel="stylesheet" />
    <link href="/common/css/site.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container" style="margin-top: 100px;">
            <asp:HiddenField ID="id" ClientIDMode="Static" runat="server" />
            <div class="login-box mx-auto">
                <div class="card">
                    <div class="card-header text-center font-weight-bold">
                        비밀번호 변경
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label>비밀번호</label>
                            <asp:TextBox ID="upwd1" runat="server" ClientIDMode="Static" TextMode="Password" MaxLength="15" CssClass="form-control form-control-lg"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>비밀번호 확인</label>
                            <asp:TextBox ID="upwd2" runat="server" ClientIDMode="Static" TextMode="Password" MaxLength="15" CssClass="form-control form-control-lg"></asp:TextBox>
                        </div>

                        <p class="text-danger mb-0">* 관리자에의해 강제 비밀번호 변경이 설정되었습니다.</p>
                        <p class="text-danger mb-2">* 비밀변경 후 이용할 수 있습니다.</p>

                        <div class="text-center">
                            <asp:LinkButton ID="btnModify" runat="server" OnClick="btnModify_Click" OnClientClick="if (!member.reset()) { return false; }" ClientIDMode="Static" CssClass="btn btn-primary btn-lg">
                                <i class="material-icons">done_all</i> 변경
                            </asp:LinkButton>
                            <a href="/" class="btn btn-secondary btn-lg">
                                <i class="material-icons">replay</i> 취소
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
        var member = {
            reset: function () {
                var literal = {
                    upwd1: { selector: $("#upwd1"), required: { message: "비밀번호를 입력해주세요." } },
                    upwd2: { selector: $("#upwd2"), required: { message: "비밀번호 확인을 입력해주세요." } },
                    confirm: { selector: $("#upwd2"), compare: { value: $("#upwd1").val(), message: "비밀번호가 일치하지 않습니다.\n다시 확인 후 입력해주세요." } },
                    checker: function () {
                        var upwd = "<%= _pwd %>";
                        var npwd = $("#upwd1").val();
                        if (upwd === npwd) {
                            alert("기존 비밀번호를 다시 사용할 수 없습니다.\n다른 비밀번호를 이용해주세요.");
                            return false;
                        }
                        return true;
                    }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    $.loading.show();
                    return true;
                } else {
                    return false;
                }
            }
        };
    </script>
</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.member.edit._default" %>

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
                <p class="subTitle">회원정보 수정</p>
                <div class="signup_wrap">
                    <p class="title">회원정보 수정</p>

                    <div class="input_wrap">
                        <div class="form_group">
                            <label for="user_name">성명</label>
                            <asp:TextBox ID="user_name" runat="server" ClientIDMode="Static" MaxLength="10" placeholder="실명으로 등록해주세요." class="form_control disabled" disabled="true"></asp:TextBox>
                        </div>

                        <div class="form_group">
                            <label for="user_tel">연락처</label>
                            <asp:TextBox ID="user_tel" runat="server" ClientIDMode="Static" MaxLength="11" placeholder="숫자만 입력" CssClass="form_control edit disabled" disabled="true"></asp:TextBox>
                        </div>

                        <div class="form_group">
                            <label for="user_fccode">FC 코드</label>
                            <asp:TextBox ID="user_fccode" runat="server" ClientIDMode="Static"  placeholder="변경불가 고정내용" CssClass="form_control edit disabled" disabled="true"></asp:TextBox>
                        </div>

                        <div class="form_group">
                            <label for="user_password">비밀번호</label>
                            <asp:TextBox ID="user_password" runat="server" ClientIDMode="Static" TextMode="Password" MaxLength="12" placeholder="숫자 4자리 이상 입력해주세요." CssClass="form_control"></asp:TextBox>
                        </div>

                        <div class="form_group">
                            <label for="user_password_check">비밀번호 확인</label>
                            <asp:TextBox ID="user_password_check" runat="server" ClientIDMode="Static" TextMode="Password" MaxLength="12" placeholder="비밀번호를 재입력해주세요." CssClass="form_control"></asp:TextBox>
                        </div>
                    </div>
                    <asp:HiddenField ID="hidlevel" runat="server" />
                    <asp:HiddenField ID="hidbranch" runat="server" />
                    <button class="btn_signup" id="btnEdit" runat="server" onserverclick="btnEdit_ServerClick" onclick="if (!member.edit()) { return false; }">회원정보수정</button>
                </div>

            </div>
        </div>
    </body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script>
        $(document).ready(function () {
        });

        var member = {
            edit: function () {
                var literal = {
                    //branch: { selector: $("#branch"), required: { message: "지점을 선택해주세요." } },
                    //level: { selector: $("#level"), required: { message: "신분을 선택해주세요." } },
                    user_password: {
                        selector: $("#user_password"), required: { message: "비밀번호를 입력해주세요." },
                        min: { value: 4, message: '비밀번호를 4자 이상으로 입력하세요.' }
                    },
                    user_password_check: { selector: $("#user_password_check"), required: { message: "비밀번호를 재입력해주세요." } },
                    confirm: { selector: $("#user_password_check"), compare: { value: $("#user_password").val(), message: "비밀번호가 일치하지 않습니다.\n다시 확인 후 입력해주세요." } },
                    //name: { selector: $("#name"), required: { message: "이름을 입력해주세요." } }
                    //mobile: {
                    //    selector: $("#mobile"), required: { message: "전화번호를 입력해주세요." },
                    //    digit: { message: "전화번호는 숫자만 입력해주세요." }
                    //}
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    return true;
                }
            }
        };
    </script>
</asp:Content>

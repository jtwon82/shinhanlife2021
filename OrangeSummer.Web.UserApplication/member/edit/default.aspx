<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.UserApplication.member.edit._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page_signup_edit">
        <div id="wrap" class="wrapper">
            <div class="title_wrap">
                <div class="btn_allmenu">
                    <a href="#"><img src="/resources/img/allmenu.png" alt="전체메뉴"></a>
                </div>
                <div class="page_title">
                    회원정보 수정
                </div>

                <a href="/achieve/" class="btn-home">
                    <img src="/resources/img/hub.png" alt="홈으로">
                </a>
            </div>

            <uc1:menu runat="server" ID="menu" />
            <!-- //nav -->

            <div class="signup_edit_banner">
                <img src="/resources/img/signup_edit_banner.png" alt="">
            </div>

            <div class="signup_wrap edit_wrap">
                <p class="title">회원정보 수정</p>

                <div class="form_group select_wrap">
                    <label for="branch">지점</label>
                    <asp:DropDownList ID="branch" runat="server" ClientIDMode="Static" CssClass="form_control" Enabled="false"></asp:DropDownList>
                </div>

                <div class="form_group select_wrap">
                    <label for="level">신분</label>
                    <asp:DropDownList ID="level" runat="server" ClientIDMode="Static" CssClass="form_control" Enabled="false"></asp:DropDownList>
                </div>

                <div class="input_wrap">
                    <div class="form_group">
                        <label>FC 코드</label>
                        <div class="fccode"><%= _code %></div>
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

                <button class="btn_signup_edit" id="btnEdit" runat="server" onserverclick="btnEdit_ServerClick" onclick="if (!member.edit()) { return false; }">회원정보 수정</button>
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
        });

        var member = {
            edit: function () {
                var literal = {
                    branch: { selector: $("#branch"), required: { message: "지점을 선택해주세요." } },
                    level: { selector: $("#level"), required: { message: "신분을 선택해주세요." } },
                    pwd1: {
                        selector: $("#pwd1"), required: { message: "비밀번호를 입력해주세요." },
                        min: { value: 4, message: '비밀번호를 4자 이상으로 입력하세요.' }
                    },
                    pwd2: { selector: $("#pwd2"), required: { message: "비밀번호를 재입력해주세요." } },
                    confirm: { selector: $("#pwd2"), compare: { value: $("#pwd1").val(), message: "비밀번호가 일치하지 않습니다.\n다시 확인 후 입력해주세요." } },
                    name: { selector: $("#name"), required: { message: "이름을 입력해주세요." } }
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

<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.member.terms._default" %>

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
                <p class="subTitle">회원가입 약관</p>

                <div class="signup_wrap terms_wrap">
                    <p class="title">회원가입 약관</p>
                    <div class="form_group form_terms">
                        <p class="term-box"><%= _service %></p>
                    </div>

                    <p class="title">개인정보 처리방침안내</p>
                    <div class="form_group form_terms">
                        <p class="term-box"><%= _person %></p>
                    </div>

                    <a href="/member/regist/" class="btn_signup_terms">회원가입 페이지로 이동</a>

                </div>

            </div>
    </body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>

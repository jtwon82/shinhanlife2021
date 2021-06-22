<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.UserApplication.member.terms._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page_signup_terms">
        <div id="wrap" class="wrapper">
            <div class="title_wrap">
                <div class="btn_allmenu">
                    <a href="#"><img src="/resources/img/allmenu.png" alt="전체메뉴"></a>
                </div>
                <div class="page_title terms_title">
                    회원가입 약관
                </div>
            </div>

            <uc1:menu runat="server" id="menu" />

            <div class="signup_wrap terms_wrap">
                <p class="title">회원가입 약관</p>
                <div class="form_group form_terms">
                    <textarea class="term-box"><%= _service %></textarea>
                </div>

                <p class="title">개인정보 처리방침안내</p>
                <div class="form_group form_terms">
                    <textarea class="term-box"><%= _person %></textarea>
                </div>

                <a href="javascript:history.back();" class="btn_signup_terms">회원가입 페이지로 이동</a>
            </div>
        </div>
        <!-- //wrap -->

        <div class="w_intro_bottom w_only">
            <img src="/resources/img/flogo.png" alt="">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
</asp:Content>

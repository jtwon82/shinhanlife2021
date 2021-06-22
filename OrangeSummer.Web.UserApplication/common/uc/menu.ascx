<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="OrangeSummer.Web.UserApplication.common.uc.menu" %>
<div class="allmenu_wrap">
    <div class="top_area">

        <% 

            if (MLib.Auth.Forms.IsAuthenticated)
            {

        %>
        <div class="name">
            <em><%= OrangeSummer.Common.User.Identify.Name %></em>님
        </div>

        <ul class="office">
            <li><%= OrangeSummer.Common.User.Identify.BranchName %></li>
            <li class="bm"><%= OrangeSummer.Common.User.Identify.Level %></li>
        </ul>

        <div class="btn_area">
            <a href="/member/edit/">내 정보 수정하기</a>
            <asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click" CssClass="btn_logout" Text="로그아웃"></asp:LinkButton>
        </div>
        <%

            }
            else
            {

        %>
        <div class="btn_area">
            <a href="/member/login/">로그인</a>
        </div>
        <% 

            }

        %>
    </div>

    <nav>
        <ul>
            <li>
                <a href="/measure/">시책 안내</a>
            </li>
            <li>
                <a href="/achieve/">업적 페이지</a>
            </li>
            <li>
                <a href="/ranking/">썸머 랭킹</a>
            </li>
            <li>
                <a href="/board/evt/">이벤트</a>
            </li>
            <li>
                <a href="/board/notice/">알림게시판</a>
            </li>
        </ul>
    </nav>

    <button class="allmenu_close">
        <img src="/resources/img/allmenu_close.png" alt="닫기">
    </button>
</div>
<!-- //nav -->

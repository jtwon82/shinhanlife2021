<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.ranking._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />
    <link rel="stylesheet" href="/resources/css/swiper.css" />

    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="/resources/js/swiper.min.4.3.5.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <body>
        <div id="sub_wrap" class="subMeta05">
            <uc1:menu runat="server" ID="menu" />
            <div class="subContainer">
                <p class="subTitle"><img src="/resources/img/sub/rankingTitle.png" alt="랭킹" /></p>
			<ul class="bmTabs measure">
				<li><a href="/ranking" class="current">개인 부문</a></li>
				<li><a href="/ranking/sl">E SL 부문</a></li>
				<li><a href="/ranking/point">지점 부문</a></li>
			</ul>
                
			<%=_person%>



            </div>
        </div>
    </body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<script>
    $(document).ready(function () {
        //$(".rankingBox dl:eq(1)").after($(".rankingBox dl:eq(0)"));
        
    });
</script>
</asp:Content>

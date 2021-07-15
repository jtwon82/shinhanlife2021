<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/reset.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
    <link rel="stylesheet" href="/resources/css/layout.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<body class="login">
<%--	<div id="intro_wrap">
		<p class="intro_img"><img src="/resources/img/intro.jpg" alt="두 번 떠나는 새롭고도 놀라운 신한라이프 첫 썸머 놀라운 Summer jeju&saipan"/></p>
	</div>--%>
</body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<script>
    location.replace('/index/')
</script>
</asp:Content>

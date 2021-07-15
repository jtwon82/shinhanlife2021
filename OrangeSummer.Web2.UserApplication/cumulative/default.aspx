<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.cumulative._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />

    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

<body>
	<div id="sub_wrap" class="subMeta05">
		<uc1:menu runat="server" ID="menu" />
		<div class="subContainer cumulative">
			<p class="subTitle"><img src="/resources/img/sub/processTitle.png" alt="달성 시상금" /></p>
			<div class="cumulativePage">
				<p class="endTxt">시책 진도 현황은<br/>
				썸머 페스티벌 이후인<br/>
				<strong>9월부터 활성화 됩니다.</strong></p>
			</div>
		</div>
	
	</div>
</body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>

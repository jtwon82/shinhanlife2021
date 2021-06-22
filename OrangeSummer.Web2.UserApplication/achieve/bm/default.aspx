<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.achieve.bm._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link rel="stylesheet" href="/resources/css/sub.css" />
<link rel="stylesheet" href="/resources/css/swiper.css" />
<script type="text/javascript" src="/resources/js/swiper3.js"></script>
<script type="text/javascript" src="/resources/js/common.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<body>
	<div id="sub_wrap" class="subMeta02">
		<uc1:menu runat="server" id="menu" />
		<div class="subContainer guide">
			<p class="subTitle">My 업적</p>
			<ul class="bmTabs">
				<li><a href="/achieve/bm" class="current">개인 부문</a></li>
                <%
                    if (OrangeSummer.Common.User.Identify.Level == "SL")
                    {
                %>
                <li><a href="/achieve/sl">SL 부문</a></li>
                <%
                    }
                %>
			</ul>
			<div id="tab-1" class="tab-content current">		
				<div class="swiper-container3">
					<div class="swiper-wrapper">
                        <%=_contents %>
					</div>
					<!-- Add Pagination 
					<div class="swiper-pagination"></div>-->
					<!-- Add Arrows -->
					<div class="swiper-button-next"></div>
					<div class="swiper-button-prev"></div>
				</div>
				<div class="swiper-container3">
					<div class="swiper-wrapper">
                        <%=_contents2 %>
					</div>
					<!-- Add Pagination 
					<div class="swiper-pagination"></div>-->
					<!-- Add Arrows -->
					<div class="swiper-button-next"></div>
					<div class="swiper-button-prev"></div>
				</div>

				<ul class="referenceBox">
					<li>* 본 데이터는 2021 Summer Contest 진도관리를 위한 <br/>보조자료이며, 달성 결과가 아님을 알려드립니다.</li>
					<li>* 7월 11일부터 Ready for Summer 가중치가 반영된 환산 CMIP업적을 확인하실 수있습니다.</li>
					<li>* 자세한 내용은 해당공문을 반드시 참고하시기 바랍니다. </li>
				</ul>
			</div>
		</div>
				
	</div>
</body>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
		<script>
		var swiper = new Swiper('.swiper-container3', {
			initialSlide:'1',
			pagination: '.swiper-pagination',
			paginationClickable: true,
			nextButton: '.swiper-button-next',
			prevButton: '.swiper-button-prev',
			autoplay:false,
			spaceBetween: 30
		});
		</script>
</asp:Content>


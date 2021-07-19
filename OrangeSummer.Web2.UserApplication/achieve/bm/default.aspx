<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.achieve.bm._default" %>
<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link rel="stylesheet" href="/resources/css/sub.css" />
<link rel="stylesheet" href="/resources/css/swiper.css" />
<script type="text/javascript" src="/resources/js/swiper3.js"></script>
<script type="text/javascript" src="/resources/js/common.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<body class="bm">
	<div id="sub_wrap" class="subMeta02">
		<uc1:menu runat="server" id="menu" />
		<div class="subContainer guide">
			<p class="subTitle"><img src="/resources/img/sub/bmTitle.png" alt="My업적" /></p>
			<ul class="bmTabs">
				<li><a href="/achieve/bm" class="current">개인 부문</a></li>
                <%
                    if ( ",SL,E SL,G SL,S SL".Contains(","+OrangeSummer.Common.User.Identify.Level) )
                    {
                %>
                <li><a href="/achieve/sl"><%=OrangeSummer.Common.User.Identify.LevelName %> 부문</a></li>
                <%
                    }
                %>
			</ul>
			<div id="tab-1" class="tab-content current">		
				<div class="swiper-container3 personal">
					<div class="swiper-wrapper">
                        <%=_contents %>
					</div>
				</div>


				<ul class="referenceBox">
					<li>* 본 데이터는 2021 Summer Contest 진도관리를 위한 보조자료이며,<br/>달성 결과가 아님을 알려드립니다.</li>
					<%--<li>* 7월 11일부터 Ready for Summer 가중치가 반영된 환산 CMIP업적을 확인하실 수있습니다.</li>--%>
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
		$('.swiper-container3 .swiper-slide').each(function (id) {
		    if ($(this).hasClass("slide1")) {
		        swiper.slideTo(id, 0, false);
		    }
		});
		</script>
</asp:Content>


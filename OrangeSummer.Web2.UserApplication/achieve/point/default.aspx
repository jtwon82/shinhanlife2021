<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.achieve.point._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/sub.css" />
    <link rel="stylesheet" href="/resources/css/swiper.css" />

    <script type="text/javascript" src="/resources/js/swiper3.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="/resources/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <body class="bm_point">
        <div id="sub_wrap" class="subMeta04">
            <uc1:menu runat="server" ID="menu" />
            <div class="subContainer guide">
                <p class="subTitle"><img src="/resources/img/sub/bmTitle.png" alt="My업적" /></p>
                <ul class="bmTabs">
                    <li><a href="/achieve/point" class="current">지점 부문</a></li>
                </ul>
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

                <!-- Initialize Swiper -->


                <ul class="referenceBox">
                    <li>* 본 데이터는 2021 Summer Contest 진도관리를 위한 보조자료이며,<br/>달성 결과가 아님을 알려드립니다.</li>
				    <%--<li>* 7월 11일부터 Ready for Summer 가중치가 반영된 환산 CMIP업적을 확인하실 수있습니다.</li>--%>
                    <li>* 자세한 내용은 해당공문을 반드시 참고하시기 바랍니다. </li>
                </ul>
            </div>
        </div>
    </body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script>
        var swiper = new Swiper('.swiper-container3', {
            initialSlide: '1',
            pagination: '.swiper-pagination',
            paginationClickable: true,
            nextButton: '.swiper-button-next',
            prevButton: '.swiper-button-prev',
            autoplay: false,
            spaceBetween: 30
        });
    </script>
</asp:Content>

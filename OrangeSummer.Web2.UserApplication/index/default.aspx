<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web2.UserApplication.index._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="/resources/css/index.css?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>" />
    <link rel="stylesheet" href="/resources/css/swiper-bundle.css">

    <script type="text/javascript" src="/resources/js/common.js"></script>
    <script type="text/javascript" src="/resources/js/swiper.min.js"></script>
    <script type="text/javascript" src="/resources/js/swiper-bundle.js"></script>
    <script type="text/javascript" src="/common/js/common.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="/common/js/jquery-library.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript" src="/common/js/site.js?v=<% =DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <body id="main">
	    <div class="background" >
		    <p class="cover"></p>
		    <p class="upload"><img src="<%=_member.BackgroundImg %>" style=""/></p>
		    <p class="topBox"><img src="/resources/img/index/mainTopImg.png" alt=""/></p>
	    </div>
        <div id="main_wrap">
            <uc1:menu runat="server" ID="menu" />
            <div class="main_container">

            <div class="main_personInfo">
               <p class="person_rank"><span class="point"><%=_member.Branch.Name %></span><%=_member.Name %> / <%=_member.LevelName %></p>
               <p class="main_personImg"><img src="<%=_member.ProfileImg %>" onerror="this.src='/resources/img/index/personImg2.jpg'" alt="" class="personImg " style="width:173px; height:173px;"/><span><label for="myfile" class="inputFileButton"><img src="/resources/img/index/fileIcon.png" alt="" /></label><input type="file" id="myfile" name="myfile" style="display:none;" accept="image/*"/></span></p>
               <p class="imgEditBtn"><label for="myfile2" class="inputFileButton2"><img src="/resources/img/index/fileIcon.png" alt="" /></label><input type="file" id="myfile2" name="myfile2" style="display:none;" /></p>
            </div>
                
			<div class="mainEventBanner">
				<p class="mainEventBanner_t">EVENT & NOTICE</p>
				<div class="EventBanner_rolling">
					<!-- Swiper -->
					<div class="swiper-container">
						<div class="swiper-wrapper">
                        <asp:Repeater ID="rptBannerList" runat="server">
                            <ItemTemplate>
                                <div class="swiper-slide"><a href="<%# MLib.Util.Check.IsNone(Eval("Link").ToString()) ? "javascript:;" : Eval("Link").ToString() %>"><img src="<%#OrangeSummer.Common.User.AppSetting.AwsUrl(Eval("AttMobile").ToString()) %>" alt="<%#Eval("Title") %>"/></a></div>
                            </ItemTemplate>
                        </asp:Repeater>
						</div>
						<!-- Add Pagination -->
						<div class="swiper-pagination"></div>
					</div>
				</div>
			</div>

                <dl class="categoryBox">
                    <dt>Category</dt>
                    <dd>
                        <%
                            if (",FC".Contains("," + OrangeSummer.Common.User.Identify.Level))
                            {
                        %>
                        <a href="/achieve/bm"><span></span>SUMMER 업적</a>
                        <%
                            }
                            else if (",SL,E SL,G SL,S SL".Contains("," + OrangeSummer.Common.User.Identify.Level))
                            {
                        %>
                        <a href="/achieve/sl"><span></span>SUMMER 업적</a>
                        <%
                            }
                            else if (",BM,EM,ERM".Contains("," + OrangeSummer.Common.User.Identify.Level))
                            {
                        %>
                        <a href="/achieve/point"><span></span>SUMMER 업적</a>
                        <%
                            }
                            else if (",NEWFC".Contains("," + OrangeSummer.Common.User.Identify.Level))
                            {
                        %>
                        <a href="/achieve/bm"><span></span>SUMMER 업적</a>
                        <%
                            }
                        %></dd>

                    <dd><a href="/measure"><span></span>시책 안내</a></dd>
                    <dd><img src="/resources/img/index/categoryIcon03.png" /></dd>
                    <dd><a href="/ranking"><span></span>SUMMER 랭킹</a></dd>
                    <dd><a href="/board/evt"><span></span>이벤트</a></dd>
                    <dd><a href="/board/notice"><span></span>공지&게시판</a></dd>
                </dl>
            </div>
        </div>
    </body>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script type="text/javascript">
        $("#myfile, #myfile2").on("change", function () {
            var thiss = this;
            console.log(thiss.name);

            if (thiss.files && thiss.files[0]) {
                var file = thiss.files[0];

                var fd = new FormData();
                if (thiss.name == 'myfile') {
                    fd.append('mode', 'PROFILE');
                } else {
                    fd.append('mode', 'BACKGROUND');
                }
                fd.append('file', file);

                $.ajax({
                    url: "/api/member/fileupload",
                    type: "POST",
                    data: fd,
                    dataType: "json",
                    processData: false,  // tell jQuery not to process the data
                    contentType: false   // tell jQuery not to set contentType
                }).done(function (req) {
                    console.log(req);
                    if (req.result != 'SUCCESS') {
                        alert("이미지 업로드에 실패했습니다.");

                    } else {
                        if (fd.get("mode") == "PROFILE") {
                            $(".profile").attr("src", req.url);
                        } else {
                            $(".upload img").attr("src", req.url);
                        }

                        var fd2 = new FormData();
                        fd2.append("mode", fd.get("mode"));
                        fd2.append("url", req.url);
                        $.ajax({
                            url: "/api/member/updateImg",
                            type: "POST",
                            data: { "mode": fd.get("mode"), 'url': req.url },
                            dataType: "json"
                        }).done(function (req) {
                            console.log(req);
                            if (req.result != 'SUCCESS') {
                                alert("이미지 업로드에 실패했습니다.");
                            } else {
                                location.reload();
                            }
                        });
                    }
                });

            }
        })
    </script>
					<!-- Initialize Swiper -->
					<script>
						var swiper = new Swiper('.swiper-container', {
						    e: '',
						    autoplay: {
						        delay: 5000,
						        disableOnInteraction: false,
						    },
						    pagination: {
							el: '.swiper-pagination',
						  },
						});
					</script>
</asp:Content>

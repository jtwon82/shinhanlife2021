<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.UserApplication.travel._default" %>

<%@ Register Src="~/common/uc/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sub_page page_desti">
        <div id="wrap" class="wrapper">
            <div class="title_wrap">
                <div class="btn_allmenu">
                    <a href="#"><img src="/resources/img/allmenu.png" alt="전체메뉴"></a>
                </div>
                <div class="page_title desti_title">
                    여행지 변경하기
                </div>

                <a href="/achieve/" class="btn-home">
                    <img src="/resources/img/hub.png" alt="홈으로">
                </a>
            </div>

            <uc1:menu runat="server" ID="menu" />

            <div class="desti_wrap">
                <p class="title">변경하고 싶은 <span>여행지</span>를 선택하세요!</p>

                <ul class="list_area">
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <li>
                                <input type="radio" name="desti" value="<%# Eval("Id").ToString() %>" <%# Eval("Id").ToString() == _travel ? " checked" : ""  %> />
                                <div class="img">
                                    <img src="<%# OrangeSummer.Common.User.AppSetting.AwsUrl(Eval("AttFile").ToString()) %>" alt="">
                                </div>
                                <span class="txt"><%# Eval("Name") %></span>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>

                <div class="btn_area">
                    <asp:LinkButton ID="btnModify" runat="server" OnClick="btnModify_Click" OnClientClick="if (!travel.modify()) { return false; }" CssClass="btn btn_complete_desti" Text="변경완료"></asp:LinkButton>
                    <a href="/achieve/" class="btn btn_cancel_desti">변경취소</a>
                </div>
            </div>
        </div>
        <!-- //wrap -->

        <div class="w_intro_bottom w_only">
            <img src="/resources/img/flogo.png" alt="">
        </div>
    </div>
    <asp:HiddenField ID="travel" runat="server" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
    <script>
        $(document).ready(function () {
            $(":input:radio[name='desti']").on("click", function () {
                $("#travel").val($(this).val());
            });
        });

        var travel = {
            modify: function () {
                var literal = {
                    travel: { selector: $(":input:radio[name='desti']"), required: { message: "여행지를 선택해주세요." } }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    return true;
                }
            }
        };
    </script>
</asp:Content>

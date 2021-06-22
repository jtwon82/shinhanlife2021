<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.board.agreement._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group">
        <label>- 서비스 이용 약관</label>
        <asp:TextBox ID="service" ClientIDMode="Static" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group">
        <label>- 개인정보 취급방침</label>
        <asp:TextBox ID="person" ClientIDMode="Static" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="form-group">
        <label>- 참여자 모집 및 이벤트 진행 안내 등 문자 수신 동의</label>
        <asp:TextBox ID="marketing" ClientIDMode="Static" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control"></asp:TextBox>
    </div>

    <div class="text-right">
        <asp:LinkButton ID="btnModify" runat="server" OnClick="btnModify_Click"  OnClientClick="if (!data.modify()) { return false; }" ClientIDMode="Static" CssClass="btn btn-warning">
            <i class="material-icons">create</i> 수정
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
    <script>
        var data = {
            modify: function () {
                var literal = {
                    service: { selector: $("#service"), required: { message: "서비스 이용 약관을 입력해주세요." } },
                    person: { selector: $("#person"), required: { message: "개인정보 취급방침을 입력해주세요." } }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    $.loading.show();
                    return true;
                }
            }
        };
    </script>
</asp:Content>

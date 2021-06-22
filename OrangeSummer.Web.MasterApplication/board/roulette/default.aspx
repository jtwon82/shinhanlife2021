<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.board.roulette._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-end">
        <asp:LinkButton ID="btnDownload" runat="server" OnClick="btnDownload_Click" CssClass="btn btn-primary">
            <i class="material-icons">vertical_align_bottom</i> 참여자 다운로드
        </asp:LinkButton>
    </div>

    <p class="mb-1">* 총 <span class="text-danger font-weight-bold"><%= _total %></span>개의 참여가 있습니다.</p>

    <div class="table-responsive">
        <table class="table table-bordered" style="min-width: 1110px;">
            <colgroup>
                <col style="width: 5%;" />
                <col />
                <col style="width: 10%;" />
                <col style="width: 10%;" />
                <col style="width: 15%;" />
                <col style="width: 15%;" />
                <col style="width: 10%;" />
                <col style="width: 10%;" />
            </colgroup>
            <thead>
                <tr>
                    <th>No</th>
                    <th>지점</th>
                    <th>신분</th>
                    <th>코드</th>
                    <th>성명</th>
                    <th>휴대폰번호</th>
                    <th>참여일</th>
                    <th>당첨여부</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# ListNumber(Eval("Total"), Container.ItemIndex) %></td>
                            <td><%# Eval("Branch.Name") %></td>
                            <td><%# Eval("Member.Level") %></td>
                            <td><%# Eval("Member.Code") %></td>
                            <td><%# Eval("Member.Name") %></td>
                            <td><%# Eval("Member.Mobile") %></td>
                            <td><%# Eval("RegistDate") %></td>
                            <td><%# Eval("Result") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr id="noData" runat="server">
                    <td colspan="8">등록된 자료가 없습니다.</td>
                </tr>
            </tbody>
        </table>
    </div>

    <%= _paging %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
</asp:Content>

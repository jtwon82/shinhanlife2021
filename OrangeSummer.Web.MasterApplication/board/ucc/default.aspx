<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.board.ucc._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul class="list-group list-group-horizontal mb-3">
        <asp:Repeater ID="rptRanking" runat="server">
            <ItemTemplate>
                <li class="list-group-item text-center" style="width:33.3%;">
                    <button type="button" class="btn btn-success mr-2"><%# Eval("Row").ToString() %>위</button>
                    <%# Eval("Vote") %>번 <%# OrangeSummer.Common.Code.UccEventName(Eval("Vote").ToString()) %>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>

    <div class="d-flex justify-content-end">
        <asp:LinkButton ID="btnDownload" runat="server" OnClick="btnDownload_Click" CssClass="btn btn-secondary">
            <i class="material-icons">vertical_align_bottom</i> 참여자 다운로드
        </asp:LinkButton>

        <asp:LinkButton ID="btnReply" runat="server" OnClick="btnReply_Click" CssClass="btn btn-secondary ml-2">
            <i class="material-icons">vertical_align_bottom</i> 댓글 다운로드
        </asp:LinkButton>

        <a href="javascript:$.library.popup('reply.aspx', 'UCC_REPLY', 1110, 650);" class="btn btn-secondary ml-2">
            <i class="material-icons">article</i> 댓글 보기
        </a>
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
                    <th>투표번호</th>
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
                            <td><%# Eval("Vote") %></td>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/popup.Master" AutoEventWireup="true" CodeBehind="reply.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.board.word.reply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-bordered" style="min-width: 680px;">
        <colgroup>
            <col style="width: 5%;" />
            <col style="width: 15%;" />
            <col style="width: 10%;" />
            <col />
            <col style="width: 8%;" />
            <col style="width: 10%;" />
            <col style="width: 8%;" />
        </colgroup>
        <thead>
            <tr>
                <th>No</th>
                <th>지점</th>
                <th>이름</th>
                <th>댓글</th>
                <th>좋아요</th>
                <th>등록일</th>
                <th>삭제</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# ListNumber(Eval("Total"), Container.ItemIndex) %></td>
                        <td><%# Eval("Branch.Name") %></td>
                        <td><%# Eval("Member.Name") %></td>
                        <td class="text-left"><%# Depth(Eval("Depth").ToString()) %> <%# Eval("Contents").ToString().Replace(Environment.NewLine, "<br/>") %></td>
                        <td><%# Eval("LikeCount").ToString() %></td>
                        <td><%# Eval("RegistDate").ToString() %></td>
                        <td>
                            <asp:LinkButton ID="btnReplyDelete" runat="server" OnClick="btnReplyDelete_Click" CommandArgument='<%# Eval("Id") %>' OnClientClick="if(!confirm('삭제하시겠습니까?')) { return false; }" CssClass="btn btn-sm btn-secondary">
                                <i class="material-icons">delete</i> 삭제
                            </asp:LinkButton> 
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr id="noData" runat="server">
                <td colspan="7">등록된 댓글이 없습니다.</td>
            </tr>
        </tbody>
    </table>

    <%= _paging %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
</asp:Content>

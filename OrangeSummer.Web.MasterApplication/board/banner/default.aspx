<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.board.banner._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p class="mb-1">* 총 <span class="text-danger font-weight-bold"><%= _total %></span>개의 배너가 있습니다.</p>

    <div class="table-responsive">
        <table class="table table-bordered" style="min-width: 1110px;">
            <colgroup>
                <col style="width: 5%;" />
                <col style="width: 10%;" />
                <col />
                <col style="width: 18%;" />
                <col style="width: 10%;" />
                <col style="width: 10%;" />
                <col style="width: 10%;" />
            </colgroup>
            <thead>
                <tr>
                    <th>No</th>
                    <th>구분(순서)</th>
                    <th>제목</th>
                    <th>노출 기간</th>
                    <th>사용 여부</th>
                    <th>관리자</th>
                    <th>등록일</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# ListNumber(Eval("Total"), Container.ItemIndex) %></td>
                            <td>구분 #<%# Eval("Section") %></td>
                            <td class="text-left"><a href="regist.aspx?id=<%# Eval("Id").ToString() + Parameters() %>"><%# Eval("Title") %></a></td>
                            <td><%# Eval("Sdate").ToString() +" ~ "+ Eval("Edate").ToString() %></td>
                            <td><%# Eval("UseYn").ToString() == "Y" ? "사용" : "미 사용" %></td>
                            <td><%# Eval("Admin.Name") %></td>
                            <td><%# Eval("RegistDate") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr id="noData" runat="server">
                    <td colspan="7">등록된 자료가 없습니다.</td>
                </tr>
            </tbody>
        </table>
    </div>

    <%= _paging %>

    <div class="text-right">
        <a href="regist.aspx" class="btn btn-success">
            <i class="material-icons">add</i> 배너 등록
        </a>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
</asp:Content>

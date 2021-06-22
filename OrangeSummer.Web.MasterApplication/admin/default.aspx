<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.admin._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p class="mb-1">* 총 <span class="text-danger font-weight-bold"><%= _total %></span>개의 관리자가 있습니다.</p>

    <div class="table-responsive">
        <table class="table table-bordered" style="min-width: 1110px;">
            <colgroup>
                <col style="width: 5%;" />
                <col style="width: 15%;" />
                <col style="width: 15%;" />
                <col />
                <col style="width: 25%;" />
                <col style="width: 15%;" />
                <col style="width: 10%;" />
            </colgroup>
            <thead>
                <tr>
                    <th>No</th>
                    <th>아이디</th>
                    <th>이름</th>
                    <th>연락처</th>
                    <th>E-mail</th>
                    <th>상태</th>
                    <th>등록일</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# ListNumber(Eval("Total"), Container.ItemIndex) %></td>
                            <td><a href="regist.aspx?id=<%# Eval("Id").ToString() + Parameters() %>"><%# Eval("Usr") %></a></td>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("Phone") %></td>
                            <td><%# Eval("Email") %></td>
                            <td><%# Eval("UseYn").ToString() == "Y" ? "활성화" : "비활성화" %></td>
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
            <i class="material-icons">add</i> 관리자 등록
        </a>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
</asp:Content>

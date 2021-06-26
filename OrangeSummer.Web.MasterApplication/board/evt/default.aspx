<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.board.evt._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-2">
        <div class="card-body" style="padding: 10px 10px 0px 10px;">
            <div class="form-row">
                <div class="form-group col-md-2">
                    <label>구분</label>
                    <asp:DropDownList ID="type" ClientIDMode="Static" runat="server" CssClass="form-control">
                        <asp:ListItem Text="전체" Value=""></asp:ListItem>
                        <asp:ListItem Text="진행" Value="진행"></asp:ListItem>
                        <asp:ListItem Text="예정" Value="예정"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-2">
                    <label>제목</label>
                    <asp:TextBox ID="title" ClientIDMode="Static" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-2">
                    <label>노출여부</label>
                    <asp:DropDownList ID="use" ClientIDMode="Static" runat="server" CssClass="form-control">
                        <asp:ListItem Text="선택" Value=""></asp:ListItem>
                        <asp:ListItem Text="노출" Value="Y"></asp:ListItem>
                        <asp:ListItem Text="비 노출" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-6">
                    <label>등록일</label>
                    <div class="form-inline">
                        <asp:TextBox ID="sdate" runat="server" ClientIDMode="Static" MaxLength="10" CssClass="form-control datepicker mr-2 mb-2" placeholder="시작일"></asp:TextBox>
                        <asp:TextBox ID="edate" runat="server" ClientIDMode="Static" MaxLength="10" CssClass="form-control datepicker mr-2 mb-2" placeholder="종료일"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="d-flex justify-content-end">
        <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" ClientIDMode="Static" CssClass="btn btn-primary mr-2">
            <i class="material-icons">search</i> 검색
        </asp:LinkButton>
        <a href="./" class="btn btn-secondary">
            <i class="material-icons">refresh</i> 초기화
        </a>
    </div>

    <p class="mb-1">* 총 <span class="text-danger font-weight-bold"><%= _total %></span>개의 이벤트가 있습니다.</p>

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
                    <th>구분</th>
                    <th>제목</th>
                    <th>이벤트 기간</th>
                    <th>노출 여부</th>
                    <th>관리자</th>
                    <th>등록일</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# ListNumber(Eval("Total"), Container.ItemIndex) %></td>
                            <td><%# Eval("Type").ToString() == "NOTICE" ? "Notice" : "일반" %></td>
                            <td class="text-left"><a href="regist.aspx?id=<%# Eval("Id").ToString() + Parameters() %>"><%# Eval("Title") %></a></td>
                            <td><%# Eval("Sdate") %> ~ <%# Eval("Edate") %></td>
                            <td><%# Eval("UseYn").ToString() == "Y" ? "노출" : "비 노출" %></td>
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
            <i class="material-icons">add</i> 이벤트 등록
        </a>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
</asp:Content>

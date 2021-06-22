<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.ranking._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-2">
        <div class="card-body" style="padding: 10px 10px 0px 10px;">
            <div class="form-row">
                <div class="form-group col-md-3">
                    <label>지점</label>
                    <asp:DropDownList ID="branch" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group col-md-3">
                    <label>신분</label>
                    <asp:DropDownList ID="level" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group col-md-3">
                    <label>코드</label>
                    <asp:TextBox ID="code" ClientIDMode="Static" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-3">
                    <label>이름</label>
                    <asp:TextBox ID="name" ClientIDMode="Static" MaxLength="20" runat="server" CssClass="form-control"></asp:TextBox>
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

    <ul class="nav nav-tabs mb-2">
        <li class="nav-item">
            <a class="nav-link<%= (_orderby == "PERSON_RANK") ? " active font-weight-bold" : "" %>" href="./?orderby=PERSON_RANK">CMIP 개인 랭킹</a>
        </li>
        <li class="nav-item">
            <a class="nav-link<%= (_orderby == "PERSON_RANK2") ? " active font-weight-bold" : "" %>" href="./?orderby=PERSON_RANK2">CAMP 개인 랭킹</a>
        </li>
        <li class="nav-item">
            <a class="nav-link<%= (_orderby == "SL_RANK") ? " active font-weight-bold" : "" %>" href="./?orderby=SL_RANK">SL부문 랭킹</a>
        </li>
        <li class="nav-item">
            <a class="nav-link<%= (_orderby == "BRANCH_RANK") ? " active font-weight-bold" : "" %>" href="./?orderby=BRANCH_RANK">지점 랭킹</a>
        </li>
    </ul>
    <p class="mb-1">* 총 <span class="text-danger font-weight-bold"><%= _total %></span>개의 랭킹이 있습니다.</p>
    <div class="table-responsive">
        <table class="table table-bordered" style="min-width: 1500px;">
            <colgroup>
                <col style="width: 5%;" />
                <col />
                <col style="width: 8%;" />
                <col style="width: 8%;" />
                <col style="width: 6%;" />
                <col style="width: 8%;" />
                <col style="width: 8%;" />
                <col style="width: 8%;" />
                <col style="width: 8%;" />
                <col style="width: 8%;" />
                <col style="width: 6%;" />
                <col style="width: 6%;" />
                <col style="width: 6%;" />
                <col style="width: 8%;" />
            </colgroup>
            <thead>
                <tr>
                    <th>No</th>
                    <th>지점</th>
                    <th>신분</th>
                    <th>코드</th>
                    <th>성명</th>
                    <!--<th>개인부문<br />누적건수</th>-->
                    <th>개인부문<br />누적 환산 CMIP</th>
                    <th>개인부문<br />누적 환산 CAMP</th>
                    <th>SL부문<br />누적 환산 CMIP</th>
                    <th>지점부문<br />누적 환산 CMIP</th>
                    <th>개인순위<br />CMIP</th>
                    <th>개인순위<br />CAMP</th>
                    <th>SL부문<br />순위</th>
                    <th>지점부문<br />순위</th>
                    <th>데이터<br />업데이트 일자</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# ListNumber(Eval("Total"), Container.ItemIndex) %></td>
                            <td><%# Eval("Branch.Name") %></td>
                            <td><%# OrangeSummer.Common.Code.MemberLevelName(Eval("Level").ToString()) %></td>
                            <td><%# Eval("Code") %></td>
                            <td><%# Eval("Name") %></td>
                            <!--<td><%# Eval("PersonSum") %></td>-->
                            <td><%# Eval("PersonCmip") %></td>
                            <td><%# Eval("PersonCamp") %></td>
                            <td><%# Eval("SlCmip") %></td>
                            <td><%# Eval("BranchCmip") %></td>
                            <td><%# Eval("PersonRank") %></td>
                            <td><%# Eval("PersonRank2") %></td>
                            <td><%# Eval("SlRank") %></td>
                            <td><%# Eval("BranchRank") %></td>
                            <td><%# Eval("Date") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr id="noData" runat="server">
                    <td colspan="14">등록된 자료가 없습니다.</td>
                </tr>
            </tbody>
        </table>
    </div>

    <%= _paging %>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
</asp:Content>

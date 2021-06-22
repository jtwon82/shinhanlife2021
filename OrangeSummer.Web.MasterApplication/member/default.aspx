<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.member._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card mb-2">
        <div class="card-body" style="padding: 10px 10px 0px 10px;">
            <div class="form-row">
                <div class="form-group col-md-3 mb-1">
                    <label>지점</label>
                    <asp:DropDownList ID="branch" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group col-md-3 mb-1">
                    <label>신분</label>
                    <asp:DropDownList ID="level" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group col-md-3 mb-1">
                    <label>코드</label>
                    <asp:TextBox ID="code" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-3 mb-1">
                    <label>휴대폰 번호</label>
                    <asp:TextBox ID="mobile" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-row">
                <div class="form-group col-md-4 mb-0">
                    <label>가입일</label>
                    <div class="form-inline">
                        <asp:TextBox ID="sdate" runat="server" ClientIDMode="Static" MaxLength="10" CssClass="form-control datepicker mr-2 mb-2" placeholder="시작일"></asp:TextBox>
                        <asp:TextBox ID="edate" runat="server" ClientIDMode="Static" MaxLength="10" CssClass="form-control datepicker mr-2 mb-2" placeholder="종료일"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-8"></div>
            </div>
        </div>
    </div>

    <div class="d-flex justify-content-end">
        <asp:LinkButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" ClientIDMode="Static" CssClass="btn btn-primary">
            <i class="material-icons">search</i> 검색
        </asp:LinkButton>
        <a href="./" class="btn btn-secondary ml-2">
            <i class="material-icons">refresh</i> 초기화
        </a>
        <asp:LinkButton ID="btnExcel" runat="server" OnClick="btnExcel_Click" ClientIDMode="Static" CssClass="btn btn-primary ml-2">
            <i class="material-icons">vertical_align_bottom</i> 엑셀다운로드
        </asp:LinkButton>
    </div>

    <p class="mb-1">* 총 <span class="text-danger font-weight-bold"><%= _total %></span>개의 회원이 있습니다.</p>

    <div class="table-responsive">
        <table class="table table-bordered" style="min-width: 1110px;">
            <colgroup>
                <col style="width: 5%;" />
                <col />
                <col style="width: 12%;" />
                <col style="width: 12%;" />
                <col style="width: 12%;" />
                <col style="width: 12%;" />
                <!--<col style="width: 10%;" />-->
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
                    <!--<th>활성여부</th>-->
                    <th>가입일</th>
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
                            <td><a href="detail.aspx?id=<%# Eval("Id").ToString() + Parameters() %>"><%# Eval("Name") %></a></td>
                            <td><%# Eval("Mobile") %></td>
                            <!--<td><%# Eval("DelYn").ToString() == "Y" ? "비활성" : "활성" %></td>-->
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.achievement._default" %>

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

    <p class="mb-1">* 총 <span class="text-danger font-weight-bold"><%= _total %></span>개의 업적이 있습니다.</p>

    <div class="table-responsive">
        <table class="table table-bordered" style="min-width: 1500px;">
            <colgroup>
                <col style="width: 5%;" />
                <col />
                <col style="width: 8%;" />
                <col style="width: 8%;" />
                <col style="width: 8%;" />
                <col style="width: 6%;" />
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
                    <th>개인부문<br />누적환산CMIP</th>
                    <th>개인부문<br />누적원CANP</th>
                    <th>SL부문<br />환산 CMIP</th>
                    <th>지점부문<br /> 환산 CMIP</th>
                    <th>개인부문<br /> 순위</th>
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
                            <td><%# Eval("PersonCmip") %></td>
                            <td><%# Eval("PersonCamp") %></td>
                            <td><%# Eval("SlCmip") %></td>
                            <td><%# Eval("BranchCmip") %></td>
                            <td><%# Eval("PersonRank") %></td>
                            <td><%# Eval("SlRank") %></td>
                            <td><%# Eval("BranchRank") %></td>
                            <td><%# Eval("Date") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr id="noData" runat="server">
                    <td colspan="13">등록된 자료가 없습니다.</td>
                </tr>
            </tbody>
        </table>
    </div>

    <%= _paging %>

    <div class="text-right">
        <a href="javascript:;" class="btn btn-success" data-toggle="modal" data-target="#modal-excel">
            <i class="material-icons">add</i> Excel Upload(업적 업로드)
        </a>
    </div>

    <div class="modal fade" id="modal-excel" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title font-weight-bold" id="exampleModalLabel">누적 업적 업로드</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>엑셀 데이터를 누적 업적으로 전송합니다.</p>

                    <div class="form-group">
                        <label class="col-form-label">첨부 파일</label>
                        <div class="custom-file">
                            <asp:FileUpload ID="attfile" ClientIDMode="Static" runat="server" AllowMultiple="true" CssClass="custom-file-input" lang="ko" />
                            <label class="custom-file-label" for="attfile">0 개</label>
                        </div>

                        <small class="form-text text-muted">* 엑셀 파일만 업로드 가능하며, 가이드라인에 맞춰 업로드 바랍니다.</small>
                        <small class="form-text text-muted">* 파일 업로드 시 전체 적용되오니  업로드 전 신중한 확인 후 업로드 바랍니다.</small>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnExcel" runat="server" OnClick="btnExcel_Click" OnClientClick="if (!data.regist()) { return false; }" ClientIDMode="Static" CssClass="btn btn-success">
                        <i class="material-icons">add</i> 업적 업로드
                    </asp:LinkButton>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal"><i class="material-icons">close</i> 닫기</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
    <script>
        var data = {
            regist: function () {
                var literal = {
                    attfile: { selector: $("#attfile"), required: { message: "엑셀파일을 선택해주세요." } }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    //$.loading.show();
                    $('div.modal-content').block({ message: null });
                    return true;
                }
            }
        };
    </script>
</asp:Content>

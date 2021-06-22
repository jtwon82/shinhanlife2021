<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="regist.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.board.banner.regist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-responsive">
        <table class="table table-bordered" style="min-width: 1110px;">
            <colgroup>
                <col style="width: 15%;" />
                <col style="width: 35%;" />
                <col style="width: 15%;" />
                <col />
            </colgroup>
            <tbody>
                <tr>
                    <th>구분</th>
                    <td colspan="3" class="text-left">
                        <asp:DropDownList ID="section" ClientIDMode="Static" runat="server" CssClass="form-control w-20"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>제목</th>
                    <td colspan="3" class="text-left"><asp:TextBox ID="title" ClientIDMode="Static" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>배너 이미지</th>
                    <td colspan="3" class="text-left">
                        <div class="custom-file">
                            <asp:FileUpload ID="pc" runat="server" ClientIDMode="Static" CssClass="custom-file-input" />
                            <label class="custom-file-label" for="pc">선택된 파일 없음</label>
                        </div>
                        <small class="text-muted">* 이미지 사이즈는 가로 680px 입니다.</small>
                        <p class="m-0"><asp:Image ID="ipc" runat="server" ClientIDMode="Static" Width="100" Visible="false" CssClass="img-thumbnail" /></p>
                        <asp:HiddenField ID="pced" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
                <tr>
                    <th>LINK</th>
                    <td colspan="3" class="text-left"><asp:TextBox ID="link" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>노출 기간</th>
                    <td colspan="3" class="text-left">
                        <div class="form-inline">
                            <asp:TextBox ID="sdate" runat="server" ClientIDMode="Static" MaxLength="10" CssClass="form-control datepicker" style="width:100px;"></asp:TextBox>
                            <span class="mx-2"> - </span>
                            <asp:TextBox ID="edate" runat="server" ClientIDMode="Static" MaxLength="10" CssClass="form-control datepicker" style="width:100px;"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th>사용 여부</th>
                    <td colspan="3" class="text-left">
                        <asp:RadioButtonList ID="useyn" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Text="사용" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="미사용" Value="N"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <%

                if (_command == "mod")
                {

                %>
                <tr>
                    <th>등록자</th>
                    <td class="text-left"><%= _admName %></td>
                    <th>등록일</th>
                    <td class="text-left"><%= _registDate %></td>
                </tr>
                <%

                }

                %>
            </tbody>
        </table>
    </div>

    <div class="text-right">
        <asp:LinkButton ID="btnRegist" runat="server" OnClick="btnRegist_Click" OnClientClick="if (!data.regist()) { return false; }" Visible="false" ClientIDMode="Static" CssClass="btn btn-success">
            <i class="material-icons">add</i> 등록
        </asp:LinkButton>

        <asp:LinkButton ID="btnModify" runat="server" OnClick="btnModify_Click" OnClientClick="if (!data.modify()) { return false; }" Visible="false" ClientIDMode="Static" CssClass="btn btn-warning">
            <i class="material-icons">create</i> 수정
        </asp:LinkButton>
        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="if (!data.delete()) { return false; }" Visible="false" ClientIDMode="Static" CssClass="btn btn-danger">
            <i class="material-icons">delete</i> 삭제
        </asp:LinkButton>
        <a href="./?command=list<%= Parameters() %>" class="btn btn-secondary">
            <i class="material-icons">list</i> 목록
        </a>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
    <script>
        var data = {
            regist: function () {
                var literal = {
                    section: { selector: $("#section"), required: { message: "구분을 선택해주세요." } },
                    title: { selector: $("#title"), required: { message: "제목을 입력해주세요." } },
                    pc: { selector: $("#pc"), required: { message: "배너 이미지 이미지를 선택해주세요." } },
                    sdate: { selector: $("#sdate"), required: { message: "노출기간(시작)을 입력해주세요." } },
                    edate: { selector: $("#edate"), required: { message: "노출기간(종료)을 입력해주세요." } },
                    useyn: { selector: $(":input:radio[name='<%= this.useyn.UniqueID %>']"), required: { message: "사용 여부를 선택해주세요." } }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    $.loading.show();
                    return true;
                }
            },
            modify: function () {
                var literal = {
                    section: { selector: $("#section"), required: { message: "구분을 선택해주세요." } },
                    title: { selector: $("#title"), required: { message: "제목을 입력해주세요." } },
                    sdate: { selector: $("#sdate"), required: { message: "노출기간(시작)을 입력해주세요." } },
                    edate: { selector: $("#edate"), required: { message: "노출기간(종료)을 입력해주세요." } },
                    useyn: { selector: $(":input:radio[name='<%= this.useyn.UniqueID %>']"), required: { message: "사용 여부를 선택해주세요." } }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    $.loading.show();
                    return true;
                }
            },
            delete: function () {
                if (confirm("삭제하시겠습니까?")) {
                    return true;
                } else {
                    return false;
                }
            }
        };
    </script>
</asp:Content>

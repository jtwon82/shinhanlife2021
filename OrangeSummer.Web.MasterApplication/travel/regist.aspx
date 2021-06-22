<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="regist.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.travel.regist" %>

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
                        <asp:DropDownList ID="section" runat="server" ClientIDMode="Static" CssClass="form-control w-20"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>제목</th>
                    <td colspan="3" class="text-left"><asp:TextBox ID="title" runat="server" ClientIDMode="Static" MaxLength="100" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>여행지 이미지<br />(아이콘)</th>
                    <td colspan="3" class="text-left">
                        <div class="custom-file">
                            <asp:FileUpload ID="attfile" runat="server" ClientIDMode="Static" CssClass="custom-file-input" />
                            <label class="custom-file-label" for="attfile">선택된 파일 없음</label>
                        </div>
                        <small class="text-muted">* 이미지 사이즈는 178px X 177px 입니다.</small>
                        <p class="m-0"><asp:Image ID="image" runat="server" ClientIDMode="Static" Width="100" Visible="false" CssClass="img-thumbnail" /></p>
                        <asp:HiddenField ID="attfiled" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
                <tr>
                    <th>여행지 이미지<br />(PC배경)</th>
                    <td colspan="3" class="text-left">
                        <div class="custom-file">
                            <asp:FileUpload ID="attpc" runat="server" ClientIDMode="Static" CssClass="custom-file-input" />
                            <label class="custom-file-label" for="attpc">선택된 파일 없음</label>
                        </div>
                        <small class="text-muted">* 이미지 사이즈는 1920px X 2500px 입니다.</small>
                        <p class="m-0"><asp:Image ID="imgpc" runat="server" ClientIDMode="Static" Width="100" Visible="false" CssClass="img-thumbnail" /></p>
                        <asp:HiddenField ID="attpced" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
                <tr>
                    <th>여행지 이미지<br />(Mobile배경)</th>
                    <td colspan="3" class="text-left">
                        <div class="custom-file">
                            <asp:FileUpload ID="attmobile" runat="server" ClientIDMode="Static" CssClass="custom-file-input" />
                            <label class="custom-file-label" for="attmobile">선택된 파일 없음</label>
                        </div>
                        <small class="text-muted">* 이미지 사이즈는 768px X 2500px 입니다.</small>
                        <p class="m-0"><asp:Image ID="imgmobile" runat="server" ClientIDMode="Static" Width="100" Visible="false" CssClass="img-thumbnail" /></p>
                        <asp:HiddenField ID="attmobiled" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
                <tr>
                    <th>여행지 장소명</th>
                    <td colspan="3" class="text-left"><asp:TextBox ID="name" runat="server" ClientIDMode="Static" MaxLength="20" CssClass="form-control"></asp:TextBox></td>
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
                    attfile: { selector: $("#attfile"), required: { message: "여행지 이미지(아이콘)를 선택해주세요." } },
                    attpc: { selector: $("#attpc"), required: { message: "여행지 이미지(PC배경)를 선택해주세요." } },
                    attmobile: { selector: $("#attmobile"), required: { message: "여행지 이미지(Mobile배경)를 선택해주세요." } },
                    name: { selector: $("#name"), required: { message: "여행지 장소명을 입력해주세요." } },
                    useyn: { selector: $(":input:radio[name='<%= this.useyn.UniqueID %>']"), required: { message: "상태를 선택해주세요." } }
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
                    name: { selector: $("#name"), required: { message: "여행지 장소명을 입력해주세요." } },
                    useyn: { selector: $(":input:radio[name='<%= this.useyn.UniqueID %>']"), required: { message: "상태를 선택해주세요." } }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    $.loading.show();
                    return true;
                }
            },
            reset: function () {
                if (confirm("비밀번호를 재설정하시겠습니까?")) {
                    return true;
                } else {
                    return false;
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

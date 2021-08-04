<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.member.detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-responsive">
        <table class="table table-bordered" style="min-width: 1110px;">
            <colgroup>
                <col style="width: 15%;" />
                <col style="width: 35%;"/>
                <col style="width: 15%;" />
                <col />
            </colgroup>
            <tbody>
                <tr>
                    <th>지점</th>
                    <td colspan="3" class="text-left">
                        <asp:DropDownList ID="branch" ClientIDMode="Static" runat="server" CssClass="form-control w-20"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>신분</th>
                    <td class="text-left"><asp:DropDownList ID="level" ClientIDMode="Static" runat="server" CssClass="form-control w-50"></asp:DropDownList></td>
                    <th>코드</th>
                    <td class="text-left"><%= _code %></td>
                </tr>
                <tr>
                    <th>이름</th>
                    <td class="text-left"><%= _name %></td>
                    <th>휴대폰 번호</th>
                    <td class="text-left"><%= _mobile %></td>
                </tr>
                <tr>
                    <th>비밀번호</th>
                    <td colspan='3' class="text-left">
                        <table style="border:0px;width:100%;">
                            <tr><td width="30%">현재비번:<%= _pwd %></td>
                                <td width="30%"><asp:TextBox ID="change_pwd" placeholder="변경비번" ClientIDMode="Static" runat="server" style="width:200px;" CssClass="form-control"></asp:TextBox></td>
                                <td width="30%"><asp:LinkButton ID="btnReset" runat="server" OnClick="btnReset_Click" OnClientClick="if (!data.reset()) { return false; }" CssClass="btn btn-primary ml-3" Text="비밀번호 재설정"></asp:LinkButton></td></tr>
                        </table>
                    </td>
                </tr>
                <!--
                <tr>
                    <th>여행지</th>
                    <td colspan="3" class="text-left"><%= _travel %></td>
                </tr>
                <tr>
                    <th>문자수신</th>
                    <td class="text-left"><%= _advert %></td>
                    <th>가입일</th>
                    <td class="text-left"><%= _registDate %></td>
                </tr>
                <tr>
                    <th>활성여부</th>
                    <td colspan="3" class="text-left">
                        <asp:RadioButtonList ID="delyn" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Text="비활성" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="활성" Value="N"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                -->
                <tr>
                    <th>프로필 사진</th>
                    <td class="text-left"><img src="https://www.shinhanlife-fc.com/<%=_profileimg %>" onerror="this.src='https://www.shinhanlife-fc.com/resources/img/index/default_profile.png'" style="width:171px;height:171px;"/></td>
                    <th>배경 사진</th>
                    <td class="text-left"><img src="https://www.shinhanlife-fc.com/<%=_backbroundimg %>" onerror="this.src='https://www.shinhanlife-fc.com/resources/img/index/backgroundImg.jpg'" style="width:300px;height:300px;"/></td>
                </tr>
            </tbody>
        </table>
    </div>

    <div class="text-right">
        <asp:LinkButton ID="btnModify" runat="server" OnClick="btnModify_Click" OnClientClick="if (!data.modify()) { return false; }" ClientIDMode="Static" CssClass="btn btn-warning">
            <i class="material-icons">create</i> 수정
        </asp:LinkButton>
        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="if (!data.delete()) { return false; }" ClientIDMode="Static" CssClass="btn btn-warning">
            <i class="material-icons">create</i> 삭제
        </asp:LinkButton>
        <a href="./?command=list<%= Parameters() %>" class="btn btn-secondary">
            <i class="material-icons">list</i> 목록
        </a>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
    <script>
        var data = {
            modify: function () {
                var literal = {
                    branch: { selector: $("#branch"), required: { message: "지점을 선택해주세요." } },
                    level: { selector: $("#level"), required: { message: "신분을 선택해주세요." } }
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
            },
            reset: function () {
                var literal = {
                    change_pwd: { selector: $("#change_pwd").val() },
                };
                $.validate.rules(literal);

                if (confirm("비밀번호를 재설정하시겠습니까?")) {
                    return true;
                } else {
                    return false;
                }
            },
        };
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="regist.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.branch.regist" %>

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
                    <th>지점</th>
                    <td colspan="3">
                        <div class="form-inline">
                            <asp:TextBox ID="name" runat="server" ClientIDMode="Static" MaxLength="20" CssClass="form-control w-20 mx-1" placeholder="신규 지점명(여백없이 작성)"></asp:TextBox>
                            <% 

                            if (_command == "add")
                            {

                            %>
                            <a href="javascript:data.check();" id="name-check" class="btn btn-primary ml-1">중복확인</a>
                            <% 

                            }

                            %>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th>여행지</th>
                    <td colspan="3">
                        <asp:DropDownList ID="fktravel" runat="server" ClientIDMode="Static" CssClass="form-control w-20"></asp:DropDownList>
                    </td>
                </tr>
                <% 

                if (_command == "mod")
                {

                %>
                <tr>
                    <th>활성여부</th>
                    <td colspan="3" class="text-left">
                        <asp:RadioButtonList ID="delyn" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Text="비활성" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="활성" Value="N"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
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
        <a href="./?command=list<%= Parameters() %>" class="btn btn-secondary">
            <i class="material-icons">list</i> 목록
        </a>
    </div>

    <input type="hidden" id="check" value="N" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
    <script>
        var data = {
            common: function () {
                var literal = {
                    name: { selector: $("#name"), required: { message: "지점명을 입력해주세요." } },
                    check: function () {
                        var pattern = /[\s]/g;
                        if (pattern.test($("#name").val()) === true) {
                            alert("지점명은 공백을 입력할 수 없습니다.");
                            $("#name").focus();
                            return false;
                        }
                        return true;
                    }
                };

                return $.validate.rules(literal, { mode: "alert" });
            },
            regist: function () {
                var literal = {
                    common: function () {
                        return data.common();
                    },
                    checker: function () {
                        if ($("#check").val() === "N") {
                            alert("지점명 중복확인을 해주세요.");
                            return false;
                        }
                        return true;
                    },
                    travel: { selector: $("#fktravel"), required: { message: "여행지를 선택해주세요." } }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    $.loading.show();
                    return true;
                }
            },
            modify: function () {
                var literal = {
                    travel: { selector: $("#fktravel"), required: { message: "여행지를 선택해주세요." } }
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
            check: function () {
                if (data.common()) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/api/branch/check",
                        data: JSON.stringify({ "name": $("#name").val() }),
                        dataType: "json",
                        async: false,
                        success: function (json) {
                            if (json != null) {
                                alert(json.message);
                                if (json.result === "SUCCESS") {
                                    $("#name").prop("readonly", true);
                                    $("#name-check").addClass("disabled");
                                    $("#check").val("Y");
                                } else {
                                    $("#name").val("");
                                    $("#check").val("N");
                                }
                            } else {
                                alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.");
                                return false;
                            }
                        },
                        error: function (jqxhr, status, error) {
                            var err = "[" + jqxhr.status + "] " + jqxhr.statusText;
                            alert("오류가 발생했습니다.\n새로고침 후 다시 시도해주세요.\n" + err);
                        }
                    });
                }
            }
        };
    </script>
</asp:Content>

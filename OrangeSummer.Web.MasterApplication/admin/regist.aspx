<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="regist.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.admin.regist" %>

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
                    <th>상태</th>
                    <td colspan="3" class="text-left">
                        <asp:DropDownList ID="useyn" ClientIDMode="Static" runat="server" CssClass="form-control w-20">
                            <asp:ListItem Text="선택" Value=""></asp:ListItem>
                            <asp:ListItem Text="활성화" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="비활성화" Value="N"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>이름</th>
                    <td colspan="3" class="text-left"><asp:TextBox ID="name" ClientIDMode="Static" runat="server" MaxLength="20" CssClass="form-control w-20"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>아이디</th>
                    <td colspan="3" class="text-left" style="">
                        <% 

                            if (_command == "add")
                            {

                        %>
                        <div class="form-inline">
                            <asp:TextBox ID="usr" runat="server" ClientIDMode="Static" MaxLength="20" CssClass="form-control w-20" placeholder=""></asp:TextBox>
                            <a href="javascript:data.usrcheck();" id="usr-check" class="btn btn-primary ml-1">중복확인</a>
                        </div>
                        <% 

                            }
                            else
                            {

                        %>
                        <input type="text" name="usr" value="<%= _usr %>" class="form-control w-20" disabled />
                        <%

                            }

                        %>
                    </td>
                </tr>
                <% 

                if (_command == "add")
                {

                %>
                <tr>
                    <th>비밀번호</th>
                    <td>
                        <asp:TextBox ID="pwd1" ClientIDMode="Static" TextMode="Password" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox></td>
                    <th>비밀번호 확인</th>
                    <td><asp:TextBox ID="pwd2" ClientIDMode="Static" TextMode="Password" MaxLength="15" runat="server" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <% 

                }
                else
                { 
                    
                %>
                <tr>
                    <th>비밀번호 재설정</th>
                    <td colspan="3" class="text-left">
                        <asp:LinkButton ID="btnReset" runat="server" OnClick="btnReset_Click" OnClientClick="if (!data.reset()) { return false; }" CssClass="btn btn-primary" Text="비밀번호 재설정"></asp:LinkButton>
                    </td>
                </tr>
                <%

                }

                %>
                <tr>
                    <th>연락처</th>
                    <td><asp:TextBox ID="phone" ClientIDMode="Static" MaxLength="11" placeholder="숫자만 입력" runat="server" CssClass="form-control"></asp:TextBox></td>
                    <th>E-mail</th>
                    <td><asp:TextBox ID="email" ClientIDMode="Static" MaxLength="30" runat="server" CssClass="form-control"></asp:TextBox></td>
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

    <input type="hidden" id="check" value="N" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
    <script>
        var data = {
            common: function () {
                var literal = {
                    usr: {
                        selector: $("#usr"), required: { message: "아이디를 입력해주세요." },
                        min: { value: 4, message: "최소 4자이상 입력해주세요." },
                        regex: { value: /^[a-z0-9]/g, message: "아이디를 영문소문자, 숫자로만 앱력해주세요." }
                    }
                };

                return $.validate.rules(literal, { mode: "alert" });
            },
            regist: function () {
                var literal = {
                    useyn: { selector: $("#useyn"), required: { message: "상태를 선택해주세요." } },
                    name: { selector: $("#name"), required: { message: "이름을 입력해주세요." } },
                    common: function () {
                        return data.common();
                    },
                    checker: function () {
                        if ($("#check").val() === "N") {
                            alert("아이디 중복확인을 해주세요.");
                            return false;
                        }
                        return true;
                    },
                    upwd1: {
                        selector: $("#pwd1"), required: { message: "비밀번호를 입력해주세요." },
                        min: { value: 4, message: "비밀번호는 최소 4자 이상으로 입력하세요." }
                    },
                    upwd2: {
                        selector: $("#pwd2"), required: { message: "비밀번호를 입력해주세요." },
                        min: { value: 4, message: "비밀번호는 최소 4자 이상으로 입력하세요." }
                    },
                    confirm: { selector: $("#pwd2"), compare: { value: $("#pwd1").val(), message: "비밀번호가 일치하지 않습니다.\n다시 확인 후 입력해주세요." } },
                    phone: { selector: $("#phone"), required: { message: "연락처를 입력해주세요." } },
                    email: { selector: $("#email"), email: { message: "이메일형식을 확인해주세요." } }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    $.loading.show();
                    return true;
                }
            },
            modify: function () {
                var literal = {
                    useyn: { selector: $("#useyn"), required: { message: "상태를 선택해주세요." } },
                    name: { selector: $("#name"), required: { message: "이름을 입력해주세요." } },
                    phone: { selector: $("#phone"), required: { message: "연락처를 입력해주세요." } },
                    email: { selector: $("#email"), email: { message: "이메일형식을 확인해주세요." } }
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
            },
            usrcheck: function () {
                if (data.common()) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/api/admin/check",
                        data: JSON.stringify({ "usr": $("#usr").val() }),
                        dataType: "json",
                        async: false,
                        success: function (json) {
                            if (json != null) {
                                alert(json.message);
                                if (json.result === "SUCCESS") {
                                    $("#usr").prop("readonly", true);
                                    $("#usr-check").addClass("disabled");
                                    $("#check").val("Y");
                                } else {
                                    $("#usr").val("");
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

<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="regist.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.board.evt.regist" %>

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
                    <td class="text-left"<%= (_command == "add") ? " colspan=\"3\"" : "" %>>
                        <asp:DropDownList ID="type" ClientIDMode="Static" runat="server" CssClass="form-control">
                            <asp:ListItem Text="선택" Value=""></asp:ListItem>
                            <asp:ListItem Text="일반" Value="NORMAL"></asp:ListItem>
                            <asp:ListItem Text="Notice" Value="NOTICE"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <%

                    if (_command == "mod")
                    {

                    %>
                    <th>조회수 / 댓글</th>
                    <td class="text-left">
                        <%= _view %>회 / <%= _reply %>건 
                        <asp:LinkButton ID="btnDownload" runat="server" OnClick="btnDownload_Click" CssClass="btn btn-sm btn-primary ml-2">
                            <i class="material-icons" style="font-size:small;">vertical_align_bottom</i> 댓글 다운로드
                        </asp:LinkButton>
                    </td>
                    <%

                    }

                    %>
                </tr>
                <tr>
                    <th>제목</th>
                    <td colspan="3" class="text-left"><asp:TextBox ID="title" ClientIDMode="Static" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>텍스트</th>
                    <td colspan="3" class="text-left">
                        <div id="editor-area"></div>
                        <asp:HiddenField ID="contents" ClientIDMode="Static" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>이벤트 기간</th>
                    <td colspan="3" class="text-left">
                        <div class="form-inline">
                            <asp:TextBox ID="sdate" runat="server" ClientIDMode="Static" MaxLength="10" CssClass="form-control datepicker" style="width:100px;"></asp:TextBox>
                            <span class="mx-2"> - </span>
                            <asp:TextBox ID="edate" runat="server" ClientIDMode="Static" MaxLength="10" CssClass="form-control datepicker" style="width:100px;"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th>노출 여부</th>
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

    <div class="text-right mb-3">
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

    <% 

    if (_command == "mod")
    {

    %>
    <div class="table-responsive">
        <table class="table table-bordered" style="min-width: 1110px;">
            <colgroup>
                <col style="width: 5%;" />
                <col style="width: 15%;" />
                <col style="width: 10%;" />
                <col />
                <col style="width: 8%;" />
                <col style="width: 10%;" />
                <col style="width: 8%;" />
            </colgroup>
            <thead>
                <tr>
                    <th>No</th>
                    <th>지점</th>
                    <th>이름</th>
                    <th>댓글</th>
                    <th>좋아요</th>
                    <th>등록일</th>
                    <th>삭제</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# ListNumber(Eval("Total"), Container.ItemIndex) %></td>
                            <td><%# Eval("Branch.Name") %></td>
                            <td><%# Eval("Member.Name") %></td>
                            <td class="text-left"><%# Depth(Eval("Depth").ToString()) %> <%# Eval("Contents").ToString().Replace(Environment.NewLine, "<br/>") %></td>
                            <td><%# Eval("LikeCount").ToString() %></td>
                            <td><%# Eval("RegistDate").ToString() %></td>
                            <td>
                                <asp:LinkButton ID="btnReplyDelete" runat="server" OnClick="btnReplyDelete_Click" CommandArgument='<%# Eval("Id") %>' OnClientClick="if(!confirm('삭제하시겠습니까?')) { return false; }" CssClass="btn btn-sm btn-secondary">
                                    <i class="material-icons">delete</i> 삭제
                                </asp:LinkButton> 
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr id="noData" runat="server">
                    <td colspan="7">등록된 댓글이 없습니다.</td>
                </tr>
            </tbody>
        </table>
    </div>

    <%= _paging %>
    <% 

    }

    %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder1" runat="server">
    <link href="/common/summernote/summernote-bs4.min.css" rel="stylesheet" />
    <script src="/common/summernote/summernote-bs4.min.js"></script>
    <script src="/common/summernote/lang/summernote-ko-KR.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#editor-area").summernote({
                lang: "ko-KR",
                height: 250,
                callbacks: {
                    onImageUpload: function (files) {
                        uploadImageFile(files[0], this);
                    }
                },
                tooltip: false
            });

            $("#editor-area").summernote("code", $("#contents").val());
        });

        function uploadImageFile(file, editor) {
            var data = new FormData();
            data.append("file", file);

            $.ajax({
                data: data,
                type: "POST",
                url: "/api/editor/upload",
                contentType: false,
                processData: false,
                success: function (json) {
                    console.log(json);
                    if (json.result === "SUCCESS") {
                        $(editor).summernote('insertImage', json.url);
                    } else {
                        alert("업로드에 실패했습니다.");
                        return false;
                    }
                }
            });
        }

        var data = {
            regist: function () {
                var literal = {
                    type: { selector: $("#type"), required: { message: "구분을 선택해주세요." } },
                    title: { selector: $("#title"), required: { message: "제목을 입력해주세요." } },
                    sdate: { selector: $("#sdate"), required: { message: "이벤트 기간(시작)을 입력해주세요." } },
                    edate: { selector: $("#edate"), required: { message: "이벤트 기간(종료)을 입력해주세요." } },
                    useyn: { selector: $(":input:radio[name='<%= this.useyn.UniqueID %>']"), required: { message: "노출 여부를 선택해주세요." } },
                    contents: function () {
                        var code = $("#editor-area").summernote("code");
                        $("#contents").val(code);

                        return true;
                    }
                };

                var checker = $.validate.rules(literal, { mode: "alert" });
                if (checker) {
                    $.loading.show();
                    return true;
                }
            },
            modify: function () {
                var literal = {
                    type: { selector: $("#type"), required: { message: "구분을 선택해주세요." } },
                    title: { selector: $("#title"), required: { message: "제목을 입력해주세요." } },
                    sdate: { selector: $("#sdate"), required: { message: "이벤트 기간(시작)을 입력해주세요." } },
                    edate: { selector: $("#edate"), required: { message: "이벤트 기간(종료)을 입력해주세요." } },
                    useyn: { selector: $(":input:radio[name='<%= this.useyn.UniqueID %>']"), required: { message: "노출 여부를 선택해주세요." } },
                    contents: function () {
                        var code = $("#editor-area").summernote("code");
                        $("#contents").val(code);

                        return true;
                    }
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

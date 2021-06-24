<%@ Page Title="" Language="C#" MasterPageFile="~/common/master/page.Master" AutoEventWireup="true" CodeBehind="regist.aspx.cs" Inherits="OrangeSummer.Web.MasterApplication.measure.regist" %>
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
                        <asp:DropDownList ID="gubun" ClientIDMode="Static" runat="server" CssClass="form-control w-20">
                        <asp:ListItem Text="일반" Value="일반"></asp:ListItem>
                        <asp:ListItem Text="특별" Value="특별"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>조회수</th>
                    <td colspan="3" class="text-left">
                        <asp:Label ID="hitCnt" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>제목</th>
                    <td colspan="3" class="text-left">
                        <asp:TextBox ID="title" ClientIDMode="Static" runat="server" MaxLength="100" CssClass="form-control">
                        </asp:TextBox></td>
                </tr>
                <tr>
                    <th>모바일 시책 배너 이미지</th>
                    <td colspan="3" class="text-left">
                        <div class="custom-file">
                            <asp:FileUpload ID="attMobile" runat="server" ClientIDMode="Static" CssClass="custom-file-input" />
                            <label class="custom-file-label" for="attMobile">선택된 파일 없음</label>
                        </div>
                        <small class="text-muted">* 이미지 사이즈는 가로 680px 입니다.</small>
                        <p class="m-0"><asp:Image ID="iattMobile" runat="server" ClientIDMode="Static" Width="100" Visible="false" CssClass="img-thumbnail" /></p>
                        <asp:HiddenField ID="attMobileed" runat="server" ClientIDMode="Static" />
                    </td>
                </tr>
                <tr>
                    <th>시책 상세 텍스트</th>
                    <td colspan="3" class="text-left">
                        <div id="editor-area"></div>
                        <asp:HiddenField ID="contents" ClientIDMode="Static" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>시책 기간</th>
                    <td colspan="3" class="text-left">
                        <div class="form-inline">
                            <asp:TextBox ID="sdate" runat="server" ClientIDMode="Static" MaxLength="10" CssClass="form-control datepicker2" style="width:100px;"></asp:TextBox>
                            <span class="mx-2"> - </span>
                            <asp:TextBox ID="edate" runat="server" ClientIDMode="Static" MaxLength="10" CssClass="form-control datepicker2" style="width:100px;"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th>노출 여부</th>
                    <td colspan="3" class="text-left">
                        <asp:RadioButtonList ID="useYn" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" RepeatLayout="Flow">
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
    <link href="/common/summernote/summernote-bs4.min.css" rel="stylesheet" />
    <script src="/common/summernote/summernote-bs4.min.js"></script>
    <script src="/common/summernote/lang/summernote-ko-KR.js"></script>
    <script>
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
            $(".datepicker2").datepicker({ minDate: 0 });
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
                    gubun: { selector: $("#gubun"), required: { message: "구분을 선택해주세요." } },
                    title: { selector: $("#title"), required: { message: "제목을 입력해주세요." } },
                    attMobile: { selector: $("#attMobile"), required: { message: "배너이미지를 선택해주세요." } },
                    sdate: { selector: $("#sdate"), required: { message: "노출 기간을 선택해주세요." } },
                    edate: { selector: $("#edate"), required: { message: "노출 기간을 선택해주세요." } },
                    useYn: { selector: $(":input:radio[name='<%= this.useYn.UniqueID %>']"), required: { message: "사용 여부를 선택해주세요." } },
                    contents: function () {
                        var code = $("#editor-area").summernote("code");
                        if (code == "") {
                            alert("이미지나 영상, 텍스트를 등록해주세요.");
                            return false;
                        }
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
                    gubun: { selector: $("#gubun"), required: { message: "구분을 선택해주세요." } },
                    title: { selector: $("#title"), required: { message: "제목을 입력해주세요." } },
                    sdate: { selector: $("#sdate"), required: { message: "노출기간(시작)을 입력해주세요." } },
                    edate: { selector: $("#edate"), required: { message: "노출기간(종료)을 입력해주세요." } },
                    useYn: { selector: $(":input:radio[name='<%= this.useYn.UniqueID %>']"), required: { message: "사용 여부를 선택해주세요." } },
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

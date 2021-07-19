using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Util;
using MLib.Attach;
using MLib.Data;
using OrangeSummer.Common;
using ClosedXML.Excel;
using System.IO;
using MLib.Config;

namespace OrangeSummer.Web.MasterApplication.board.evt
{
    public partial class regist : System.Web.UI.Page
    {
        protected string _command = string.Empty;
        protected string _reply = string.Empty;
        protected string _view = string.Empty;
        protected string _admName = string.Empty;
        protected string _registDate = string.Empty;
        protected string _paging = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageLoad();
            }
        }

        private void PageLoad()
        {
            try
            {
                string id = Check.IsNone(Request["id"], false);
                if (!Check.IsNone(id))
                {
                    Model.Event evt = null;
                    using (Business.Event biz = new Business.Event(Common.Master.AppSetting.Connection))
                    {
                        evt = biz.Detail(id);
                        if (evt != null)
                        {
                            _command = "mod";
                            _reply = Convert.ToDecimal(evt.ReplyCount).ToString("#,##0");
                            _view = Convert.ToDecimal(evt.ViewCount).ToString("#,##0");
                            _registDate = evt.RegistDate;
                            _admName = evt.Admin.Name;

                            Element.Set(this.type, evt.Type);
                            Element.Set(this.title, evt.Title);
                            Element.Set(this.attMobileed, evt.AttImage);
                            Element.Set(this.contents, evt.Contents);
                            Element.Set(this.url_new, evt.Url);
                            Element.Set(this.sdate, evt.Sdate);
                            Element.Set(this.edate, evt.Edate);
                            Element.Set(this.useyn, evt.UseYn);
                            this.type.CssClass = "form-control w-60";

                            this.iattMobile.ImageUrl = Common.Master.AppSetting.uploadFileUrl(evt.AttImage);
                            this.iattMobile.Visible = true;

                            this.btnModify.Visible = true;
                            this.btnDelete.Visible = true;
                        }
                        else
                        {
                            _command = "add";
                            this.btnRegist.Visible = true;
                        }
                    }

                    #region [ 댓글 ]
                    int subpage = Check.IsNone(Request["subpage"], 1);
                    int size = 10;
                    using (Business.EventReply biz = new Business.EventReply(Common.Master.AppSetting.Connection))
                    {
                        List<Model.EventReply> list = biz.List(subpage, size, id);
                        if (list != null)
                        {
                            this.rptList.DataSource = list;
                            this.rptList.DataBind();
                            this.noData.Visible = false;
                            int total = list[0].Total;

                            Common.Master.SubPaging paging = new Common.Master.SubPaging("regist.aspx", subpage, size, 10, total);
                            paging.AddParams("id", id);
                            paging.AddParams("page", Check.IsNone(Request["page"], "1"));
                            paging.AddParams("type", Check.IsNone(Request["type"], ""));
                            paging.AddParams("title", Check.IsNone(Request["title"], ""));
                            paging.AddParams("use", Check.IsNone(Request["use"], ""));
                            paging.AddParams("sdate", Check.IsNone(Request["sdate"], ""));
                            paging.AddParams("edate", Check.IsNone(Request["edate"], ""));

                            _paging = paging.ToString();
                        }
                    }
                    #endregion
                }
                else
                {
                    _command = "add";
                    this.type.CssClass = "form-control w-20";
                    this.btnRegist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                string mobile = string.Empty;
                string attMobileed = Element.Get(this.attMobileed);
                string ext = string.Empty;

                ext = System.IO.Path.GetExtension(this.attMobile.PostedFile.FileName).ToLower();
                if (!Check.IsNone(ext))
                {
                    HttpUpload upload = new HttpUpload(this.attMobile.PostedFile);
                    upload.Attached();
                    if (upload.Result)
                        mobile = upload.FIleFullPath();
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
                }
                else
                {
                    mobile = attMobileed;
                }

                Model.Event evt = new Model.Event();
                evt.Id = Tool.UniqueNewGuid;
                evt.FkAdmin = Common.Master.Identify.Id;
                evt.Type = Element.Get(this.type);
                evt.Title = Element.Get(this.title);
                evt.AttImage = mobile;
                evt.Url = Element.Get(this.url_new) ;
                evt.Contents = Element.Get(this.contents);
                evt.Sdate = Element.Get(this.sdate);
                evt.Edate = Element.Get(this.edate);
                evt.UseYn = Element.Get(this.useyn);
                using (Business.Event biz = new Business.Event(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Regist(evt);
                    if (result)
                        Tool.RR("./");
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

   
        protected void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                string mobile = string.Empty;
                string attMobileed = Element.Get(this.attMobileed);
                string ext = string.Empty;

                ext = System.IO.Path.GetExtension(this.attMobile.PostedFile.FileName).ToLower();
                if (!Check.IsNone(ext))
                {
                    HttpUpload upload = new HttpUpload(this.attMobile.PostedFile);
                    upload.Attached();
                    if (upload.Result)
                        mobile = upload.FIleFullPath();
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
                }
                else
                {
                    mobile = attMobileed;
                }

                string id = Check.IsNone(Request["id"], true);
                Model.Event evt = new Model.Event();
                evt.Id = id;
                evt.Type = Element.Get(this.type);
                evt.Title = Element.Get(this.title);
                evt.AttImage = mobile; ;
                evt.Url = Element.Get(this.url_new);
                evt.Contents = Element.Get(this.contents);
                evt.Sdate = Element.Get(this.sdate);
                evt.Edate = Element.Get(this.edate);
                evt.UseYn = Element.Get(this.useyn);
                using (Business.Event biz = new Business.Event(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Modify(evt);
                    if (result)
                        JS.Move("수정되었습니다.", $"regist.aspx?id={id}{Parameters()}");
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = Check.IsNone(Request["id"], true);
                using (Business.Event biz = new Business.Event(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Delete(id);
                    if (result)
                        Tool.RR($"./?command=list{Parameters()}");
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string id = Check.IsNone(Request["id"], true);
                using (Business.EventReply biz = new Business.EventReply(Common.Master.AppSetting.Connection))
                {
                    List<Model.EventReply> reply = biz.Excel(id);
                    if (reply != null)
                    {
                        string _filename = "이벤트댓글";
                        using (XLWorkbook book = new XLWorkbook())
                        {
                            string path = Path.Combine(Common.Master.AppSetting.Path, "temp", $"{_filename}_{DateTime.Now.ToString("yyyyMMdd")}.xlsx");

                            IXLWorksheet sheet = book.Worksheets.Add(_filename);
                            sheet.Style.Font.FontName = "Malgun Gothic";
                            sheet.Style.Font.FontSize = 10;

                            sheet.Row(1).Height = 20;
                            sheet.Cell("A1").Value = "#";
                            sheet.Cell("B1").Value = "지점";
                            sheet.Cell("C1").Value = "CODE";
                            sheet.Cell("D1").Value = "이름";
                            sheet.Cell("E1").Value = "내용";
                            sheet.Cell("F1").Value = "좋아요";
                            sheet.Cell("G1").Value = "등록일";
                            sheet.Range("A1", "G1").Style.Font.Bold = true;
                            sheet.Range("A1", "G1").Style.Fill.BackgroundColor = XLColor.LightGray;
                            sheet.Range("A1", "G1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            sheet.Column(1).Width = 10;


                            int index = 2;
                            int count = reply.Count;
                            foreach (Model.EventReply item in reply)
                            {
                                sheet.Cell("A" + index).Value = count - (index - 2);
                                sheet.Cell("B" + index).Value = item.Branch.Name;
                                sheet.Cell("C" + index).Value = item.Member.Code;
                                sheet.Cell("D" + index).Value = item.Member.Name;
                                sheet.Cell("E" + index).Value = new System.Text.StringBuilder().Insert(0, "  ", item.Depth).ToString() + item.Contents;
                                sheet.Cell("F" + index).Value = item.LikeCount;
                                sheet.Cell("G" + index).Value = item.RegistDate;
                                sheet.Row(index).Height = 18;
                                index++;
                            }

                            sheet.Columns("A").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Columns("A").AdjustToContents();
                            sheet.Columns("A").Width = 7;

                            sheet.Columns("B").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Columns("B").AdjustToContents();
                            sheet.Columns("B").Width = 30;

                            sheet.Columns("C").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Columns("C").AdjustToContents();
                            sheet.Columns("C").Width = 20;

                            sheet.Columns("D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Columns("D").AdjustToContents();
                            sheet.Columns("D").Width = 20;

                            sheet.Columns("E").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            sheet.Columns("E").AdjustToContents();
                            sheet.Columns("E").Width = 50;

                            sheet.Columns("F").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Columns("F").AdjustToContents();
                            sheet.Columns("F").Width = 10;

                            sheet.Columns("G").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Columns("G").AdjustToContents();
                            sheet.Columns("G").Width = 20;

                            book.SaveAs(path);
                            book.Dispose();

                            using (MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(path)))
                            {
                                System.IO.File.Delete(path);
                                HttpContext.Current.Response.Clear();
                                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + HttpContext.Current.Server.UrlEncode(Path.GetFileName(path)));

                                ms.WriteTo(HttpContext.Current.Response.OutputStream);
                                HttpContext.Current.Response.Flush();
                                HttpContext.Current.Response.End();
                            }
                        }
                    }
                    else
                        JS.Back("다운로드할 댓글이 없습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected string Parameters()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("&page=" + Check.IsNone(Request["page"], "1"));
            sb.Append("&type=" + Check.IsNone(Request["type"], ""));
            sb.Append("&title=" + Check.IsNone(Request["title"], ""));
            sb.Append("&use=" + Check.IsNone(Request["use"], ""));
            sb.Append("&sdate=" + Check.IsNone(Request["sdate"], ""));
            sb.Append("&edate=" + Check.IsNone(Request["edate"], ""));

            return sb.ToString();
        }

        protected void btnReplyDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                string subpage = Check.IsNone(Request["subpage"], "1");
                string id = Check.IsNone(Request["id"], true);
                using (Business.EventReply biz = new Business.EventReply(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Delete(btn.CommandArgument);
                    if (result)
                        Tool.RR($"regist.aspx?id={id}&subpage={subpage}{Parameters()}");
                    else
                        JS.Back("처리중 에러가 발생했습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected string ListNumber(object obj, int index)
        {
            int subpage = Check.IsNone(Request["subpage"], 1);
            int number = (Convert.ToInt32(obj) - 10 * (subpage - 1) - index);
            return number.ToString();
        }

        protected string Depth(string depth)
        {
            int deep = Convert.ToInt32(depth);
            if (deep > 0)
                return new System.Text.StringBuilder().Insert(0, "&nbsp;&nbsp;", deep).ToString() + "<span class=\"material-icons\" style=\"font-size:1rem;\">subdirectory_arrow_right</span>";
            else
                return "";
        }
    }
}
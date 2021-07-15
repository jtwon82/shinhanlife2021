using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Attach;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;
using MLib.Logger;
using MLib.Config;

namespace OrangeSummer.Web.MasterApplication.board.notice
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
                    Model.Notice notice = null;
                    using (Business.Notice biz = new Business.Notice(Common.Master.AppSetting.Connection))
                    {
                        notice = biz.Detail(id);
                        if (notice != null)
                        {
                            _command = "mod";
                            _reply = Convert.ToDecimal(notice.ReplyCount).ToString("#,##0");
                            _view = Convert.ToDecimal(notice.ViewCount).ToString("#,##0");
                            _registDate = notice.RegistDate;
                            _admName = notice.Admin.Name;

                            Element.Set(this.type, notice.Type);
                            Element.Set(this.title, notice.Title);
                            Element.Set(this.contents, notice.Contents);
                            Element.Set(this.useyn, notice.UseYn);
                            Element.Set(this.attfiled, notice.AttFile);
                            Element.Set(this.attfilenamed, notice.AttFileName);

                            if (!Check.IsNone(notice.AttFile))
                            {
                                this.btnDownload.CommandArgument = notice.AttFile;
                                this.btnDownload.CommandName = notice.AttFileName;
                                this.btnDownload.Visible = true;
                                this.btnDelFile.Visible = true;
                            }

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
                    using (Business.NoticeReply biz = new Business.NoticeReply(Common.Master.AppSetting.Connection))
                    {
                        List<Model.NoticeReply> list = biz.List(subpage, size, id);
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
                string filename = string.Empty;
                string file = string.Empty;
                string ext = string.Empty;
                //S3 s3 = new S3(Common.Master.AppSetting.AwsAccess, Common.Master.AppSetting.AwsSecret, Common.Master.AppSetting.AwsBucket);
                //s3.IsPublic = true;

                //ext = System.IO.Path.GetExtension(this.attfile.PostedFile.FileName).ToLower();
                //if (!Check.IsNone(ext))
                //{
                //    int size = this.attfile.PostedFile.ContentLength;
                //    if (size > (10 * 1024 * 1024))
                //        JS.Back("첨부파일은 10MB이하로 해주세요.");

                //    s3.Upload(this.attfile.PostedFile.InputStream, ext);
                //    if (s3.Result)
                //    {
                //        file = s3.Key;
                //        filename = this.attfile.PostedFile.FileName;
                //    }
                //}

                HttpUpload upload = new HttpUpload(this.attfile.PostedFile);
                upload.Attached();
                if (upload.Result)
                {
                    file = upload.FIleFullPath();
                    filename = upload.FileName;
                }

                Model.Notice notice = new Model.Notice();
                notice.Id = Tool.UniqueNewGuid;
                notice.FkAdmin = Common.Master.Identify.Id;
                notice.Type = Element.Get(this.type);
                notice.Title = Element.Get(this.title);
                notice.AttImage = "";
                notice.AttFile = file;
                notice.AttFileName = filename;
                notice.Url = "";
                notice.Contents = Element.Get(this.contents);
                notice.UseYn = Element.Get(this.useyn);
                using (Business.Notice biz = new Business.Notice(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Regist(notice);
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
                string id = Check.IsNone(Request["id"], true);
                string file = string.Empty;
                string filename = string.Empty;
                string attfiled = Element.Get(this.attfiled);
                string attfilenamed = Element.Get(this.attfilenamed);
                string ext = string.Empty;

                ext = System.IO.Path.GetExtension(this.attfile.PostedFile.FileName).ToLower();
                if (!Check.IsNone(ext))
                {
                    int size = this.attfile.PostedFile.ContentLength;
                    if (size > (10 * 1024 * 1024))
                        JS.Back("첨부파일은 10MB이하로 해주세요.");

                    HttpUpload upload = new HttpUpload(this.attfile.PostedFile);
                    upload.Attached();
                    if (upload.Result)
                    {
                        file = upload.FIleFullPath();
                        filename = upload.FileName;
                    }
                }
                else
                {
                    file = attfiled;
                    filename = attfilenamed;
                }

                Model.Notice notice = new Model.Notice();
                notice.Id = id;
                notice.Type = Element.Get(this.type);
                notice.Title = Element.Get(this.title);
                notice.AttImage = "";
                notice.AttFile = file;
                notice.AttFileName = filename;
                notice.Url = "";
                notice.Contents = Element.Get(this.contents);
                notice.UseYn = Element.Get(this.useyn);
                using (Business.Notice biz = new Business.Notice(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Modify(notice);
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
                using (Business.Notice biz = new Business.Notice(Common.Master.AppSetting.Connection))
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
            LinkButton btn = (LinkButton)sender;
            string key = btn.CommandArgument;
            string filename = btn.CommandName;

            string path = MapPath("/upload/" + key);
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.BinaryWrite(bts); Response.Flush(); Response.End();

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
                using (Business.NoticeReply biz = new Business.NoticeReply(Common.Master.AppSetting.Connection))
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
                return new System.Text.StringBuilder().Insert(0, "&nbsp;", deep).ToString() + "<span class=\"material-icons\" style=\"font-size:1rem;\">subdirectory_arrow_right</span>";
            else
                return "";
        }
    }
}
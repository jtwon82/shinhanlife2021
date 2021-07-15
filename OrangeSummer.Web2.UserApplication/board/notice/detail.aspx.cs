using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Attach;
using MLib.Data;
using MLib.Util;
using MLib.Logger;
using MLib.Config;
using System.IO;
using System.Net;
using MLib.Cipher;

namespace OrangeSummer.Web2.UserApplication.board.notice
{
    public partial class detail : System.Web.UI.Page
    {
        protected string _title = string.Empty;
        protected string _contents = string.Empty;
        protected string _attfile = string.Empty;
        protected string _attfilename = string.Empty;
        protected string _adminName = string.Empty;
        protected string _registDate = string.Empty;
        protected string _viewCount = string.Empty;
        protected string _replyCoun = string.Empty;
        protected string _before = string.Empty;
        protected string _next = string.Empty;
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
                string id = Check.IsNone(Request["id"], true);
                string type = Check.IsNone(Request["type"], true);

                Model.Notice notice = null;
                using (Business.Notice biz = new Business.Notice(Common.User.AppSetting.Connection))
                {
                    notice = biz.UserDetail(id);
                    if (notice != null)
                    {
                        _title = notice.Title;
                        _contents = HttpUtility.HtmlDecode(notice.Contents);
                        _attfile = notice.AttFile;
                        _attfilename = notice.AttFileName;
                        _registDate = notice.RegistDate.Substring(0, 10);
                        _viewCount = notice.ViewCount.ToString();
                        _replyCoun = notice.ReplyCount.ToString();
                        _adminName = notice.Admin.Name;
                        
                        MLib.Auth.Web.Cookies("ORANGESUMMER", "NOTICE", AES.Encrypt(Common.User.AppSetting.EncKey, $"{id}|{DateTime.Now.ToString("yyyyMMddHHmmss")}"), 1);

                        notice = biz.UserBefore(id, type);
                        if (notice != null)
                            _before = $"<a href=\"detail.aspx?id={notice.Id}&type={notice.Type}\">{notice.Title}</a>";
                        else
                            _before = "&nbsp;";

                        notice = biz.UserNext(id, type);
                        if (notice != null)
                            _next = $"<a href=\"detail.aspx?id={notice.Id}&type={notice.Type}\">{notice.Title}</a>";
                        else
                            _next = "&nbsp;";

                        #region [ 댓글 ]
                        int subpage = Check.IsNone(Request["subpage"], 1);
                        int size = 10;
                        using (Business.NoticeReply bizNotice = new Business.NoticeReply(Common.User.AppSetting.Connection))
                        {
                            List<Model.NoticeReply> list = bizNotice.UserList(1, (size * subpage), id, Common.User.Identify.Id);
                            if (list != null)
                            {
                                this.rptList.DataSource = list;
                                this.rptList.DataBind();

                                int total = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(list[0].Total) / Convert.ToDouble(size)));
                                if (total > subpage)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append("<div class=\"btn_area page_more\">");
                                    sb.Append($" <a href=\"detail.aspx?id={id}&type={type}{Parameters()}&subpage={subpage + 1}#subpage{subpage + 1}\" class=\"btn_more\">더보기</a>");
                                    sb.Append("</div>");
                                    _paging = sb.ToString();
                                }
                            }
                        }
                        #endregion
                    }
                    else
                        JS.Back("잘못된 접근입니다.");
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

            return sb.ToString();
        }

        protected string AnchorPage()
        {
            string subpage = Check.IsNone(Request["subpage"], "1");
            return $"<a name=\"subpage{subpage}\"></a>";
        }

        protected string Like(string id, string like, string count, string delete)
        {
            StringBuilder sb = new StringBuilder();
            if (delete == "N")
            {
                sb.AppendFormat("<div class=\"like\" id=\"like_{0}\">", id);
                sb.AppendFormat("   <a href=\"javascript:reply.like('{0}');\"{1}>", id, (like != "0") ? " class=\"on\"" : "");
                sb.AppendFormat("       <img src=\"/resources/img/sub/board/{0}.png\" alt=\"\">", like != "0" ? "likeIcon2" : "unlike");
                sb.AppendFormat("   </a>");
                sb.AppendFormat("   <div class=\"number\">LIKE <span>{0}</span></div>", count);
                sb.AppendFormat("</div>");
            }


          

            return sb.ToString();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                string key = btn.CommandArgument;
                string filename = btn.CommandName;

                string secret = MLib.Auth.Web.Cookies("ORANGESUMMER", "NOTICE");
                secret = AES.Decrypt(Common.User.AppSetting.EncKey, secret);
                string id = Tool.Separator(secret, "|", 0);
                Model.Notice notice = null;
                using (Business.Notice biz = new Business.Notice(Common.User.AppSetting.Connection))
                {
                    notice = biz.UserDetail(id);
                    filename = notice.AttFileName;
                    key = notice.AttFile;
                }

                if(notice!= null)
                {
                    string encodefilename = Server.UrlEncode(filename);
                    
                    string path = (ServerVariables.uploadFullPath +"/"+ key);

                    WebClient wc = new WebClient();
                    wc.DownloadFile(ServerVariables.uploadFullPath+"/"+key, ServerVariables.uploadFullPath+"/"+filename);

                    Tool.RR("/upload/" + filename);

                    //Tool.RR("/upload/"+ key);

                    //byte[] bts = System.IO.File.ReadAllBytes(path);

                    //Response.ClearContent();
                    //Response.ClearHeaders();
                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("Cache-Control", "max-age=3");
                    //Response.AddHeader("Pragma", "public");
                    //Response.AddHeader("Content-Type", "Application/pdf");
                    //Response.AddHeader("Content-Length", bts.Length.ToString());
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + bts.Length + "_" + encodefilename);
                    //Response.BinaryWrite(bts); Response.Flush(); Response.End();

                    //using (Stream source = System.IO.File.OpenRead(path))
                    //{
                    //    byte[] buffer = new byte[2048];
                    //    int bytesRead;
                    //    while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                    //    {
                    //        if (Response.IsClientConnected)
                    //        {
                    //            Response.OutputStream.Write(buffer, 0, buffer.Length);
                    //            Response.OutputStream.Flush();
                    //            Response.OutputStream.Close();
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
        private bool DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            bool bImage = response.ContentType.StartsWith("image",
                StringComparison.OrdinalIgnoreCase);
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                bImage)
            {
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = System.IO.File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
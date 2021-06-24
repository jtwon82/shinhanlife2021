using MLib.Attach;
using MLib.Data;
using MLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web.MasterApplication.measure
{
    public partial class regist : System.Web.UI.Page
    {
        protected string _command = string.Empty;
        protected string _admName = string.Empty;
        protected string _registDate = string.Empty;

        private string _type = "EVENT";

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
                //for (int i = 1; i <= 10; i++)
                //    this.section.Items.Add(new ListItem("구분 #" + i.ToString(), i.ToString()));
                //this.section.Items.Insert(0, new ListItem("선택", ""));

                if (!Check.IsNone(id))
                {
                    Model.Measure member = null;
                    using (Business.Measure biz = new Business.Measure(Common.Master.AppSetting.Connection))
                    {
                        member = biz.Detail(id);
                        if (member != null)
                        {
                            _command = "mod";
                            _registDate = member.RegistDate;
                            _admName = member.Admin.Name;

                            Element.Set(this.gubun, member.Gubun);
                            Element.Set(this.title, member.Title);
                            Element.Set(this.hitCnt, member.HitCnt);
                            Element.Set(this.attMobileed, member.attMobile);
                            Element.Set(this.contents, member.Contents);
                            Element.Set(this.sdate, member.sdate);
                            Element.Set(this.edate, member.edate);
                            Element.Set(this.useYn, member.UseYn);

                            this.iattMobile.ImageUrl = Common.Master.AppSetting.uploadFileUrl(member.attMobile);
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
                //int section = Convert.ToInt32(Element.Get(this.section)); ;
                //string pc = string.Empty;
                string mobile = string.Empty;
                string ext = string.Empty;
                
                HttpUpload upload = new HttpUpload(this.attMobile.PostedFile);
                upload.Attached();
                if (upload.Result)
                    mobile = upload.FIleFullPath();
                else
                    JS.Back("처리중 에러가 발생했습니다.");

                Model.Measure banner = new Model.Measure();
                banner.Id = Tool.UniqueNewGuid;
                banner.Gubun = Element.Get(this.gubun);
                banner.Title = Element.Get(this.title);
                banner.attMobile = mobile;
                banner.Contents = HttpContext.Current.Server.UrlEncode(Element.Get(this.contents));
                banner.sdate = Element.Get(this.sdate);
                banner.edate = Element.Get(this.edate);
                banner.UseYn = Element.Get(this.useYn);
                banner.Admin = new Model.Admin()
                {
                    Id = Common.Master.Identify.Id
                };

                using (Business.Measure biz = new Business.Measure(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Regist(banner);
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
                //int section = Convert.ToInt32(Element.Get(this.section)); ;
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

                Model.Measure banner = new Model.Measure();
                banner.Id = id;
                banner.Gubun = Element.Get(this.gubun);
                banner.Title = Element.Get(this.title);
                banner.attMobile = mobile;
                banner.Contents = Element.Get(this.contents);
                banner.sdate = Element.Get(this.sdate);
                banner.edate = Element.Get(this.edate);
                banner.UseYn = Element.Get(this.useYn);
                using (Business.Measure biz = new Business.Measure(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Modify(banner);
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
                using (Business.Measure biz = new Business.Measure(Common.Master.AppSetting.Connection))
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

        /// <summary>
        /// 파라메터 조합
        /// </summary>
        /// <returns></returns>
        protected string Parameters()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("&page=" + Check.IsNone(Request["page"], "1"));

            return sb.ToString();
        }
    }
}
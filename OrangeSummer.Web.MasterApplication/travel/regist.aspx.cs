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
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication.travel
{
    public partial class regist : System.Web.UI.Page
    {
        protected string _command = string.Empty;
        protected string _admName = string.Empty;
        protected string _registDate = string.Empty;
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
                for (int i = 1; i <= 10; i++)
                    this.section.Items.Add(new ListItem("구분 #" + i.ToString(), i.ToString()));
                this.section.Items.Insert(0, new ListItem("선택", ""));

                if (!Check.IsNone(id))
                {
                    Model.Travel travel = null;
                    using (Business.Travel biz = new Business.Travel(Common.Master.AppSetting.Connection))
                    {
                        travel = biz.Detail(id);
                        if (travel != null)
                        {
                            _command = "mod";
                            _registDate = travel.RegistDate;
                            _admName = travel.Admin.Name;

                            Element.Set(this.section, travel.Section.ToString());
                            Element.Set(this.title, travel.Title);
                            Element.Set(this.attfiled, travel.AttFile);
                            Element.Set(this.attpced, travel.AttPc);
                            Element.Set(this.attmobiled, travel.AttMobile);
                            Element.Set(this.name, travel.Name);
                            Element.Set(this.useyn, travel.UseYn);
                            this.image.ImageUrl = Common.Master.AppSetting.AwsUrl(travel.AttFile);
                            this.image.Visible = true;

                            this.imgpc.ImageUrl = Common.Master.AppSetting.AwsUrl(travel.AttPc);
                            this.imgpc.Visible = true;
                            
                            this.imgmobile.ImageUrl = Common.Master.AppSetting.AwsUrl(travel.AttMobile);
                            this.imgmobile.Visible = true;

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
                int section = Convert.ToInt32(Element.Get(this.section)); ;
                using (Business.Travel biz = new Business.Travel(Common.Master.AppSetting.Connection))
                {
                    bool check = biz.Check(section);
                    if (check)
                        JS.Back("기존 사용중인 배너가 있습니다.\\n사용 여부를 미사용으로 교체 후 등록 바랍니다.");
                }

                //S3 s3 = new S3(Common.Master.AppSetting.AwsAccess, Common.Master.AppSetting.AwsSecret, Common.Master.AppSetting.AwsBucket);
                //s3.IsPublic = true;
                string file = string.Empty;
                string pc = string.Empty;
                string mobile = string.Empty;
                string ext = System.IO.Path.GetExtension(this.attfile.PostedFile.FileName).ToLower();
                if (ext != ".jpg" && ext != ".png")
                    JS.Back("jpg, png파일만 업로드 가능합니다.");

                ext = System.IO.Path.GetExtension(this.attpc.PostedFile.FileName).ToLower();
                if (ext != ".jpg" && ext != ".png")
                    JS.Back("jpg, png파일만 업로드 가능합니다.");

                ext = System.IO.Path.GetExtension(this.attmobile.PostedFile.FileName).ToLower();
                if (ext != ".jpg" && ext != ".png")
                    JS.Back("jpg, png파일만 업로드 가능합니다.");

                // icon
                //s3.Upload(this.attfile.PostedFile.InputStream, ext);
                //if (s3.Result)
                //    file = s3.Key;
                //else
                //    JS.Back("처리중 에러가 발생했습니다.");
                HttpUpload upload = new HttpUpload(this.attfile.PostedFile);
                upload.Attached();
                if (upload.Result)
                {
                    file = upload.FIleFullPath();
                }

                // pc
                //s3.Upload(this.attpc.PostedFile.InputStream, ext);
                //if (s3.Result)
                //    pc = s3.Key;
                //else
                //    JS.Back("처리중 에러가 발생했습니다.");
                HttpUpload upload2 = new HttpUpload(this.attpc.PostedFile);
                upload2.Attached();
                if (upload2.Result)
                {
                    pc = upload2.FIleFullPath();
                }

                // mobile
                //s3.Upload(this.attmobile.PostedFile.InputStream, ext);
                //if (s3.Result)
                //    mobile = s3.Key;
                //else
                //    JS.Back("처리중 에러가 발생했습니다.");
                HttpUpload upload3 = new HttpUpload(this.attmobile.PostedFile);
                upload3.Attached();
                if (upload3.Result)
                {
                    mobile = upload3.FIleFullPath();
                }

                Model.Travel travel = new Model.Travel();
                travel.Id = Tool.UniqueNewGuid;
                travel.FkAdmin = Common.Master.Identify.Id;
                travel.Section = section;
                travel.Title = Element.Get(this.title);
                travel.Name = Element.Get(this.name);
                travel.AttFile = file;
                travel.AttPc = pc;
                travel.AttMobile = mobile;
                travel.UseYn = Element.Get(this.useyn);
                using (Business.Travel biz = new Business.Travel(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Regist(travel);
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
                int section = Convert.ToInt32(Element.Get(this.section)); ;
                string file = string.Empty;
                string pc = string.Empty;
                string mobile = string.Empty;
                string attfiled = Element.Get(this.attfiled);
                string attpced = Element.Get(this.attpced);
                string attmobiled = Element.Get(this.attmobiled);
                //S3 s3 = new S3(Common.Master.AppSetting.AwsAccess, Common.Master.AppSetting.AwsSecret, Common.Master.AppSetting.AwsBucket);
                //s3.IsPublic = true;
                
                string ext = System.IO.Path.GetExtension(this.attfile.PostedFile.FileName).ToLower();
                if (!Check.IsNone(ext))
                {
                    if (ext != ".jpg" && ext != ".png")
                        JS.Back("jpg, png파일만 업로드 가능합니다.");

                    //s3.Upload(this.attfile.PostedFile.InputStream, ext);
                    //if (s3.Result)
                    //{
                    //    file = s3.Key;
                    //    s3.Delete(attfiled); // 기존파일 삭제
                    //}
                    //else
                    //    JS.Back("처리중 에러가 발생했습니다.");

                    HttpUpload upload = new HttpUpload(this.attfile.PostedFile);
                    upload.Attached();
                    if (upload.Result)
                    {
                        file = upload.FIleFullPath();
                    }
                }
                else
                    file = attfiled;

                ext = System.IO.Path.GetExtension(this.attpc.PostedFile.FileName).ToLower();
                if (!Check.IsNone(ext))
                {
                    if (ext != ".jpg" && ext != ".png")
                        JS.Back("jpg, png파일만 업로드 가능합니다.");

                    //s3.Upload(this.attpc.PostedFile.InputStream, ext);
                    //if (s3.Result)
                    //{
                    //    pc = s3.Key;
                    //    s3.Delete(attpced); // 기존파일 삭제
                    //}
                    //else
                    //    JS.Back("처리중 에러가 발생했습니다.");
                    HttpUpload upload = new HttpUpload(this.attpc.PostedFile);
                    upload.Attached();
                    if (upload.Result)
                    {
                        pc = upload.FIleFullPath();
                    }
                }
                else
                    pc = attpced;

                ext = System.IO.Path.GetExtension(this.attmobile.PostedFile.FileName).ToLower();
                if (!Check.IsNone(ext))
                {
                    if (ext != ".jpg" && ext != ".png")
                        JS.Back("jpg, png파일만 업로드 가능합니다.");

                    //s3.Upload(this.attmobile.PostedFile.InputStream, ext);
                    //if (s3.Result)
                    //{
                    //    mobile = s3.Key;
                    //    s3.Delete(attmobiled); // 기존파일 삭제
                    //}
                    //else
                    //    JS.Back("처리중 에러가 발생했습니다.");
                    HttpUpload upload = new HttpUpload(this.attmobile.PostedFile);
                    upload.Attached();
                    if (upload.Result)
                    {
                        pc = upload.FIleFullPath();
                    }
                }
                else
                    mobile = attmobiled;

                Model.Travel travel = new Model.Travel();
                travel.Id = id;
                travel.Section = section;
                travel.Title = Element.Get(this.title);
                travel.Name = Element.Get(this.name);
                travel.AttFile = file;
                travel.AttPc = pc;
                travel.AttMobile = mobile;
                travel.UseYn = Element.Get(this.useyn);
                using (Business.Travel biz = new Business.Travel(Common.Master.AppSetting.Connection))
                {
                    bool result = biz.Modify(travel);
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
                using (Business.Travel biz = new Business.Travel(Common.Master.AppSetting.Connection))
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
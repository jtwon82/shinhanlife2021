using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Auth;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;
using OrangeSummer.Web.MasterApplication.kr.co.youiwe.webservice;
using MLib.Cipher;
using MLib.Logger;

namespace SAP.Master.WebApplication
{
    public partial class _default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string id = Element.Get(this.id);
                string pwd = Element.Get(this.pwd);
                string pno = Element.Get(this.pno);
                string rndNo = Element.Get(this.rndNo);
                string referer = Check.IsNone(Request["referer"], "");

                string _rndNo = AES.Decrypt(OrangeSummer.Common.User.AppSetting.EncKey, MLib.Auth.Web.Cookies("ORANGESUMMER_RNDNO", "RNDNO"));
                
                if (_rndNo == rndNo || OrangeSummer.Common.Master.AppSetting.DevMode == "DEV")
                {
                    using (OrangeSummer.Business.Admin biz = new OrangeSummer.Business.Admin(OrangeSummer.Common.Master.AppSetting.Connection))
                    {
                        OrangeSummer.Model.Admin admin = biz.Login(id, pwd);
                        if (admin != null)
                        {
                            if (admin.Reset == "Y")
                            {
                                Form form = new Form("dataForm", "/reset/");
                                form.Add("id", id);
                                form.Add("pwd", pwd);
                                form.Make();
                                string html = form.ToString();
                                Tool.End(html);
                            }
                            else
                            {
                                string[] array = { admin.Id, admin.Usr, admin.Name };
                                Forms.Authorize(OrangeSummer.Common.Master.AppSetting.EncKey, admin.Id, array);
                                if (!Check.IsNone(referer))
                                {
                                    Tool.RR(referer);
                                }
                                else
                                {
                                    MLib.Auth.Web.Cookies("ORANGESUMMER_RNDNO", "RNDNO", "", 1);
                                    Tool.RR("/dash/");
                                }
                            }
                        }
                        else
                            JS.Back("로그인 정보를 확인해주세요. ");
                    }
                }
                else
                {
                    JS.Back("인증번호를 확인해주세요. ");
                }

            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Attach;
using MLib.DataBase;
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication
{
    public partial class template : System.Web.UI.Page
    {
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

                string access = "AKIAYWS5C4PBQE7U5IGB";
                string secret = "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";
                string bucket = "smartvisitingnurse";
                string path = @"C:\test\233-scaled.jpg";
                //S3 s3 = new S3(access, secret, bucket);
                //s3.IsPublic = true; // 객체를 퍼블릭권한으로 업로드(특별한 경우 아니면 사용 하지 말것)
                //s3.Upload(path);
                //if (s3.Result)
                //{
                //    // s3.Key는 2020/07/08/6458e6eb887c48bebd56fd8078ec0664.jpg 형태로 생성
                //    string key = s3.Key; // 키값(DB저장)
                //}

                // 클라이언트 다운로드
                //S3 s3 = new S3(access, secret, bucket);
                //s3.Download("키값", "파일명지정.jpg");


                //Template template = new Template(Common.Master.AppSetting.Connection, "전윤기", "UCC", "UCC이벤트", "ADM_UCC", "OrangeSummer", "ucc");
                //Template template = new Template(Common.Master.AppSetting.Connection, "전윤기", "AGREEMENT", "약관내용", "ADM_AGREEMENT", "OrangeSummer", "agreement");
                //template.All();
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
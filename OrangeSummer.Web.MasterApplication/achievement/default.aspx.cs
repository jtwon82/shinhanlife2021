using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDataReader;
using MLib.Attach;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication.achievement
{
    public partial class _default : System.Web.UI.Page
    {
        protected int _total = 0;
        protected string _paging = string.Empty;
        private int _size = 10;
        private int _block = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageLoad();
            }
        }

        /// <summary>
        /// 관리자 리스트
        /// </summary>
        private void PageLoad()
        {
            try
            {
                int page = Check.IsNone(Request["page"], 1);
                string branch = Check.IsNone(Request["branch"], "");
                string level = Check.IsNone(Request["level"], "");
                string code = Check.IsNone(Request["code"], "");
                string name = Check.IsNone(Request["name"], "");
                string orderby = Check.IsNone(Request["orderby"], "ORDERBY");

                // 지점
                using (Business.Branch biz = new Business.Branch(Common.Master.AppSetting.Connection))
                {
                    List<Model.Branch> list = biz.Line();
                    if (list != null)
                    {
                        this.branch.DataSource = list;
                        this.branch.DataTextField = "NAME";
                        this.branch.DataValueField = "NAME";
                        this.branch.DataBind();
                    }

                    this.branch.Items.Insert(0, new ListItem("선택", ""));
                }

                // 신분
                Dictionary<string, string> dic = Code.MemberLevel;
                this.level.DataSource = dic;
                this.level.DataTextField = "Value";
                this.level.DataValueField = "Key";
                this.level.DataBind();
                this.level.Items.Insert(0, new ListItem("선택", ""));

                Element.Set(this.branch, branch);
                Element.Set(this.level, level);
                Element.Set(this.code, code);
                Element.Set(this.name, name);

                using (Business.Achievement biz = new Business.Achievement(Common.Master.AppSetting.Connection))
                {
                    List<Model.Achievement> list = biz.List(page, _size, orderby, branch, level, code, name);
                    if (list != null)
                    {
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                        this.noData.Visible = false;
                        _total = list[0].Total;
                    }

                    Common.Master.Paging paging = new Common.Master.Paging("./", page, _size, _block, _total);
                    paging.AddParams("branch", branch);
                    paging.AddParams("level", level);
                    paging.AddParams("code", code);
                    paging.AddParams("name", name);
                    paging.AddParams("orderby", orderby);
                    _paging = paging.ToString();
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            try
            {
                if (this.attfile.HasFile)
                {
                    string ext = Path.GetExtension(this.attfile.PostedFile.FileName.ToLower());
                    if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                    {
                        path = Path.Combine(Common.Master.AppSetting.Path, "file", Tool.Unique + ext);
                        Upload upload = new Upload(this.attfile, path);
                        upload.Attached();

                        if (upload.Result)
                        {
                            DataSet ds = Reader(upload.SaveFilePath);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                using (Business.Achievement biz = new Business.Achievement(Common.Master.AppSetting.Connection))
                                {
                                    DataTable dt = biz.Regist(ds.Tables[0]);
                                    if (dt.Rows.Count == 1)
                                    {
                                        DataRow dr = dt.Rows[0];
                                        string result = dr["RESULT"].ToString();
                                        string message = dr["MESSAGE"].ToString();
                                        if (result == "SUCCESS")
                                            JS.Move("등록되었습니다.\\n데이터를 확인해주세요.", "./");
                                        else
                                            JS.Move(message.Replace(Environment.NewLine, "\\n"), "./");
                                    }
                                }
                            }
                            else
                                JS.Back("등록할 자료가 없습니다.");
                        }
                    }
                    else
                        JS.Back("EXCEL파일(xlsx, xls)만 업로드해주세요.");
                }
                else
                    JS.Back("잘못된 접근입니다.");
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
            finally
            {
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }
        }

        private DataSet Reader(string _path)
        {
            using (var stream = System.IO.File.Open(_path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    //return reader.AsDataSet();
                    return reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        UseColumnDataType = false,
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Url url = new Url("./");
            url.AddParams("branch", Element.Get(this.branch));
            url.AddParams("level", Element.Get(this.level));
            url.AddParams("code", Element.Get(this.code));
            url.AddParams("name", Element.Get(this.name));
            url.AddParams("orderby", "ORDERBY");
            url.Redirect();
        }

        protected string ListNumber(object obj, int index)
        {
            int page = Check.IsNone(Request["page"], 1);
            int number = (Convert.ToInt32(obj) - _size * (page - 1) - index);
            return number.ToString();
        }
    }
}
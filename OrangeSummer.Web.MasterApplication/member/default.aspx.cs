using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using MLib.Data;
using MLib.Util;
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication.member
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
                string mobile = Check.IsNone(Request["mobile"], "");
                string sdate = Check.IsNone(Request["sdate"], "");
                string edate = Check.IsNone(Request["edate"], "");

                // 지점
                using (Business.Branch biz = new Business.Branch(Common.Master.AppSetting.Connection))
                {
                    List<Model.Branch> list = biz.Line();
                    if (list != null)
                    {
                        this.branch.DataSource = list;
                        this.branch.DataTextField = "Name";
                        this.branch.DataValueField = "Id";
                        this.branch.DataBind();
                    }

                    this.branch.Items.Insert(0, new ListItem("전체", ""));
                }

                // 신분
                Dictionary<string, string> dic = Code.MemberLevel;
                this.level.DataSource = dic;
                this.level.DataTextField = "Value";
                this.level.DataValueField = "Key";
                this.level.DataBind();
                this.level.Items.Insert(0, new ListItem("전체", ""));

                Element.Set(this.branch, branch);
                Element.Set(this.level, level);
                Element.Set(this.code, code);
                Element.Set(this.mobile, mobile);
                Element.Set(this.sdate, sdate);
                Element.Set(this.edate, edate);

                using (Business.Member biz = new Business.Member(Common.Master.AppSetting.Connection))
                {
                    List<Model.Member> list = biz.List(page, _size, branch, level, code, mobile, sdate, edate);
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
                    paging.AddParams("mobile", mobile);
                    paging.AddParams("sdate", sdate);
                    paging.AddParams("edate", edate);
                    _paging = paging.ToString();
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }

        protected string ListNumber(object obj, int index)
        {
            int page = Check.IsNone(Request["page"], 1);
            int number = (Convert.ToInt32(obj) - _size * (page - 1) - index);
            return number.ToString();
        }

        protected string Parameters()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("&page=" + Check.IsNone(Request["page"], "1"));
            sb.Append("&branch=" + Check.IsNone(Request["branch"], ""));
            sb.Append("&level=" + Check.IsNone(Request["level"], ""));
            sb.Append("&code=" + Check.IsNone(Request["code"], ""));
            sb.Append("&mobile=" + Check.IsNone(Request["mobile"], ""));
            sb.Append("&sdate=" + Check.IsNone(Request["sdate"], ""));
            sb.Append("&edate=" + Check.IsNone(Request["edate"], ""));

            return sb.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Url url = new Url("./");
            url.AddParams("branch", Element.Get(this.branch));
            url.AddParams("level", Element.Get(this.level));
            url.AddParams("code", Element.Get(this.code));
            url.AddParams("mobile", Element.Get(this.mobile));
            url.AddParams("sdate", Element.Get(this.sdate));
            url.AddParams("edate", Element.Get(this.edate));
            url.Redirect();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string branch = Check.IsNone(Request["branch"], "");
                string level = Check.IsNone(Request["level"], "");
                string code = Check.IsNone(Request["code"], "");
                string mobile = Check.IsNone(Request["mobile"], "");
                string sdate = Check.IsNone(Request["sdate"], "");
                string edate = Check.IsNone(Request["edate"], "");

                using (Business.Member biz = new Business.Member(Common.Master.AppSetting.Connection))
                {
                    List<Model.Member> list = biz.Excel(branch, level, code, mobile, sdate, edate);
                    if (list != null)
                    {
                        string _filename = "회원리스트";
                        using (XLWorkbook book = new XLWorkbook())
                        {
                            string path = Path.Combine(Common.Master.AppSetting.Path, "temp", $"{_filename}_{DateTime.Now.ToString("yyyyMMdd")}.xlsx");

                            IXLWorksheet sheet = book.Worksheets.Add(_filename);
                            sheet.Style.Font.FontName = "Malgun Gothic";
                            sheet.Style.Font.FontSize = 10;

                            sheet.Row(1).Height = 20;
                            sheet.Cell("A1").Value = "#";
                            sheet.Cell("B1").Value = "지점";
                            sheet.Cell("C1").Value = "신분";
                            sheet.Cell("D1").Value = "코드";
                            sheet.Cell("E1").Value = "성명";
                            sheet.Cell("F1").Value = "휴대폰번호";
                            sheet.Cell("G1").Value = "활성여부";
                            sheet.Cell("H1").Value = "가입일";
                            sheet.Range("A1", "H1").Style.Font.Bold = true;
                            sheet.Range("A1", "H1").Style.Fill.BackgroundColor = XLColor.LightGray;
                            sheet.Range("A1", "H1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            sheet.Column(1).Width = 10;


                            int index = 2;
                            int count = list.Count;
                            foreach (Model.Member item in list)
                            {
                                sheet.Cell("A" + index).Value = count - (index - 2);
                                sheet.Cell("B" + index).Value = item.Branch.Name;
                                sheet.Cell("C" + index).Value = item.Level;
                                sheet.Cell("D" + index).Value = item.Code;
                                sheet.Cell("E" + index).Value = item.Name;
                                sheet.Cell("F" + index).Style.NumberFormat.Format = new string('0', item.Mobile.Length);
                                sheet.Cell("F" + index).Value = item.Mobile;
                                sheet.Cell("G" + index).Value = item.DelYn == "Y" ? "비활성" : "활성";
                                sheet.Cell("H" + index).Value = item.RegistDate;
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
                            sheet.Columns("C").Width = 10;

                            sheet.Columns("D").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Columns("D").AdjustToContents();
                            sheet.Columns("D").Width = 10;

                            sheet.Columns("E").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Columns("E").AdjustToContents();
                            sheet.Columns("E").Width = 15;

                            sheet.Columns("F").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Columns("F").AdjustToContents();
                            sheet.Columns("F").Width = 20;

                            sheet.Columns("G").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Columns("G").AdjustToContents();
                            sheet.Columns("G").Width = 15;

                            sheet.Columns("H").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Columns("H").AdjustToContents();
                            sheet.Columns("H").Width = 15;

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
                        JS.Back("다운로드할 회원이 없습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
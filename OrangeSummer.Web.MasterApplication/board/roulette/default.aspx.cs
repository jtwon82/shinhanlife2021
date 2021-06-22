using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using MLib.Util;
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication.board.roulette
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

        private void PageLoad()
        {
            try
            {
                int page = Check.IsNone(Request["page"], 1);
                using (Business.Roulette biz = new Business.Roulette(Common.Master.AppSetting.Connection))
                {
                    List<Model.Roulette> list = biz.List(page, _size);
                    if (list != null)
                    {
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                        this.noData.Visible = false;
                        _total = list[0].Total;
                    }

                    Common.Master.Paging paging = new Common.Master.Paging("./", page, _size, _block, _total);
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

            return sb.ToString();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                using (Business.Roulette biz = new Business.Roulette(Common.Master.AppSetting.Connection))
                {
                    List<Model.Roulette> list = biz.Excel();
                    if (list != null)
                    {
                        string _filename = "롤렛이벤트";
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
                            sheet.Cell("G1").Value = "참여일";
                            sheet.Cell("H1").Value = "당첨여부";
                            sheet.Range("A1", "H1").Style.Font.Bold = true;
                            sheet.Range("A1", "H1").Style.Fill.BackgroundColor = XLColor.LightGray;
                            sheet.Range("A1", "H1").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                            sheet.Column(1).Width = 10;


                            int index = 2;
                            int count = list.Count;
                            foreach (Model.Roulette item in list)
                            {
                                sheet.Cell("A" + index).Value = count - (index - 2);
                                sheet.Cell("B" + index).Value = item.Branch.Name;
                                sheet.Cell("C" + index).Value = item.Member.Level;
                                sheet.Cell("D" + index).Value = item.Member.Code;
                                sheet.Cell("E" + index).Value = item.Member.Name;
                                sheet.Cell("F" + index).Style.NumberFormat.Format = new string('0', item.Member.Mobile.Length);
                                sheet.Cell("F" + index).Value = item.Member.Mobile;
                                sheet.Cell("G" + index).Value = item.RegistDate;
                                sheet.Cell("H" + index).Value = item.Result;
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
                        JS.Back("다운로드할 댓글이 없습니다.");
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrangeSummer.Web.MasterApplication.dash
{
    public partial class _default : System.Web.UI.Page
    {
        protected string _mtotal = string.Empty;
        protected string _mnew = string.Empty;
        protected string _aperson = string.Empty;
        protected string _apersoncamp = string.Empty;
        protected string _asl = string.Empty;
        protected string _abranch = string.Empty;
        protected string _pbranch = string.Empty;
        protected string _pname = string.Empty;
        protected string _lbranch = string.Empty;
        protected string _lname = string.Empty;
        protected string _bbranch = string.Empty;
        protected string _bname = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PostLoad();
            }
        }

        private void PostLoad()
        {
            try
            {
                #region [ 업적현황 ]
                using (Business.Dash biz = new Business.Dash(Common.Master.AppSetting.Connection))
                {
                    DataSet ds = biz.Summary();

                    // 회원 현황
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        _mtotal = Convert.ToDecimal(dr["TOTAL"].ToString()).ToString("#,##0");
                        _mnew = Convert.ToDecimal(dr["NEW"].ToString()).ToString("#,##0");
                    }
                    else
                    {
                        _mtotal = "0";
                        _mnew = "0";
                    }

                    // 업적 현황
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[1].Rows[0];
                        _aperson = Convert.ToDecimal(dr["PERSON"].ToString()).ToString("#,##0");
                        _apersoncamp = Convert.ToDecimal(dr["PERSONCAMP"].ToString()).ToString("#,##0");
                        _asl = Convert.ToDecimal(dr["SL"].ToString()).ToString("#,##0");
                        _abranch = Convert.ToDecimal(dr["BRANCH"].ToString()).ToString("#,##0");
                    }
                    else
                    {
                        _aperson = "0";
                        _asl = "0";
                        _abranch = "0";
                    }

                    // 개인 1위
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[2].Rows[0];
                        _pbranch = dr["BRANCH_NAME"].ToString().Replace("지점", "");
                        _pname = dr["NAME"].ToString();
                    }
                    else
                    {
                        _pbranch = "0";
                        _pname = "0";
                    }

                    // SL 1위
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[3].Rows[0];
                        _lbranch = dr["BRANCH_NAME"].ToString().Replace("지점", "");
                        _lname = dr["NAME"].ToString();
                    }
                    else
                    {
                        _lbranch = "0";
                        _lname = "0";
                    }

                    // 지점 1위
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[4].Rows[0];
                        _bbranch = dr["BRANCH_NAME"].ToString().Replace("지점", "");
                        _bname = dr["NAME"].ToString();
                    }
                    else
                    {
                        _bbranch = "0";
                        _bname = "0";
                    }
                }
                #endregion

                #region [ 공지사항 ]
                using (Business.Notice biz = new Business.Notice(Common.Master.AppSetting.Connection))
                {
                    List<Model.Notice> list = biz.Main();
                    if (list != null)
                    {
                        this.rptNoticeList.DataSource = list;
                        this.rptNoticeList.DataBind();
                    }
                }
                #endregion

                #region [ 이벤트 ]
                using (Business.Event biz = new Business.Event(Common.Master.AppSetting.Connection))
                {
                    List<Model.Event> list = biz.Main();
                    if (list != null)
                    {
                        this.rptEventList.DataSource = list;
                        this.rptEventList.DataBind();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
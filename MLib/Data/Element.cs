using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;
using MLib.Util;
using System.Data;
using System;
using System.Web;
using System.Globalization;

namespace MLib.Data
{
    public static class Element
    {
        #region [ 컨트롤 입력값 ]
        /// <summary>
        /// 문자열
        /// </summary>
        /// <param name="str">string 문자열</param>
        /// <returns>string 문자열</returns>
        public static string Get(string str)
        {
            str=CheckInput(str);
            return str;
        }

        /// <summary>
        /// 숫자
        /// </summary>
        /// <param name="num">string 숫자</param>
        /// <returns>string 숫자</returns>
        public static string Get(int num)
        {
            //CheckInput(num.ToString());
            return CheckInput(num.ToString());
        }

        /// <summary>
        /// TextBox 입력 값
        /// </summary>
        /// <param name="control">TextBox 컨트롤</param>
        /// <returns>string 문자열</returns>
        public static string Get(TextBox control)
        {
            string rtn = string.Empty;

            if (!Check.IsNone(control.Text))
            {
                rtn = control.Text;
                rtn=CheckInput(rtn);
            }

            return rtn;
        }

        /// <summary>
        /// HiddenField 입력 값
        /// </summary>
        /// <param name="control">HiddenField 컨트롤</param>
        /// <returns>string 문자열</returns>
        public static string Get(HiddenField control)
        {
            string rtn = string.Empty;

            if (!Check.IsNone(control.Value))
            {
                rtn = control.Value;
                rtn=CheckInput(rtn);
            }

            return rtn;
        }

        /// <summary>
        /// DropDownList 선택 값
        /// </summary>
        /// <param name="control">DropDownList 컨트롤</param>
        /// <returns>string 문자열</returns>
        public static string Get(DropDownList control)
        {
            string rtn = string.Empty;
            if (!Check.IsNone(control.SelectedValue))
            {
                rtn = control.SelectedValue;
                rtn=CheckInput(rtn);
            }

            return rtn;
        }

        /// <summary>
        /// RadioButtonList 선택 값
        /// </summary>
        /// <param name="control">RadioButtonList 컨트롤</param>
        /// <returns>string 문자열</returns>
        public static string Get(RadioButtonList control)
        {
            string rtn = string.Empty;
            if (!Check.IsNone(control.SelectedValue))
            {
                rtn = control.SelectedValue;
                rtn = CheckInput(rtn);
            }

            return rtn;
        }

        /// <summary>
        /// HtmlInputRadioButton 선택 값
        /// </summary>
        /// <param name="control">HtmlInputRadioButton 컨트롤</param>
        /// <returns>string 문자열</returns>
        public static string Get(HtmlInputRadioButton control)
        {
            if (control.Checked){
                return CheckInput(control.Value);
            }
            else{
                return "";
            }
        }

        /// <summary>
        /// CheckBoxList 선택 값
        /// </summary>
        /// <param name="control">CheckBoxList 컨트롤</param>
        /// <returns>string 문자열</returns>
        public static string Get(CheckBoxList control)
        {
            string rtn = string.Empty;
            ArrayList al = new ArrayList();
            foreach (ListItem item in control.Items)
            {
                if (item.Selected)
                {
                    al.Add(CheckInput(item.Value));
                }
            }

            return string.Join(",", al.ToArray());
        }

        /// <summary>
        /// RadioButton 선택
        /// </summary>
        /// <param name="control">RadioButton 컨트롤</param>
        /// <param name="value"></param>
        /// <returns>string 문자열</returns>
        public static string Get(RadioButton control)
        {
            string rtn = string.Empty;
            if (control.Checked == true)
            {
                rtn = control.Attributes["Value"].ToString();
                rtn=CheckInput(rtn);
            }
            return rtn;
        }
        #endregion

        #region [ 컨트롤 셋팅 ]
        /// <summary>
        /// Label 셋팅
        /// </summary>
        /// <param name="control">Label 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(Label control, string value)
        {
            control.Text = HttpUtility.HtmlDecode(value);
        }

        /// <summary>
        /// TextBox 셋팅
        /// </summary>
        /// <param name="control">TextBox 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(TextBox control, string value)
        {
            control.Text = HttpUtility.HtmlDecode(value);
        }

        /// <summary>
        /// TextBox 셋팅
        /// </summary>
        /// <param name="control">CheckBox 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(CheckBox control, bool value)
        {
            control.Checked = value;
        }

        /// <summary>
        /// HiddenField 셋팅
        /// </summary>
        /// <param name="control">HiddenField 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(HiddenField control, string value)
        {
            control.Value = HttpUtility.HtmlDecode(value);
        }

        /// <summary>
        /// DropDownList 셋팅
        /// </summary>
        /// <param name="control">DropDownList 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(DropDownList control, string value)
        {
            control.SelectedValue = HttpUtility.HtmlDecode(value);
        }

        /// <summary>
        /// RadioButtonList 선택 값
        /// </summary>
        /// <param name="control">RadioButtonList 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(RadioButtonList control, string value)
        {
            control.SelectedValue = HttpUtility.HtmlDecode(value);
        }

        /// <summary>
        /// CheckBoxList 선택 값
        /// </summary>
        /// <param name="control">CheckBoxList 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(CheckBoxList control, string value)
        {
            string[] array = value.Split(',');
            foreach (ListItem item in control.Items)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (item.Value == array[i].ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// HtmlTableCell 선택 값
        /// </summary>
        /// <param name="control">HtmlTableCell 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(HtmlTableCell control, string value)
        {
            control.InnerHtml = HttpUtility.HtmlDecode(value);
        }

        /// <summary>
        /// HtmlInputText 선택 값
        /// </summary>
        /// <param name="control">HtmlInputText 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(HtmlInputText control, string value)
        {
            control.Value = HttpUtility.HtmlDecode(value);
        }

        /// <summary>
        /// HtmlGenericControl 선택 값
        /// </summary>
        /// <param name="control">HtmlGenericControl 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(HtmlGenericControl control, string value)
        {
            control.InnerHtml = HttpUtility.HtmlDecode(value);
        }

        /// <summary>
        /// HtmlButton 선택 값
        /// </summary>
        /// <param name="control">HtmlButton 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(HtmlButton control, string value)
        {
            control.InnerHtml = HttpUtility.HtmlDecode(value);
        }

        /// <summary>
        /// RadioButton 선택 값
        /// </summary>
        /// <param name="control">RadioButton 컨트롤</param>
        /// <param name="value">string 셋팅 값</param>
        public static void Set(RadioButton control, string value)
        {

            if (control.Attributes["Value"].ToString().Equals(value))
            {
                control.Checked = true;
            }
        }
        #endregion

        #region [ 컨트롤 바인딩 ]
        /// <summary>
        /// DropDwonList 데이터 바인딩
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="control">DropDownList 컨트롤</param>
        /// <param name="field">DropDownList Value 항목</param>
        /// <param name="text">DropDownList Text 항목</param>
        /// <param name="value">선택될 값</param>
        /// <param name="option">디폴트 옵선항목</param>
        public static void Set(DataTable dt, DropDownList control, string field, string text, string value, string option)
        {
            if (dt.Rows.Count > 0)
            {
                control.DataSource = dt;
                control.DataValueField = field;
                control.DataTextField = text;
                control.DataBind();
                control.SelectedValue = value;
            }
            control.Items.Insert(0, new ListItem(option, ""));
        }
        #endregion

        #region 인젝션공격 방어로직

        public static string[] blackList = {"--",";--",";","/*","*/","@@","@",
                                         "char","nchar","varchar","nvarchar",
                                         "alter","begin","cast","create","cursor","declare","delete","drop","end","exec","execute",
                                         "fetch","insert","kill","open",
                                         "select", "sys","sysobjects","syscolumns",
                                         "table","update"};
        
        //The utility method that performs the blacklist comparisons
        //You can change the error handling, and error redirect location to whatever makes sense for your site.
        public static string CheckInput(string parameter)
        {
            CompareInfo comparer = CultureInfo.InvariantCulture.CompareInfo;

            for (int i = 0; i < blackList.Length; i++)
            {
                if (comparer.IndexOf(parameter, blackList[i], CompareOptions.IgnoreCase) >= 0)
                {
                    //
                    //Handle the discovery of suspicious Sql characters here
                    //
                    //HttpContext.Current.Response.Redirect("/error.aspx");  //generic error page on your site
                    //HttpContext.Current.Response.Write(HttpUtility.HtmlEncode(parameter));
                    //HttpContext.Current.Response.End();
                    parameter= HttpUtility.HtmlEncode(parameter);
                    break;
                }
            }
            return parameter;
        }

        #endregion
    }
}

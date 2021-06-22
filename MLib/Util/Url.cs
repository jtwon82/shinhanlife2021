using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;

namespace MLib.Util
{
    public class Url
    {
        private string _url;
        private Dictionary<string, string> _dic = new Dictionary<string, string>();

        /// <summary>
        /// 생성자
        /// </summary>
        public Url(string url)
        {
            _url = url;
        }

        /// <summary>
        /// URL파라메터 추가
        /// </summary>
        public void AddParams(string key, string value)
        {
            _dic.Add(key, value);
        }

        /// <summary>
        /// 숫자
        /// </summary>
        public void AddParams(string key, int value)
        {
            _dic.Add(key, value.ToString());
        }

        /// <summary>
        /// TextBox 입력 값
        /// </summary>
        public void AddParams(string key, TextBox control)
        {
            _dic.Add(key, control.Text);
        }

        /// <summary>
        /// HiddenField 입력 값
        /// </summary>
        public void AddParams(string key, HiddenField control)
        {
            _dic.Add(key, control.Value);
        }

        /// <summary>
        /// DropDownList 선택 값
        /// </summary>
        public void AddParams(string key, DropDownList control)
        {
            _dic.Add(key, control.SelectedValue);
        }

        /// <summary>
        /// RadioButtonList 선택 값
        /// </summary>
        public void AddParams(string key, RadioButtonList control)
        {
            _dic.Add(key, control.SelectedValue);
        }

        /// <summary>
        /// CheckBoxList 선택 값
        /// </summary>
        public void AddParams(string key, CheckBoxList control)
        {
            ArrayList al = new ArrayList();
            foreach (ListItem item in control.Items)
            {
                if (item.Selected)
                    al.Add(item.Value);
            }

            _dic.Add(key, string.Join(",", al.ToArray()));
        }

        /// <summary>
        /// RadioButton 선택
        /// </summary>
        public void AddParams(string key, RadioButton control)
        {
            if (control.Checked == true)
                _dic.Add(key, control.Attributes["Value"].ToString());
            else
                _dic.Add(key, "");
        }

        /// <summary>
        /// URL 완성된 문자열
        /// </summary>
        public override string ToString()
        {
            bool prefix = false;
            string url = _url;

            if (_dic.Count > 0)
            {
                if (Check.IsIn(url, "?"))
                    prefix = true;
            }

            int index = 0;
            foreach (KeyValuePair<string, string> item in _dic)
            {
                if (prefix)
                    url += "&";
                else
                {
                    if (index.Equals(0))
                        url += "?";
                    else
                        url += "&";
                }

                url += item.Key + "=" + HttpUtility.UrlEncode(item.Value);
                index++;
            }

            return url;
        }

        /// <summary>
        /// 생성된 URL로 Redirect
        /// </summary>
        public void Redirect()
        {
            Tool.RR(this.ToString());
        }
    }
}

using MLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLib.Data
{
    public class Form : IDisposable
    {
        private string _name = string.Empty;
        private string _action = string.Empty;
        private Dictionary<string, string> _parameter = null;
        private string _script = string.Empty;
        private string _html = string.Empty;
        public Form(string name, string action)
        {
            _parameter = new Dictionary<string, string>();
            _name = name;
            _action = action;
        }

        public Form(string name, string action, string script)
            :this(name, action)
        {
            _script = script;
        }

        public void Make()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"utf-8\" />");
            sb.AppendLine("    <title></title>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("    <form name=\"" + _name + "\" id=\"" + _name + "\" method=\"post\" action=\"" + _action + "\">");

            if (_parameter.Count > 0)
            {
                foreach (var item in _parameter)
                    sb.AppendLine("    <input type=\"hidden\" name=\"" + item.Key + "\" value=\"" + item.Value + "\" />");
            }
            sb.AppendLine("    </form>");
            if(!Check.IsNone(_script))
                sb.AppendLine(_script);
            else
                sb.AppendLine("<script>document." + _name + ".submit();</script>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            _html = sb.ToString();
        }

        public void Add(string key, string value)
        {
            _parameter.Add(key, value);
        }

        public override string ToString()
        {
            return _html;
        }

        public void Dispose()
        {
            _parameter = null;
        }
    }
}

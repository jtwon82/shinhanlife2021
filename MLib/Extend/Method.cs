using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MLib.Extend
{
    public static class Method
    {
        public static Uri Add(this Uri uri, string name, string value)
        {
            UriBuilder ub = new UriBuilder(uri);
            NameValueCollection http = HttpUtility.ParseQueryString(uri.Query);

            http.Add(name, value);

            ub.Query = http.ToString();

            return ub.Uri;
        }
    }
}

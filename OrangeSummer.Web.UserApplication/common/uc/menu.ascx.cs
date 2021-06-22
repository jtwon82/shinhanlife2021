using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Auth;
using MLib.Util;

namespace OrangeSummer.Web.UserApplication.common.uc
{
    public partial class menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                MLib.Auth.Web.Cookies("ORANGESUMMER", "SECRET", "", -1);
                Forms.UnAuthorize();
                Tool.RR("/");
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
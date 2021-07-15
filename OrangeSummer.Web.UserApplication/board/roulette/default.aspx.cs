using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MLib.Util;

namespace OrangeSummer.Web.UserApplication.board.roulette
{
    public partial class _default : System.Web.UI.Page
    {
        protected string _result = string.Empty;
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
                using (Business.Roulette biz = new Business.Roulette(Common.User.AppSetting.Connection))
                {
                    Random random = new Random();
                    int ran = 0;
                    bool check = true;
                    if (biz.UserSuccess())
                    {
                        check = biz.UserCheck(Common.User.Identify.Id);
                        if (check)
                            _result = "EXISTS";
                        else
                        {
                            ran = random.Next(1, 50);

                            if (ran <= 10)
                                _result = "SUCCESS";
                            else
                                _result = "FAIL";
                        }
                    }
                    else
                        _result = "FAIL";
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.WebHandler(ex);
            }
        }
    }
}
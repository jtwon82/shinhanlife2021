using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLib.Auth;

namespace OrangeSummer.Common.Master
{
    public class Identify
    {
        /// <summary>
        /// 관리자 ID
        /// </summary>
        public static string Id
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 0);
            }
        }

        /// <summary>
        /// 관리자 아이디
        /// </summary>
        public static string Usr
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 1);
            }
        }

        /// <summary>
        /// 관리자 이름
        /// </summary>
        public static string Name
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 2);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLib.Auth;

namespace OrangeSummer.Common.User
{
    public class Identify
    {
        /// <summary>
        /// 사용자 ID(PK)
        /// </summary>
        public static string Id
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 0);
            }
        }

        /// <summary>
        /// 사용자 코드
        /// </summary>
        public static string Code
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 1);
            }
        }

        /// <summary>
        /// 사용자 이름
        /// </summary>
        public static string Name
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 2);
            }
        }

        /// <summary>
        /// 사용자 신분
        /// </summary>
        public static string Level
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 3);
            }
        }

        /// <summary>
        /// 사용자 소속 지점(PK)
        /// </summary>
        public static string BranchId
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 4);
            }
        }

        /// <summary>
        /// 사용자 소속 지점명
        /// </summary>
        public static string BranchName
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 5);
            }
        }

        public static string ProfileImg
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 6);
            }
        }

        public static string BackgroundImg
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 7);
            }
        }
        public static string LevelName
        {
            get
            {
                return Forms.AuthInfo(AppSetting.EncKey, 8);
            }
        }
    }
}

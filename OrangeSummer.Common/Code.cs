using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLib.Config;
using System.Web;
using System.Globalization;

namespace OrangeSummer.Common
{
    public class Code
    {
        /// <summary>
        /// 회원 신분
        /// </summary>
        public static Dictionary<string, string> MemberLevel
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("FC", "FC");
                dic.Add("NEWFC", "신인FC");
                dic.Add("SL", "SL");
                dic.Add("BM", "BM");
                dic.Add("EM", "EM");
                dic.Add("ERM", "ERM");

                return dic;
            }
        }

        /// <summary>
        /// 회원 신분 이름
        /// </summary>
        public static string MemberLevelName(string level)
        {
            Dictionary<string, string> dic = Code.MemberLevel;
            if (dic.ContainsKey(level))
                return dic[level];
            else
                return "";
        }

        /// <summary>
        /// UCC
        /// </summary>
        public static Dictionary<string, string> UccEvent
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("1", "SUPER사업단");
                dic.Add("2", "금강지점");
                dic.Add("3", "대치사업본부");
                dic.Add("4", "반포지점");
                dic.Add("5", "선두지점");
                dic.Add("6", "신화창조지점");

                return dic;
            }
        }

        /// <summary>
        /// UCC 지점명
        /// </summary>
        public static string UccEventName(string branch)
        {
            Dictionary<string, string> dic = Code.UccEvent;
            if (dic.ContainsKey(branch))
                return dic[branch];
            else
                return "";
        }

        /// <summary>
        /// UCC
        /// </summary>
        public static Dictionary<string, string> WordEvent
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("1", "라스베가스");
                dic.Add("2", "하와이");
                dic.Add("3", "바르셀로나");

                return dic;
            }
        }

        /// <summary>
        /// UCC 지점명
        /// </summary>
        public static string WordEventName(string branch)
        {
            Dictionary<string, string> dic = Code.WordEvent;
            if (dic.ContainsKey(branch))
                return dic[branch];
            else
                return "";
        }
        
    }
}

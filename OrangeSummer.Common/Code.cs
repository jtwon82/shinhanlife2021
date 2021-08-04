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
                dic.Add("신인FC", "신인FC");
                dic.Add("E SL", "E SL");
                dic.Add("S SL", "S SL");
                dic.Add("G SL", "G SL");
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
                return level;
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
                int id = 1;
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add((id++) + "", "OCAN지점/36037/이종호");
                dic.Add((id++) + "", "강남지점/17721/최재갑");
                dic.Add((id++) + "", "한신지점/62336/이상윤");
                dic.Add((id++) + "", "도곡지점/26776/정영섭");
                dic.Add((id++) + "", "부산1지점/22003/김태호");
                dic.Add((id++) + "", "TOP지점/55497/김종민");
                dic.Add((id++) + "", "리더스지점/61576/변영성");
                dic.Add((id++) + "", "GUINNESS지점/43031/이옥준");
                dic.Add((id++) + "", "리더스지점/65603/박초희");
                dic.Add((id++) + "", "잠실지점/28161/조은경");
                dic.Add((id++) + "", "광주1지점/26671/김상렬");
                dic.Add((id++) + "", "한신지점/58969/김병선");
                dic.Add((id++) + "", "신화창조지점/66418/이영수");
                dic.Add((id++) + "", "압구정지점/64309/강성국");
                dic.Add((id++) + "", "선두지점/62449/조병화");
                dic.Add((id++) + "", "ㅁㅁㅁ");

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

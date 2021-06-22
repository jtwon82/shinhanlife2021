using MLib.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace MLib.Util
{
    public static class Tool
    {
        #region [ Print ]
        /// <summary>
        /// 문자열 print
        /// </summary>
        /// <param name="value">문자열</param>
        public static void Print(string value)
        {
            HttpContext.Current.Response.Write(value);
        }

        /// <summary>
        /// 숫자 print
        /// </summary>
        /// <param name="num">int : 정수</param>
        public static void Print(int value)
        {
            Print(value);
        }

        /// <summary>
        /// Hashtable print
        /// </summary>
        /// <param name="ht">Hashtable : Hashtable 콜렉션</param>
        public static void Print(Hashtable ht)
        {

            foreach (DictionaryEntry de in ht)
            {
                Print(string.Format("Key = {0}, Value = {1}<br/>", de.Key, de.Value));
            }

        }

        /// <summary>
        /// SortedList print
        /// </summary>
        /// <param name="sort">SortedList : SortedList 콜렉션</param>
        public static void Print(SortedList sort)
        {
            foreach (DictionaryEntry de in sort)
            {
                Print(string.Format("Key = {0}, Value = {1}<br/>", de.Key, de.Value));
            }
        }

        /// <summary>
        /// List<SqlParameter> print
        /// </summary>
        /// <param name="parameters">List<SqlParameter> : List<SqlParameter> 콜렉션</param>
        public static void Print(List<System.Data.SqlClient.SqlParameter> parameters)
        {
            foreach (var item in parameters)
            {
                PrintLine("{0} = {1}", item.ParameterName, item.Value);
            }
        }

        /// <summary>
        /// 문자열 배열 print
        /// </summary>
        /// <param name="array">string[] : 배열</param>
        public static void Print(string[] array)
        {
            for (int i = 0, length = array.Length; i < length; i++)
            {
                Print(string.Format("array[{0}] = {1}<br/>", i.ToString(), array[i].ToString()));
            }

        }

        /// <summary>
        /// int 배열 print
        /// </summary>
        /// <param name="array">int[] : 배열</param>
        public static void Print(int[] array)
        {

            for (int i = 0, length = array.Length; i < length; i++)
            {
                Print(string.Format("array[{0}] = {1}<br/>", i.ToString(), array[i].ToString()));
            }
        }

        /// <summary>
        /// ArrayList print
        /// </summary>
        /// <param name="array">ArrayList : ArrayList 콜렉션</param>
        public static void Print(ArrayList al)
        {
            for (int i = 0, length = al.Count; i < length; i++)
            {
                Print(string.Format("ArrayList[{0}] = {1}<br/>", i.ToString(), al[i].ToString()));
            }
        }

        /// <summary>
        /// DataTable Print
        /// </summary>
        /// <param name="dt">DataTable : DataTable</param>
        public static string Print(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn columns in dt.Columns)
                {
                    sb.Append($"row["+ columns + "] = '"+ row[columns].ToString() + "' ");
                }
                sb.Append("|");
            }
            return sb.ToString();
        }
        
        /// <summary>
        /// string.Format Print
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        public static void Print(string format, params object[] arg)
        {
            Print(string.Format(format, arg));
        }

        /// <summary>
        /// 문자열 print
        /// </summary>
        /// <param name="value">문자열</param>
        public static void PrintLine(string value)
        {
            Print(value + "<br />");
        }

        /// <summary>
        /// string.Format Print
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        public static void PrintLine(string format, params object[] arg)
        {
            PrintLine(string.Format(format, arg));
        }

        /// <summary>
        /// Model Print
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void Print<T>(T obj)
        {
            foreach (PropertyDescriptor descript in TypeDescriptor.GetProperties(obj))
            {
                string name = descript.Name;
                object value = descript.GetValue(obj);
                Tool.PrintLine("{0} = {1}", name, value);
            }
        }
        #endregion

        #region [ End ]
        /// <summary>
        /// 프로세스 중지
        /// </summary>
        public static void End()
        {
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 프로세스 중지
        /// </summary>
        /// <param name="msg">string </param>
        public static void End(string msg)
        {
            Tool.Print(msg);
            Tool.End();
        }
        #endregion

        #region [ ETC ]
        //public static string Rep

        /// <summary>
        /// 구분자 구분된 문자열 '(홀따움표)
        /// </summary>
        /// <param name="value">문자열</param>
        /// <param name="separator">구분자</param>
        /// <returns>'1', '2', '3'</returns>
        public static string AddMark(string value, string separator)
        {
            if (!Check.IsNone(value))
            {
                string[] array = value.Split(new string[] { separator }, StringSplitOptions.None);
                return "'" + string.Join("', '", array) + "'";
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Split함수 인덱스 범위 예외처리
        /// </summary>
        /// <param name="value">원본문자열</param>
        /// <param name="section">구분자</param>
        /// <param name="index">분리한 문자열의 인덱스번호</param>
        /// <returns>string index번째 분리된 문자열</returns>
        public static string Separator(string value, string section, int index)
        {
            string rtn = string.Empty;
            string[] array = null;

            if (Check.IsIn(value, section))
            {
                array = value.Split(new string[] { section }, StringSplitOptions.None);
                if ((array.Length - 1) < index)
                    return "";
                else
                    return array[index];
            }
            else
                return "";
        }

        /// <summary>
        /// 문자열 자르기
        /// </summary>
        /// <param name="str">원본 문자열</param>
        /// <param name="length">int : 자를 자리수</param>
        /// <param name="endstr">자르고 붙일 문자열</param>
        /// <returns>string 문자열</returns>
        public static string SubString(string str, int length, string endstr)
        {
            if (str.Length > length)
            {
                return str.Substring(0, length) + endstr;
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 문자열 중 태그 제거
        /// </summary>
        /// <param name="html">html 태그 문자열</param>
        /// <returns>string 문자열</returns>
        public static string RemoveHTML(string html)
        {
            string str = string.Empty;
            string pattern = @"\<[^\<\>]*\>";
            str = System.Text.RegularExpressions.Regex.Replace(html, pattern, string.Empty);
            str = str.Replace("<", string.Empty).Replace(">", string.Empty);
            return str;
        }

        /// <summary>
        /// 문자열 치환
        /// </summary>
        /// <param name="value">원본문자열</param>
        /// <returns>string 치환된 문자열 반환</returns>
        public static string JSReplace(string value)
        {
            return value.Replace(@"\", @"\\").Replace("\r", @"\r").Replace("\t", @"\t").Replace("\n", @"\n").Replace("'", @"\'");
        }

        /// <summary>
        /// 문자열 자리 수 만들기
        /// </summary>
        /// <param name="value">원본문자열</param>
        /// <param name="fill">문자열</param>
        /// <param name="size">int : 자리 수</param>
        /// <returns>string 지정된 자리 수의 문자열</returns>
        public static string Fill(string value, string fill, int size)
        {
            string rtn = string.Empty;
            if (value.Length >= size)
            {
                rtn = value;
            }
            else
            {
                string temp = string.Empty;
                for (int i = 0; i < (size - value.Length); i++)
                {
                    temp += fill;
                }
                rtn = temp + value;
            }
            return rtn;
        }

        /// <summary>
        /// 문자열 중간에 문자열 끼워넣기
        /// </summary>
        /// <param name="text">원본문자열</param>
        /// <param name="index">int : 끼워넣을 위치(1부터 시작)</param>
        /// <param name="value">끼워넣을 문자열</param>
        /// <returns>string 지정된 문자열 삽입된 문자열</returns>
        public static string Insert(string text, int index, string value)
        {
            if (text.Length > index)
            {
                string start = text.Substring(0, index);
                return start + value + text.Substring(index, text.Length - start.Length);
            }
            else
            {
                return text;
            }
        }

        /// <summary>
        /// 문자열 중간에 문자열 끼워넣기(반복)
        /// </summary>
        /// <param name="text">원본문자열</param>
        /// <param name="index">int : 끼워넣을 위치(1부터 시작)</param>
        /// <param name="value">끼워넣을 문자열</param>
        /// <returns>string 지정된 문자열 삽입된 문자열</returns>
        public static string InsertLoop(string text, int len, string value)
        {
            if (!Check.IsNone(text))
            {
                if (text.Length > len)
                {
                    int total = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(text.Length) / len));
                    int length = len;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    for (int i = 0; i < total; i++)
                    {
                        string temp = text.Substring(i * len, length);
                        string etc = text.Substring((i + 1) * length);

                        if (etc.Length <= len)
                            length = etc.Length;

                        sb.Append(temp + value);
                    }

                    return sb.ToString();
                }
                else
                    return text;
            }
            else
                return text;
        }

        /// <summary>
        /// 파라메터 연결 문자열 중에서 원하는 파라메터 추출
        /// </summary>
        /// <param name="str">Querystring 문자열</param>
        /// <param name="name">파라매터 명</param>
        /// <returns>string 파라매터 명의 value</returns>
        public static string FindValue(string str, string name)
        {
            string rtn = string.Empty;
            if (!Check.IsNone(str))
            {
                string[] param = str.Split('&');
                foreach (string p in param)
                {
                    string[] value = p.Split('=');
                    if (value[0].Equals(name))
                    {
                        rtn = value[1];
                        break;
                    }
                }
            }
            else
                rtn = "";

            return rtn;
        }

        /// <summary>
        /// 확장메서드 엔터문자를
        /// </summary>
        /// <param name="text">원본 문자열</param>
        /// <returns>string 엔터가 <br />로 치환된 문자열</returns>
        public static string EnterToBr(string text)
        {
            return text.Replace("\n", "<br />");
        }

        /// <summary>
        /// Response.Redirect 이동
        /// </summary>
        /// <param name="url">이동할 URL</param>
        public static void RR(string url)
        {
            HttpContext.Current.Response.Redirect(url);
        }

        /// <summary>
        /// Server.Transfer 이동
        /// </summary>
        /// <param name="url">이동할 URL</param>
        public static void ST(string url)
        {
            HttpContext.Current.Server.Transfer(url);
        }

        /// <summary>
        /// GUID를 이용해 유니크한 문자열(32자 영문, 숫자)
        /// </summary>
        /// <returns>유니크한 32자 문자열</returns>
        public static string Unique
        {
            get
            {
                return Guid.NewGuid().ToString("N");
            }
        }

        /// <summary>
        /// GUID를 이용해 유니크한 문자열(36자 문자열 '-' 포함 대문자)
        /// </summary>
        /// <returns>유니크한 36자 문자열 '-' 포함 대문자</returns>
        public static string UniqueNewGuid
        {
            get
            {
                return Guid.NewGuid().ToString("D").ToUpper();
            }
        }

        /// <summary>
        /// GUID를 이용해 유니크한 문자열을 NEWGUID형태로 변환
        /// </summary>
        /// <param name="guid">32자 영문, 숫자</param>
        /// <returns>NewGuid형태</returns>
        public static string UniqueToNewGuid(string guid)
        {
            if (guid.Length == 32)
            {
                guid = guid.ToUpper();
                return guid.Substring(0, 8) + "-" + guid.Substring(8, 4) + "-" + guid.Substring(12, 4) + "-" + guid.Substring(16, 4) + "-" + guid.Substring(20, 12);
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// 영문(대문자, 소문자) + 숫자의 랜덤 생성
        /// </summary>
        /// <param name="len">생성할 문자열 길이</param>
        /// <returns>정해진 자리수 대로 랜덤문자 생성</returns>
        public static string RandomString(int len)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] charArray = chars.ToCharArray();
            string rtn = string.Empty;
            int seed = Environment.TickCount;
            Random rd = new Random(seed);
            int ran = 0;

            for (int i = 0; i < len; i++)
            {
                ran = rd.Next(0, charArray.Length - 1);
                rtn += charArray[ran];
            }

            return rtn;
        }

        /// <summary>
        /// 영문(대문자, 소문자) + 숫자의 랜덤 생성
        /// </summary>
        /// <param name="len">생성할 문자열 길이</param>
        /// <returns>정해진 자리수 대로 랜덤문자 생성</returns>
        public static string RandomInt(int len)
        {
            string chars = "0123456789";
            char[] charArray = chars.ToCharArray();
            string rtn = string.Empty;
            int seed = Environment.TickCount;
            Random rd = new Random(seed);
            int ran = 0;

            for (int i = 0; i < len; i++)
            {
                ran = rd.Next(0, charArray.Length - 1);
                rtn += charArray[ran];
            }

            return rtn;
        }

        /// <summary>
        /// 날짜 포맷 20160601 to 2016-06-01
        /// </summary>
        /// <param name="date">생년월일 ex) 19801216</param>
        /// <returns>생년월일 포맷으로 변환 : ex) 1980-12-16</returns>
        public static string FormatDate(string date)
        {
            string rtn = string.Empty;
            if (!Check.IsNone(date))
            {
                if (date.Length == 8)
                    rtn = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
                else
                    rtn = date;
            }
            return rtn;
        }

        /// <summary>
        /// 연락처 '-'추가 01012341234 to 010-1234-1234
        /// </summary>
        /// <param name="phone">전화번호 ex) 01012341234</param>
        /// <returns>전화번호 포맷으로 변환 : ex) 010-1234-1234</returns>
        public static string FormatPhone(string phone)
        {
            string rtn = string.Empty;
            if (phone.Length >= 9)
            {
                string start = phone.Substring(0, 2);
                if (start == "02")
                {
                    if (phone.Length == 10)
                    {
                        rtn = phone.Substring(0, 2) + "-" + phone.Substring(2, 4) + "-" + phone.Substring(6, 4);
                    }
                    else
                    {
                        rtn = phone.Substring(0, 2) + "-" + phone.Substring(2, 3) + "-" + phone.Substring(5, 4);
                    }
                }
                else
                {
                    if (phone.Length == 10)
                    {
                        rtn = phone.Substring(0, 3) + "-" + phone.Substring(3, 4) + "-" + phone.Substring(6, 4);
                    }
                    else
                    {
                        rtn = phone.Substring(0, 3) + "-" + phone.Substring(3, 4) + "-" + phone.Substring(7, 4);
                    }
                }
            }
            else
            {
                rtn = phone;
            }

            return rtn;
        }
        #endregion

        #region [ 주민등록번호 ]
        /// <summary>
        /// 주민번호로 성별(M : 남자, F : 여자)
        /// </summary>
        /// <param name="serial">주민등록번호 13자리</param>
        /// <returns>M, F</returns>
        public static string SerialToGender(string serial)
        {
            string rtn = string.Empty;
            if (serial.Length == 13)
            {
                string gender = serial.Substring(6, 1);
                if (gender.Equals("9") || gender.Equals("1") || gender.Equals("3") || gender.Equals("5") || gender.Equals("7"))
                    rtn = "M";
                else
                    rtn = "F";
            }

            return rtn;
        }

        /// <summary>
        /// 주민번호로 생년월일 EX) 19801216
        /// </summary>
        /// <param name="serial">주민등록번호 13자리</param>
        /// <returns>19801216</returns>
        public static string SerialToBirth(string serial)
        {
            string rtn = string.Empty;
            if (serial.Length == 13)
            {
                string gender = serial.Substring(6, 1);
                switch (gender)
                {
                    case "9": rtn = "18"; break;
                    case "0": rtn = "18"; break;
                    case "1": rtn = "19"; break;
                    case "2": rtn = "19"; break;
                    case "3": rtn = "20"; break;
                    case "4": rtn = "20"; break;
                    case "5": rtn = "19"; break;
                    case "6": rtn = "19"; break;
                    case "7": rtn = "20"; break;
                    case "8": rtn = "20"; break;
                    default: break;
                }
                rtn = rtn + serial.Substring(0, 6);
            }

            return rtn;
        }

        /// <summary>
        /// 주민번호로 성별(N : 내국, F : 외국)
        /// </summary>
        /// <param name="serial">주민등록번호 13자리</param>
        /// <returns>N, F</returns>
        public static string SerialToNation(string serial)
        {
            string rtn = string.Empty;
            if (serial.Length == 13)
            {
                string gender = serial.Substring(6, 1);
                switch (gender)
                {
                    case "9": rtn = "N"; break;
                    case "0": rtn = "N"; break;
                    case "1": rtn = "N"; break;
                    case "2": rtn = "N"; break;
                    case "3": rtn = "N"; break;
                    case "4": rtn = "N"; break;
                    case "5": rtn = "F"; break;
                    case "6": rtn = "F"; break;
                    case "7": rtn = "F"; break;
                    case "8": rtn = "F"; break;
                    default: break;
                }
            }

            return rtn;
        }
        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace MLib.Config
{
    public static class Section
    {
        #region [Web.config 셋팅 정보 읽기]
        /// <summary>
        /// 코드성 데이터를 SortedList 로 반환
        /// </summary>
        /// <param name="group">코드 그룹</param>
        /// <returns>SortedList</returns>
        public static SortedList List(string group)
        {
            SortedList list = new SortedList();
            NameValueCollection settings = (NameValueCollection)ConfigurationManager.GetSection("MLib.Config/" + group);
            if (settings != null)
            {
                foreach (string key in settings.AllKeys)
                {
                    list.Add(key.ToString(), settings[key].ToString());
                }
            }
            else
            {
                throw (new Exception("설정한 코드그룹을 찾을 수 없습니다."));
            }

            return list;
        }

        /// <summary>
        /// 코드성 데이터를 Dictionary 로 반환
        /// </summary>
        /// <param name="group">코드 그룹</param>
        /// <returns>SortedList</returns>
        public static Dictionary<string, string> Dictionary(string group)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            NameValueCollection settings = (NameValueCollection)ConfigurationManager.GetSection("MLib.Config/" + group);
            if (settings != null)
            {
                foreach (string key in settings.AllKeys)
                {
                    list.Add(key.ToString(), settings[key].ToString());
                }
            }
            else
            {
                throw (new Exception("설정한 코드그룹을 찾을 수 없습니다."));
            }

            return list;
        }

        /// <summary>
        /// 코드성 데이터를 Key값으로 Value값을 찾음
        /// </summary>
        /// <param name="group">코드 그룹</param>
        /// <param name="key">코드 키</param>
        /// <returns>string 코드 키에 해당하는 값</returns>
        public static string Value(string group, string key)
        {
            string rtn = string.Empty;
            NameValueCollection settings = (NameValueCollection)ConfigurationManager.GetSection("MLib.Config/" + group);
            if (settings != null)
            {
                rtn = settings[key];
            }
            else
            {
                throw (new Exception("설정한 코드그룹을 찾을 수 없습니다."));
            }
            return rtn;
        }
        #endregion
    }
}

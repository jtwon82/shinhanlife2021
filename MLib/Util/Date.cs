using System;

namespace MLib.Util
{
    public static class Date
    {
        #region [ 날짜 관련 ]
        /// <summary>
        /// 요청 월의 마지막 일 (EX : 31)
        /// </summary>
        /// <param name="year">년</param>
        /// <param name="month">월</param>
        /// <returns>int : 31</returns>
        public static int LastDay(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        /// <summary>
        /// 요청 월의 마지막 일 (EX : 2019-03-20)
        /// </summary>
        /// <param name="year">년</param>
        /// <param name="month">월</param>
        /// <returns>string 날짜형식(ex : 2016-03-31)</returns>
        public static string LastDate(int year, int month)
        {
            DateTime date = new DateTime(year, month, 1);
            return date.ToString("yyyy") + "-" + date.ToString("MM") + "-" + LastDay(year, month);
        }

        /// <summary>
        /// 요일
        /// </summary>
        /// <param name="date">날짜(2015-01-12)</param>
        /// <returns>string 요일</returns>
        public static string Week(string date)
        {
            return DateTime.Parse(date).ToString("ddd");
        }

        /// <summary>
        /// 요일
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>int 요일 0(일) ~ 6(토)</returns>
        public static int Week(DateTime date)
        {
            int rtn = 0;
            var dt = date.DayOfWeek;

            switch (dt)
            {
                case DayOfWeek.Monday:      // 월
                    rtn = 1;
                    break;
                case DayOfWeek.Tuesday:     // 화
                    rtn = 2;
                    break;
                case DayOfWeek.Wednesday:   // 수
                    rtn = 3;
                    break;
                case DayOfWeek.Thursday:    // 목
                    rtn = 4;
                    break;
                case DayOfWeek.Friday:      // 금
                    rtn = 5;
                    break;
                case DayOfWeek.Saturday:    // 토
                    rtn = 6;
                    break;
                case DayOfWeek.Sunday:      // 일
                    rtn = 0;
                    break;
            }

            return rtn;
        }

        /// <summary>
        /// 날짜 차이 구하기
        /// </summary>
        /// <param name="interval">일=dd, 시간=hh, 분=mm, 초=ss</param>
        /// <param name="start">날짜</param>
        /// <param name="end">날짜</param>
        /// <returns>int 날짜 차이(interval 지정되로 반환)</returns>
        public static int DateDiff(string interval, DateTime start, DateTime end)
        {
            int rtn = 0;
            TimeSpan ts = end - start;

            switch (interval)
            {
                case "day":
                    rtn = ts.Days;
                    break;
                case "hour":
                    rtn = ts.Hours;
                    break;
                case "minutes":
                    rtn = ts.Minutes;
                    break;
                case "seconds":
                    rtn = ts.Seconds;
                    break;
            }

            return rtn;
        }

        /// <summary>
        /// 생년월일로 만나이 계산
        /// </summary>
        /// <param name="birth">생년월일 EX) 1980-12-16</param>
        /// <returns>int 만나이 반환</returns>
        public static int Age(string birth)
        {
            DateTime birthday = Convert.ToDateTime(birth);
            DateTime today = DateTime.Today;
            int start = (today.Year * 100 + today.Month) * 100 + today.Day;
            int end = (birthday.Year * 100 + birthday.Month) * 100 + birthday.Day;

            return (start - end) / 10000;
        }
        #endregion
    }
}

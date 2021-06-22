using System;
using MLib.Config;
using MLib.Logger;

namespace MLib.Util
{
    public class Error
    {
        /// <summary>
        /// 페이지 사용 - 에러 출력
        /// </summary>
        /// <param name="path">에러로그 경로</param>
        /// <param name="ex">에러</param>
        public static void WebHandler(Exception ex)
        {
            if (!ex.GetType().ToString().Equals("System.Threading.ThreadAbortException"))
            {
                Log log = new Log();
                log.Error(ex.ToString());

                if (!ServerVariables.ServerName.Equals("localhost"))
                    Tool.Print("처리도중 에러가 발생했습니다.<br/>관리자에 문의해주세요."+ ex.ToString());
                else
                    Tool.Print(ex.ToString());
                Tool.End();
            }
        }

        /// <summary>
        /// 백엔드 - 로그
        /// </summary>
        /// <param name="path">에러로그 경로</param>
        /// <param name="ex">에러</param>
        public static void ApiHandler(Exception ex)
        {
            if (!ex.GetType().ToString().Equals("System.Threading.ThreadAbortException"))
            {
                Log log = new Log();
                log.Error(ex.ToString());
            }
        }
    }
}

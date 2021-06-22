using System;
using System.IO;
using System.Web;

namespace MLib.Attach
{
    public class Download
    {
        private string _path;

        #region [ 생성자 ]
        /// <summary>
        /// 생성자
        /// </summary>
        public Download(string path)
        {
            _path = path;
        }
        #endregion

        #region [ 함수 ]
        public void Start()
        {
            Down();
        }

        public void Start(string filename)
        {
            Down(filename);
        }


        /// <summary>
        /// 첨부파일 다운로드
        /// </summary>
        /// <param name="path">파일 경로</param>
        private void Down()
        {
            FileInfo info = new FileInfo(_path);
            if (!info.Exists)
            {
                throw new Exception("파일이 존재하지 않습니다.");
            }
            else
            {
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(info.Name).Replace("+", "%20"));
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.WriteFile(_path);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 첨부파일 다운로드(다운로드 명 지정)
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <param name="filename">다운로드 명</param>
        private void Down(string filename)
        {
            FileInfo info = new FileInfo(_path);
            if (!info.Exists)
            {
                throw new Exception("파일이 존재하지 않습니다.");
            }
            else
            {
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(filename).Replace("+", "%20"));
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.WriteFile(_path);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        #endregion
    }
}

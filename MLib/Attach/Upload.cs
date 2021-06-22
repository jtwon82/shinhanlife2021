using MLib.Util;
using System;
using System.IO;
using System.Web.UI.WebControls;

namespace MLib.Attach
{
    public class Upload : File
    {
        private FileUpload _file = null;
        private string _path = string.Empty;
        private bool _overwrite = false;
        private string _filename = string.Empty;

        #region [ 속성 ]
        /// <summary>
        /// 첨부파일(원본 파일명)
        /// </summary>
        public string FileName { get { return _filename; } }

        /// <summary>
        /// 첨부된 파일 명 (실 저장된 파일명)
        /// </summary>
        public string SaveFileName { get { return this._saveFileName; } }

        /// <summary>
        /// 저장된 파일 경로
        /// </summary>
        public string SaveFilePath { get { return this._saveFilePath; } }

        /// <summary>
        /// 첨부된 파일 사이즈
        /// </summary>
        public long Size { get { return this._size; } }

        /// <summary>
        /// 첨부된 파일 확장자
        /// </summary>
        public string Ext { get { return this._ext; } }

        /// <summary>
        /// 첨부된 파일 형식(Mime type)
        /// </summary>
        public string ContentType { get { return this._type; } }

        /// <summary>
        /// 첨부 결과
        /// </summary>
        public bool Result { get { return this._result; } }
        #endregion

        #region [ 생성자 ]
        /// <summary>
        /// 첨부파일 업로드
        /// </summary>
        /// <param name="file">업로드 컨트롤</param>
        /// <param name="path">업로드 경로</param>
        public Upload(FileUpload file, string path)
        {
            this._file = file;
            string ext = Path.GetExtension(path);
            if (Check.IsNone(ext))
            {
                DateTime dt = DateTime.Now;
                _path = Path.Combine(path, dt.ToString("yyyy"), dt.ToString("MM"), Guid.NewGuid().ToString("N") + Path.GetExtension(file.PostedFile.FileName));
            }
            else
            {
                _path = path;
            }
        }

        /// <summary>
        /// 첨부파일 업로드(파일명 지정)
        /// </summary>
        /// <param name="file">FileUpload 컨트롤</param>
        /// <param name="path">파일 전체경로</param>
        /// <param name="overwrite">덮어쓰기 옵션(true : 덮어쓰기 허용, false : 덮어쓰기 금지)</param>
        public Upload(FileUpload file, string path, bool overwrite)
            : this(file, path)
        {
            _overwrite = overwrite;
        }
        #endregion

        #region [ 함수 ]
        /// <summary>
        /// 업로드 실행
        /// </summary>
        public void Attached()
        {
            if (_file.HasFile)
            {
                string type = _file.PostedFile.ContentType.ToString().ToLower();

                if (this.LimitType(type))
                {
                    string path = this.Distinct(_path, _overwrite);

                    Directory.CreateDirectory(Path.GetDirectoryName(_path));
                    _file.PostedFile.SaveAs(path);

                    this._filename = _file.FileName;
                    this._type = _file.PostedFile.ContentType;
                    this.Properties(new FileInfo(path));
                }
                else
                {
                    throw new Exception("허용되지 않은 파일 형식입니다. 파일 형식을 확인해주세요.");
                }
            }
        }
        #endregion
    }
}

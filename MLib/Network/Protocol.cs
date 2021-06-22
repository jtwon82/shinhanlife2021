using System;
using System.IO;
using System.Net;
using System.Text;

namespace MLib.Network
{
    /// <summary>
    /// 서버통신
    /// </summary>
    public class Protocol
    {
        #region [ 필드 ]
        private string _method;
        private string _url;
        private string _data;
        private string _status;
        private string _description;
        private string _result;
        private string _type;
        private long _length;
        private string _header;
        private string _charset;
        private string _server;
        private string _version;
        #endregion

        #region [ 속성 ]
        /// <summary>
        /// 통신 상태 (성공 : OK)
        /// </summary>
        public string Status
        {
            get
            {
                return this._status;
            }
        }

        /// <summary>
        /// 통신 설명
        /// </summary>
        public string Description
        {
            get
            {
                return this._description;
            }
        }

        /// <summary>
        /// 통신 리턴 값
        /// </summary>
        public string Result
        {
            get
            {
                return this._result;
            }
        }

        /// <summary>
        /// 응답 ContentType
        /// </summary>
        public string ContentType
        {
            get
            {
                return this._type;
            }
        }

        /// <summary>
        /// 응답 사이즈
        /// </summary>
        public long ContentLength
        {
            get
            {
                return this._length;
            }
        }

        /// <summary>
        /// 응답 Header
        /// </summary>
        public string Header
        {
            get
            {
                return this._header;
            }
        }

        /// <summary>
        /// 응답 언어 셋
        /// </summary>
        public string CharSet
        {
            get
            {
                return this._charset;
            }
        }

        /// <summary>
        /// 응답 서버정보
        /// </summary>
        public string Server
        {
            get
            {
                return this._server;
            }
        }

        /// <summary>
        /// 통신버전
        /// </summary>
        public string ProtocolVersion
        {
            get
            {
                return this._version;
            }
        }
        #endregion

        #region [ 생성자 ]
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="method">요청 방식 POST 또는 GET</param>
        /// <param name="url">요청 주소</param>
        /// <param name="data">요청 데이터</param>
        public Protocol(string method, string url, string data)
        {
            this._method = method;
            this._url = url;
            this._data = data;
        }
        #endregion

        #region [ 메서드 ]
        /// <summary>
        /// HTTP통신
        /// </summary>
        public void Start()
        {
            try
            {
                Uri uri;
                if (_method.ToUpper() == "POST")
                    uri = new Uri(_url);
                else
                    uri = new Uri(_url + "?" + _data);

                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
                req.ContentType = "application/x-www-form-urlencoded";

                if (_method.ToUpper() == "POST")
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(_data);
                    req.Method = WebRequestMethods.Http.Post;
                    req.ContentLength = buffer.Length;

                    Stream post = req.GetRequestStream();
                    post.Write(buffer, 0, buffer.Length);
                    post.Close();
                }
                else
                {
                    req.Method = WebRequestMethods.Http.Get;
                }

                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    using (Stream stream = res.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            this._result = reader.ReadToEnd();
                        }
                    }

                    // 통신응답 값 속성으로 셋팅
                    this._status = res.StatusCode.ToString();
                    this._description = res.StatusDescription;
                    this._header = res.Headers.ToString();
                    this._length = res.ContentLength;
                    this._type = res.ContentType;
                    this._charset = res.CharacterSet;
                    this._server = res.Server;
                    this._version = res.ProtocolVersion.ToString();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    this._status = ((HttpWebResponse)ex.Response).StatusCode.ToString();
                    this._description = ex.Message;
                }
                else
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        #endregion
    }
}

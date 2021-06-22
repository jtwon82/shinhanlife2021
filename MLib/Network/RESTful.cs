using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MLib.Network
{
    public class RESTful
    {
        private string _url = string.Empty;

        private bool _result = false;
        private string _json = string.Empty;

        public bool Result { get { return _result; } }
        public string Json { get { return _json; } }
        public string Authorization { get; set; }

        public RESTful(string url)
        {
            _url = url;
        }

        /// <summary>
        /// Method POST
        /// </summary>
        public void Post(string json)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Timeout = 30 * 1000;
            if (!Util.Check.IsNone(Authorization))
                request.Headers.Add("Authorization", Authorization);

            byte[] bytes = Encoding.UTF8.GetBytes(json);
            request.ContentLength = bytes.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            // Response 처리
            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                HttpStatusCode status = resp.StatusCode;

                if (status == HttpStatusCode.OK)
                {
                    _result = true;

                    Stream respStream = resp.GetResponseStream();
                    using (StreamReader sr = new StreamReader(respStream))
                    {
                        _json = sr.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// Method GET
        /// </summary>
        public void Get()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "GET";
            request.Timeout = 30 * 1000;
            if (!Util.Check.IsNone(Authorization))
                request.Headers.Add("Authorization", Authorization);

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                if (resp.StatusCode.Equals(HttpStatusCode.OK))
                {
                    _result = true;

                    Stream respStream = resp.GetResponseStream();
                    using (StreamReader sr = new StreamReader(respStream))
                    {
                        _json = sr.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// Method POST multipart/form-data
        /// </summary>
        public void Post(List<Parameters> parameters)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.Timeout = 30 * 1000;
            request.KeepAlive = true;
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;

            if (parameters != null && parameters.Count > 0)
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    foreach (Parameters item in parameters)
                    {
                        requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                        if (item.FileInfo == null)
                        {
                            string data = "Content-Disposition: form-data; name=\"" + item.Key + "\"\r\n\r\n" + item.Value;
                            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                            requestStream.Write(bytes, 0, bytes.Length);
                        }
                        else
                        {
                            string header = "Content-Disposition: form-data; name=" + item.Key + "; filename=" + Path.GetFileName(item.FileInfo.Path) + "\r\nContent-Type: " + item.FileInfo.ContentType + "\r\n\r\n";
                            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(header);
                            requestStream.Write(bytes, 0, bytes.Length);

                            byte[] buffer = new byte[32768];
                            int bytesRead;
                            if (item.FileInfo.Stream == null)
                            {
                                using (FileStream fileStream = File.OpenRead(item.FileInfo.Path))
                                {
                                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                        requestStream.Write(buffer, 0, bytesRead);
                                    fileStream.Close();
                                }
                            }
                            else
                            {
                                while ((bytesRead = item.FileInfo.Stream.Read(buffer, 0, buffer.Length)) != 0)
                                    requestStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }

                    byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                    requestStream.Write(trailer, 0, trailer.Length);
                    requestStream.Close();
                }

                // Response 처리
                using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
                {
                    if (resp.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        _result = true;

                        Stream respStream = resp.GetResponseStream();
                        using (StreamReader sr = new StreamReader(respStream))
                        {
                            _json = sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}

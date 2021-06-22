using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ExcelDataReader.Log;
using MLib.Attach;
using MLib.DataBase;
using MLib.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MLib.Config;

namespace OrangeSummer.Web.MasterApplication.controllers
{
    public class EditorController : ApiController
    {
        private MLib.Logger.Log _log = new MLib.Logger.Log();

        /// <summary>
        /// 에디터 첨부파일 업로드
        /// </summary>
        [HttpPost]
        [ActionName("upload")]
        public async Task<HttpResponseMessage> Upload()
        {
            HttpResponseMessage resp = null;
            try
            {
                DateTime dt = DateTime.Now;

                Json json = new Json() { Result = "FAIL" };
                string path = Path.Combine(ServerVariables.uploadFullPath, dt.ToString("yyyy"), dt.ToString("MM")); // Common.Master.AppSetting.Path, "file", "temp");
                //string path = Path.Combine(dt.ToString("yyyy"), dt.ToString("MM"));
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!Request.Content.IsMimeMultipartContent())
                {
                    resp = new HttpResponseMessage() { StatusCode = HttpStatusCode.UnsupportedMediaType };
                }
                else
                {
                    var provider = new MultipartFormDataStreamProvider(path);
                    await Request.Content.ReadAsMultipartAsync(provider);

                    try
                    {
                        foreach (MultipartFileData item in provider.FileData)
                        {
                            string ext = Path.GetExtension(item.Headers.ContentDisposition.FileName.Replace("\"", ""));
                            string newFilename = Tool.Unique + ext;
                            path = Path.Combine(path, newFilename);
                            System.IO.File.Copy(item.LocalFileName, path, true);
                            string _uri = Path.Combine(dt.ToString("yyyy"), dt.ToString("MM"), newFilename);
                            
                            json.Result = "SUCCESS";
                            json.Url = Common.Master.AppSetting.uploadFileUrl(_uri);

                        }

                        JsonSerializerSettings jss = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                        string stringfy = JsonConvert.SerializeObject(json, jss);
                        resp = new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new StringContent(stringfy, Encoding.UTF8, "application/json")
                        };
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally
                    {
                        foreach (MultipartFileData item in provider.FileData)
                        {
                            if (System.IO.File.Exists(item.LocalFileName))
                                System.IO.File.Delete(item.LocalFileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MLib.Util.Error.ApiHandler(ex);
                resp = new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError };
            }

            return resp;
        }

        private class Json
        {
            public string Result { get; set; }
            public string Url { get; set; }
        }
    }
}
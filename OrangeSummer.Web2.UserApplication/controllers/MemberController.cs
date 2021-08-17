using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrangeSummer.Common;
using System.Threading.Tasks;
using MLib.Config;
using System.IO;
using MLib.Util;
using MLib.Data;
using MLib.Logger;
using MLib.Auth;

namespace OrangeSummer.Web2.UserApplication.controllers
{
    public class MemberController : ApiController
    {

        /// <summary>
        /// 사용자 코드 중복체크
        /// </summary>
        [HttpPost]
        [ActionName("checkPno")]
        public HttpResponseMessage checkPno(MemberRequest request)
        {
            HttpResponseMessage resp = null;
            try
            {
                Json json = new Json() { Result = "FAIL", Message = "사용할 수 없는 연락처입니다." };
                using (Business.Member biz = new Business.Member(Common.User.AppSetting.Connection))
                {
                    string result = biz.UserCheckPno(request.Mobile);
                    if (result == "EXISTS")
                    {
                        json.Result = "EXISTS";
                        json.Message = "이미 등록된 연락처입니다.";
                    }
                    else if (result == "SUCCESS")
                    {
                        json.Result = "SUCCESS";
                        json.Message = "사용 가능한 연락처입니다.";
                    }
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
                MLib.Util.Error.ApiHandler(ex);
                resp = new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError };
            }

            return resp;
        }

        /// <summary>
        /// 사용자 코드 중복체크
        /// </summary>
        [HttpPost]
        [ActionName("checkCode")]
        public HttpResponseMessage checkCode(MemberRequest request)
        {
            HttpResponseMessage resp = null;
            try
            {
                Json json = new Json() { Result = "FAIL", Message = "사용할 수 없는 코드입니다." };
                using (Business.Member biz = new Business.Member(Common.User.AppSetting.Connection))
                {
                    string result = biz.UserCheck(request.Code, request.Name);
                    if (result == "EXISTS")
                    {
                        json.Result = "EXISTS";
                        json.Message = "이미 사용 중인 코드입니다.";
                    }
                    else if (result == "VALIDITY")
                    {
                        json.Result = "FAIL";
                        json.Message = "정보가 맞지 않습니다.\n지점, 신분, FC코드를 다시 확인 해 주세요.";
                    }
                    else if (result == "SUCCESS")
                    {
                        json.Result = "SUCCESS";
                        json.Message = "사용 가능한 코드입니다.";
                    }
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
                MLib.Util.Error.ApiHandler(ex);
                resp = new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError };
            }

            return resp;
        }

        /// <summary>
        /// 사용자 코드 중복체크 V3
        /// </summary>
        [HttpPost]
        [ActionName("checkCodeV3")]
        public HttpResponseMessage checkCodeV3(MemberRequest request)
        {
            HttpResponseMessage resp = null;
            try
            {
                Json json = new Json() { Result = "FAIL", Message = "." };
                using (Business.Member biz = new Business.Member(Common.User.AppSetting.Connection))
                {
                    string result = biz.UserCheckV3(request.Code, request.Name);
                    json.Result = result;
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
                MLib.Util.Error.ApiHandler(ex);
                resp = new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError };
            }

            return resp;
        }

        /// <summary>
        /// 에디터 첨부파일 업로드
        /// </summary>
        [HttpPost]
        [ActionName("fileupload")]
        public async Task<HttpResponseMessage> fileupload()
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
                            Boolean able = Common.Master.AppSetting.ABLE_UPLOAD_FILE_EXT.Contains(ext.ToLower());
                            
                            if (able)
                            {
                                string newFilename = Tool.Unique + ext;
                                path = Path.Combine(path, newFilename);
                                System.IO.File.Copy(item.LocalFileName, path, true);
                                string _uri = Path.Combine(dt.ToString("yyyy"), dt.ToString("MM"), newFilename);

                                json.Result = "SUCCESS";
                                json.Url = Common.Master.AppSetting.uploadFileUrl(_uri);
                            }
                            else
                            {
                                json.Result = "FAIL";
                            }
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

        /// <summary>
        /// 이미지변경
        /// </summary>
        [HttpPost]
        [ActionName("updateImg")]
        public HttpResponseMessage updateImg(MemberRequest request)
        {
            HttpResponseMessage resp = null;
            try
            {
                Json json = new Json() { Result = "FAIL" };

                Model.Member member = new Model.Member();
                using (Business.Member biz = new Business.Member(Common.Master.AppSetting.Connection))
                {
                    member = biz.UserDetail(Common.User.Identify.Id);
                    if (request.mode== "PROFILE")
                    {
                        member.ProfileImg = request.url;
                        //OrangeSummer.Common.User.Identify.ProfileImg = request.url;
                        string[] array = { member.Id, member.Code, member.Name, member.Level, member.Branch.Id, member.Branch.Name, member.ProfileImg, member.BackgroundImg };
                        Forms.Authorize(OrangeSummer.Common.User.AppSetting.EncKey, member.Id, array);

                    }
                    else
                    {
                        member.BackgroundImg = request.url;
                    }
                }

                using (Business.Member biz = new Business.Member(Common.User.AppSetting.Connection))
                {
                    bool result = biz.UserModify(member);
                    if (result)
                    {
                        json.Result = "SUCCESS";
                        //JS.Move("회원정보 수정이 완료되었습니다.", "./");
                    }
                }

                JsonSerializerSettings jss = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                string stringfy = JsonConvert.SerializeObject(json, jss);
                resp = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(stringfy, System.Text.Encoding.UTF8, "application/json")
                };
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
            public string Message { get; set; }
            public string Url { get; internal set; }
        }

        public class MemberRequest
        {
            public string mode { get; set; }
            public string Branch { get; set; }
            public string Level { get; set; }
            public string Code { get; set; }
            public string url { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
        }
    }
}
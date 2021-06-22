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

namespace OrangeSummer.Web.UserApplication.controllers
{
    public class MemberController : ApiController
    {
        /// <summary>
        /// 사용자 코드 중복체크
        /// </summary>
        [HttpPost]
        [ActionName("check")]
        public HttpResponseMessage Post(MemberRequest request)
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
                        json.Result = "FAIL";
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

        private class Json
        {
            public string Result { get; set; }
            public string Message { get; set; }
        }

        public class MemberRequest
        {
            public string Branch { get; set; }
            public string Level { get; set; }
            public string Code { get; set; }

            public string Name { get; set; }
        }
    }
}
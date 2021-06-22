using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrangeSummer.Common;

namespace OrangeSummer.Web.MasterApplication.controllers
{
    public class BranchController : ApiController
    {
        /// <summary>
        /// 지점 중복체크
        /// </summary>
        [HttpPost]
        [ActionName("check")]
        public HttpResponseMessage Post(BranchRequest request)
        {
            HttpResponseMessage resp = null;
            try
            {
                Json json = new Json();
                using (Business.Branch biz = new Business.Branch(Common.Master.AppSetting.Connection))
                {
                    Model.Branch admin = biz.Check(request.Name);
                    if (admin != null)
                    {
                        json.Result = "FAIL";
                        json.Message = "이미 사용 중인 지점명입니다.";
                    }
                    else
                    {
                        json.Result = "SUCCESS";
                        json.Message = "사용 가능한 지점명입니다.";
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

        public class BranchRequest
        {
            public string Name { get; set; }
        }
    }
}
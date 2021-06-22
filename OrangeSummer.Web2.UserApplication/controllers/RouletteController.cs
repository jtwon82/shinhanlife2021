using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MLib.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrangeSummer.Common;

namespace OrangeSummer.Web2.UserApplication.controllers
{
    public class RouletteController : ApiController
    {
        #region [ 룰렛 당첨여부 ]
        /// <summary>
        /// UCC댓글 삭제
        /// </summary>
        [HttpPost]
        [ActionName("play")]
        public HttpResponseMessage Play(PlayRequest request)
        {
            HttpResponseMessage resp = null;
            try
            {
                PlayResponse json = new PlayResponse() { Result = "FAIL", Message = "실패" };
                using (Business.Roulette biz = new Business.Roulette(Common.User.AppSetting.Connection))
                {
                    bool result = biz.UserRegist(new Model.Roulette()
                    {
                        Id = Tool.UniqueNewGuid,
                        FkMember = Common.User.Identify.Id,
                        Result = request.Result
                    });

                    if (result)
                    {
                        json.Result = "SUCCESS";
                        json.Message = "성공";
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

        public class PlayRequest
        {
            public string Result { get; set; }
        }

        private class PlayResponse
        {
            public string Result { get; set; }
            public string Message { get; set; }
        }
        #endregion
    }
}
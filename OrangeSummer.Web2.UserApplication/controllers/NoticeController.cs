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
using MLib.Logger;

namespace OrangeSummer.Web2.UserApplication.controllers
{
    public class NoticeController : ApiController
    {
        #region [ 공지사항 댓글 좋아요 ]
        /// <summary>
        /// 공지사항 댓글 좋아요
        /// </summary>
        [HttpPost]
        [ActionName("like")]
        public HttpResponseMessage Like(LikeRequest request)
        {
            HttpResponseMessage resp = null;
            try
            {
                LikeResponse json = new LikeResponse();
                using (Business.NoticeReply biz = new Business.NoticeReply(Common.User.AppSetting.Connection))
                {
                    Model.NoticeReplyLike like = biz.UserLike(request.Id, Common.User.Identify.Id);
                    json.Result = like.Result;
                    json.Count = like.LikeCount;
                    if (like.Result == "PLUS")
                        json.Message = "좋아요";
                    else
                        json.Message = "좋아요 취소";
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

        public class LikeRequest
        {
            public string Id { get; set; }
        }

        private class LikeResponse
        {
            public string Result { get; set; }
            public int Count { get; set; }
            public string Message { get; set; }
        }
        #endregion

        #region [ 공지사항 댓글 등록 ]
        /// <summary>
        /// 공지사항 댓글 등록
        /// </summary>
        [HttpPost]
        [ActionName("regist")]
        public HttpResponseMessage Regist(RegistRequest request)
        {
            HttpResponseMessage resp = null;
            try
            {
                RegistResponse json = new RegistResponse() { Result = "FAIL", Message = "실패" };
                
                using (Business.NoticeReply biz = new Business.NoticeReply(Common.User.AppSetting.Connection))
                {
                    bool result = biz.UserRegist(new Model.NoticeReply()
                    {
                        Id = Tool.UniqueNewGuid,
                        FkNotice = request.Id,
                        FkMember = Common.User.Identify.Id,
                        Contents = request.Contents.Replace("\n", Environment.NewLine)
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

        public class RegistRequest
        {
            public string Id { get; set; }
            public string Contents { get; set; }
        }

        private class RegistResponse
        {
            public string Result { get; set; }
            public string Message { get; set; }
        }
        #endregion

        #region [ 공지사항 댓글 수정 ]
        /// <summary>
        /// 공지사항 댓글 수정
        /// </summary>
        [HttpPost]
        [ActionName("modify")]
        public HttpResponseMessage Modify(ModifyRequest request)
        {
            HttpResponseMessage resp = null;
            try
            {
                ModifyResponse json = new ModifyResponse() { Result = "FAIL" };
                using (Business.NoticeReply biz = new Business.NoticeReply(Common.User.AppSetting.Connection))
                {
                    bool result = biz.UserModify(new Model.NoticeReply()
                    {
                        Id = request.Id,
                        FkMember = Common.User.Identify.Id,
                        Contents = request.Contents.Replace("\n", Environment.NewLine)
                    });

                    if (result)
                    {
                        json.Result = "SUCCESS";
                        json.Message = request.Contents.Replace("\n", "<br>");
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

        public class ModifyRequest
        {
            public string Id { get; set; }
            public string Contents { get; set; }
        }

        private class ModifyResponse
        {
            public string Result { get; set; }
            public string Message { get; set; }
        }
        #endregion

        #region [ 공지사항 댓글 답글 ]
        /// <summary>
        /// 공지사항 댓글 답글
        /// </summary>
        [HttpPost]
        [ActionName("answer")]
        public HttpResponseMessage Answer(AnswerRequest request)
        {
            HttpResponseMessage resp = null;
            try
            {
                AnswerResponse json = new AnswerResponse() { Result = "FAIL", Message = "실패" };
                using (Business.NoticeReply biz = new Business.NoticeReply(Common.User.AppSetting.Connection))
                {
                    bool result = biz.UserAnswer(new Model.NoticeReply()
                    {
                        Id = request.Id,
                        FkMember = Common.User.Identify.Id,
                        Contents = request.Contents.Replace("\n", Environment.NewLine)
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

        public class AnswerRequest
        {
            public string Id { get; set; }
            public string Contents { get; set; }
        }

        private class AnswerResponse
        {
            public string Result { get; set; }
            public string Message { get; set; }
        }
        #endregion

        #region [ 공지사항 댓글 삭제 ]
        /// <summary>
        /// 공지사항 댓글 삭제
        /// </summary>
        [HttpPost]
        [ActionName("delete")]
        public HttpResponseMessage Delete(DeleteRequest request)
        {
            HttpResponseMessage resp = null;
            try
            {
                DeleteResponse json = new DeleteResponse() { Result = "FAIL", Message = "실패" };
                using (Business.NoticeReply biz = new Business.NoticeReply(Common.User.AppSetting.Connection))
                {
                    bool result = biz.UserDelete(request.Id, Common.User.Identify.Id);

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

        public class DeleteRequest
        {
            public string Id { get; set; }
        }

        private class DeleteResponse
        {
            public string Result { get; set; }
            public string Message { get; set; }
        }
        #endregion
    }
}
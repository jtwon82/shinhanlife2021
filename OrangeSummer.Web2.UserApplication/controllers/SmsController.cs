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
using OrangeSummer.Web2.UserApplication.kr.co.youiwe.webservice;
using MLib.Util;
using MLib.Cipher;
using MLib.Logger;
using System.Web.Security;

namespace OrangeSummer.Web2.UserApplication.controllers
{
    public class SmsController : ApiController
    {
        /// <summary>
        /// 문자발송
        /// </summary>
        [HttpPost]
        [ActionName("sendMsg")]
        public HttpResponseMessage sendMsg(UserRequest request)
        {
            Json json = new Json();

            string sms_id = "mayday33";   //부여 받은 너나우리 SMS 아이디를 넣으십시요.
            string sms_pwd = "qwer1234";  //부여 받은 너나우리 SMS 패스워드를 넣으십시요.

            string result = "";
            string snd_number = "1588-5005";
            string rcv_number = request.Pno;
            string rand_no = Tool.RandomInt(4);
            string sms_content = $"[신한라이프] 인증번호 {rand_no}을 입력해 주세요.";
            string reserve_date = "";
            string reserve_time = "";
            string userdefine = sms_id;  //예약취소를 위해 넣어주는 구분자 정의값, 사용자 임의로 지정해주시면 됩니다. 영문으로 넣어주셔야 합니다. 사용자가 구분할 수 있는 값을 넣어주세요.
            string canclemode = "1";     //예약 취소 모드 1: 사용자정의값에 의한 삭제.  현재는 무조건 1을 넣어주시면 됩니다.


            if (Common.User.AppSetting.DevMode == "DEV")
            {
                json.Result = "SUCCESS";
                json.Message = "DEVMODE " + rand_no;
                MLib.Auth.Web.Cookies("ORANGESUMMER", "RNDNO", AES.Encrypt(Common.User.AppSetting.EncKey, $"{rand_no}"), 1);
            }
            else
            {
                string hash_value = FormsAuthentication.HashPasswordForStoringInConfigFile(sms_id + sms_pwd, "MD5"); //해쉬값생성
                ServiceSMS oSoap = new ServiceSMS();

                int ableCnt = oSoap.GetRemainCount(sms_id, hash_value);
                if (ableCnt < 1)
                {
                    json.Result = "FAIL";
                    json.Message = "GetRemainCount : " + ableCnt;
                }
                else
                {
                    hash_value = FormsAuthentication.HashPasswordForStoringInConfigFile(sms_id + sms_pwd + rcv_number, "MD5"); //해쉬값생성

                    //SMS 즉시발송 함수호출
                    result = oSoap.SendSMS(sms_id, hash_value, snd_number, rcv_number, sms_content);

                    json.Code = result;
                    if (result == "1")
                    {
                        json.Result = "SUCCESS";
                        MLib.Auth.Web.Cookies("ORANGESUMMER", "RNDNO", AES.Encrypt(Common.User.AppSetting.EncKey, $"{rand_no}"), 1);
                    }
                    else
                    {
                        json.Result = "FAIL";
                        json.Message = "";
                    }
                }
            }

            HttpResponseMessage resp = null;
            try
            {
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
        /// compareRndNo
        /// </summary>
        [HttpPost]
        [ActionName("compareRndNo")]
        public HttpResponseMessage compareRndNo(UserRequest request)
        {
            Json json = new Json();

            if (Common.Master.AppSetting.DevMode == "DEV")
            {
                json.Result = "SUCCESS";
            }
            else
            {
                if (request.RndNo == "")
                {
                    json.Result = "FAIL";
                }
                else
                {
                    string rndNo = AES.Decrypt(Common.User.AppSetting.EncKey, MLib.Auth.Web.Cookies("ORANGESUMMER", "RNDNO"));
                    
                    if (rndNo == request.RndNo)
                    {
                        json.Result = "SUCCESS";
                    }
                    else
                    {
                        json.Result = "FAIL";
                    }
                }
            }


            HttpResponseMessage resp = null;
            try
            {
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
            public string Code { get; set; }
            public string Message { get; set; }
        }

        public class UserRequest
        {
            public string Usr { get; set; }
            public string Result { get; set; }
            public string Pno { get; set; }
            public string RndNo { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLib.Config;

namespace OrangeSummer.Common.User
{
    public static class AppSetting
    {
        /// <summary>
        /// DEV_MODE
        /// </summary>
        public static string DevMode
        {
            get
            {
                return ConfigurationManager.AppSettings["DEV_MODE"];
            }
        }

        /// <summary>
        /// DB Connection
        /// </summary>
        public static string Connection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DB_CONNECTION"].ConnectionString;
            }
        }

        /// <summary>
        /// 암호화 키
        /// </summary>
        public static string EncKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ENCRYPT_KEY"];
            }
        }

        /// <summary>
        /// 사이트 타이틀
        /// </summary>
        public static string SiteTitle
        {
            get
            {
                return ConfigurationManager.AppSettings["SITE_TITLE"];
            }
        }

        /// <summary>
        /// S3 Access Key
        /// </summary>
        public static string AwsAccess
        {
            get
            {
                return ConfigurationManager.AppSettings["AWS_ACCESS"];
            }
        }

        /// <summary>
        /// S3 Secret Key
        /// </summary>
        public static string AwsSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["AWS_SECRET"];
            }
        }

        /// <summary>
        /// S3 Bucket
        /// </summary>
        public static string AwsBucket
        {
            get
            {
                return ConfigurationManager.AppSettings["AWS_BUCKET"];
            }
        }

        /// <summary>
        /// S3 Public Url
        /// </summary>
        public static string AwsUrl(string key)
        {
            //return $"https://{AppSetting.AwsBucket}.s3.ap-northeast-2.amazonaws.com/{key}";
            return uploadFileUrl(key);
        }
        public static string uploadFileUrl(string key)
        {
            return $"/upload/{key}";
        }

        /// <summary>
        /// 업로드 경로
        /// </summary>
        public static string Path
        {
            get
            {
                return ServerVariables.ApplPhysicalPath;
            }
        }

        /// <summary>
        /// 관리자 페이지 타이틀
        /// </summary>
        public static string Title
        {
            get
            {
                string title = "################";
                switch (Agent.Directory(1))
                {
                    case "member":
                        switch (Agent.Directory(2))
                        {
                            case "regist": title = "회원가입"; break;
                            case "terms": title = "회원가입 약관"; break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }

                return title;
            }
        }
    }
}

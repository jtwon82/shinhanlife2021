using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLib.Config;

namespace OrangeSummer.Common.Master
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

        public static string ABLE_UPLOAD_FILE_EXT
        {
            get
            {
                return ConfigurationManager.AppSettings["ABLE_UPLOAD_FILE_EXT"];
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
        /// 관리자 페이지 타이틀
        /// </summary>
        public static string Title
        {
            get
            {
                string title = "################";
                switch (Agent.Directory(1))
                {
                    case "dash": title = "Dash Board"; break;
                    case "main":
                        switch (Agent.Directory(2))
                        {
                            case "banner": title = "배너 관리"; break;
                            default:
                                break;
                        }
                        break;
                    case "member": title = "회원 관리"; break;
                    case "measure": title = "시책 상세 관리"; break;
                    case "branch": title = "지점 관리"; break;
                    case "achievement": title = "업적 관리"; break;
                    case "ranking": title = "랭킹 관리"; break;
                    case "travel": title = "여행지 관리"; break;
                    case "board":
                        switch (Agent.Directory(2))
                        {
                            case "notice": title = "공지사항 관리"; break;
                            case "evt": title = "이벤트 관리"; break;
                            case "banner": title = "이벤트 배너 관리"; break;
                            case "ucc": title = "UCC 콘테스트"; break;
                            case "word": title = "여행지명 백일장 인기투표 이벤트"; break;
                            case "roulette": title = "룰렛 이벤트"; break;
                            case "agreement": title = "약관 관리"; break;
                            default:
                                break;
                        }
                        break;

                    case "qna": title = "1:1문의"; break;
                    case "admin": title = "관리자"; break;
                    default:
                        break;
                }

                return title;
            }
        }

        /// <summary>
        /// 업로드 경로
        /// </summary>
        public static string  Path 
        {
            get
            {
                return ServerVariables.uploadFullPath; // ApplPhysicalPath;
            }
        }
    }
}

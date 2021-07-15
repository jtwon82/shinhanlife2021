using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Amazon.S3;
using Amazon.S3.Model;
using MLib.Util;
using MLib.Config;
using System.Net;

namespace MLib.Attach
{
    public class S3
    {
        private string _access = string.Empty;
        private string _secret = string.Empty;
        private string _bucket = string.Empty;

        public bool Result { get; private set; }
        public string Key { get; private set; }
        public string Msg { get; private set; }
        public string SignedUrl { get; private set; }
        public bool IsPublic { get; set; } = false;

        public S3(string access, string secret, string bucket)
        {
            _access = access;
            _secret = secret;
            _bucket = bucket;
        }

        /// <summary>
        /// AWS S3 Upload
        /// </summary>
        public void Upload(string path)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string ext = Path.GetExtension(path).ToLower();
                string key = string.Format("{0}/{1}/{2}/{3}{4}", dt.ToString("yyyy"), dt.ToString("MM"), dt.ToString("dd"), Tool.Unique, ext);
                using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(_access, _secret, Amazon.RegionEndpoint.APNortheast2))
                {
                    PutObjectRequest req = new PutObjectRequest()
                    {
                        BucketName = _bucket,
                        CannedACL = IsPublic ? S3CannedACL.PublicRead : S3CannedACL.Private, // 권한
                        ServerSideEncryptionMethod = new ServerSideEncryptionMethod(ServerSideEncryptionMethod.AES256), // S3 암호화 사용
                        Key = key, // S3 Object 생성키(업로드될 경로 + 파일명)
                        FilePath = path // 파일 경로
                    };

                    client.PutObject(req);
                }

                this.Result = true;
                this.Msg    = "성공";
                this.Key    = key;
            }
            catch (Exception ex)
            {
                this.Result = false;
                this.Msg = ex.ToString();

                throw;
            }
        }

        /// <summary>
        /// AWS S3 Upload
        /// </summary>
        public void Upload(Stream stream, string ext)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string key = string.Format("{0}/{1}/{2}/{3}{4}", dt.ToString("yyyy"), dt.ToString("MM"), dt.ToString("dd"), Tool.Unique, ext);
                using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(_access, _secret, Amazon.RegionEndpoint.APNortheast2))
                {
                    PutObjectRequest req = new PutObjectRequest()
                    {
                        BucketName = _bucket,
                        CannedACL = IsPublic ? S3CannedACL.PublicRead : S3CannedACL.Private, // 권한
                        ServerSideEncryptionMethod = new ServerSideEncryptionMethod(ServerSideEncryptionMethod.AES256), // S3 암호화 사용
                        Key = key, // S3 Object 생성키(업로드될 경로 + 파일명)
                        InputStream = stream // 파일 스트림
                    };

                    client.PutObject(req);
                }

                this.Result = true;
                this.Msg = "성공";
                this.Key = key;
            }
            catch (Exception ex)
            {
                this.Result = false;
                this.Msg = ex.ToString();
                throw;
            }
        }

        /// <summary>
        /// AWS S3 Download
        /// </summary>
        public void Save(string key, string path)
        {
            try
            {
                using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(_access, _secret, Amazon.RegionEndpoint.APNortheast2))
                {
                    GetObjectRequest req = new GetObjectRequest
                    {
                        BucketName = _bucket,
                        Key = key
                    };

                    using (GetObjectResponse res = client.GetObject(req))
                    {
                        if (!System.IO.File.Exists(path))
                        {
                            res.WriteResponseStreamToFile(path);
                        }
                    }

                    this.Result = true;
                }
            }
            catch (Exception ex)
            {
                this.Result = false;
                this.Msg = ex.ToString();
            }
        }

        /// <summary>
        /// AWS S3 Download
        /// </summary>
        public void Download(string key, string filename)
        {
            try
            {
                //using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(_access, _secret, Amazon.RegionEndpoint.APNortheast2))
                //{
                //    GetObjectRequest req = new GetObjectRequest
                //    {
                //        BucketName = _bucket,
                //        Key = key
                //    };
                //    string dest = Path.Combine(HttpRuntime.CodegenDir, filename);
                //    using (GetObjectResponse response = client.GetObject(req))
                //    {
                //        response.WriteResponseStreamToFile(dest, false);
                //    }
                //    HttpContext.Current.Response.Clear();
                //    HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + filename);
                //    HttpContext.Current.Response.ContentType = "application/octet-stream";
                //    HttpContext.Current.Response.TransmitFile(dest);
                //    HttpContext.Current.Response.Flush();
                //    HttpContext.Current.Response.End();
                //    System.IO.File.Delete(dest);
                //    this.Result = true;
                //}

                string dest = Path.Combine(ServerVariables.uploadFullPath, filename);

                WebClient mywebClient = new WebClient();

                mywebClient.DownloadFile(Path.Combine("/upload/", key), Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + filename);
                
                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=" + filename);
                //HttpContext.Current.Response.ContentType = "application/octet-stream";
                //HttpContext.Current.Response.TransmitFile(dest);
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.End();

                this.Result = true;
            }
            catch (Exception ex)
            {
                this.Result = false;
                this.Msg = ex.ToString();
            }
        }

        /// <summary>
        /// AWS S3 Download
        /// </summary>
        public void Delete(string key)
        {
            try
            {
                using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(_access, _secret, Amazon.RegionEndpoint.APNortheast2))
                {
                    DeleteObjectRequest req = new DeleteObjectRequest
                    {
                        BucketName = _bucket,
                        Key = key
                    };

                    client.DeleteObject(req);
                }

                this.Result = true;
            }
            catch (Exception ex)
            {
                this.Result = false;
                this.Msg = ex.ToString();
            }
        }

        /// <summary>
        /// AWS S3 SRC
        /// </summary>
        public void Src(string key)
        {
            try
            {
                using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(_access, _secret, Amazon.RegionEndpoint.APNortheast2))
                {
                    GetObjectRequest req = new GetObjectRequest
                    {
                        BucketName = _bucket,
                        Key = key
                    };

                    using (GetObjectResponse res = client.GetObject(req))
                    {
                        using (BinaryReader br = new BinaryReader(res.ResponseStream))
                        {
                            Byte[] bytes = br.ReadBytes((Int32)res.ResponseStream.Length);

                            HttpContext.Current.Response.Buffer = true;
                            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + key);
                            HttpContext.Current.Response.BinaryWrite(bytes);
                            HttpContext.Current.Response.Flush();
                            HttpContext.Current.Response.End();
                        }
                    }
                }

                this.Result = true;
            }
            catch (Exception ex)
            {
                this.Result = false;
                this.Msg = ex.ToString();
            }
        }

        /// <summary>
        /// 파일경로
        /// </summary>
        public void Url(string key, DateTime dt)
        {
            using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(_access, _secret, Amazon.RegionEndpoint.APNortheast2))
            {
                GetPreSignedUrlRequest req = new GetPreSignedUrlRequest
                {
                    BucketName = _bucket,
                    Key = key,
                    Expires = dt
                };

                this.Result = true;
                this.SignedUrl = client.GetPreSignedURL(req);
            }
        }
    }
}

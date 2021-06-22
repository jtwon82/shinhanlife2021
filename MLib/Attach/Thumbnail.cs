using MLib.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace MLib.Attach
{
    public class Thumbnail
    {
        #region [ 필드 ]
        private bool _result = false;
        private string _saveFileName;
        private string _saveFilePath;
        private int _width;
        private int _height;
        private long _size;

        private string _path;
        private string _save;
        private int _w;
        private int _h;
        private int _quality;
        #endregion

        #region [ 속성 ]
        /// <summary>
        /// 썸네일 이미지 명
        /// </summary>
        public string SaveFileName
        {
            get
            {
                return this._saveFileName;
            }
        }

        /// <summary>
        /// 쎔네일 이미지 경로
        /// </summary>
        public string SaveFilePath
        {
            get
            {
                return this._saveFilePath;
            }
        }

        /// <summary>
        /// 쎔네일 이미지 가로 사이즈
        /// </summary>
        public int Width
        {
            get
            {
                return this._width;
            }
        }

        /// <summary>
        /// 쎔네일 이미지 세로 사이즈
        /// </summary>
        public int Height
        {
            get
            {
                return this._height;
            }
        }

        /// <summary>
        /// 썸네일 생성 여부
        /// </summary>
        public bool Result
        {
            get
            {
                return this._result;
            }
        }

        /// <summary>
        /// 썸네일 사이즈
        /// </summary>
        public long Size
        {
            get
            {
                return this._size;
            }
        }
        #endregion

        #region [ 생성자 ]
        /// <summary>
        /// 썸네일 이미지 생성(이미지 포맷은 JPG로 생성됨)
        /// </summary>
        /// <param name="path">원본 이미지 경로</param>
        /// <param name="save">썸네일 이미지 저장될 경로</param>
        /// <param name="width">썸네일 이미지 최대 가로 사이즈</param>
        /// <param name="height">썸네일 이미지 최대 세로 사이즈</param>
        /// <param name="quality">썸네일 이미지 품질</param>
        public Thumbnail(string path, string save, int width, int height, int quality)
        {
            this._path = path;
            this._save = save;
            this._w = width;
            this._h = height;
            this._quality = quality;
        }
        #endregion

        #region [ 메쏘드 ]
        /// <summary>
        /// 썸네일 이미지 생성
        /// </summary>
        public void MakeThumbnail()
        {
            if (System.IO.File.Exists(this._save))
            {
                throw new Exception("이미 존재하는 썸네일 이미지입니다.");
            }

            FileInfo info = new FileInfo(this._path);
            if (info.Exists)
            {
                string type = File.MimeType(info.Extension);
                if (Check.IsIn(type, "image"))
                {
                    using (Bitmap image = new Bitmap(this._path))
                    {
                        int w = image.Width;
                        int h = image.Height;
                        float rx = (float)this._w / (float)w;
                        float ry = (float)this._h / (float)h;
                        float ratio = Math.Min(rx, ry);
                        int nw = (int)(w * ratio);
                        int nh = (int)(h * ratio);
                        using (Bitmap bitmap = new Bitmap(nw, nh, PixelFormat.Format24bppRgb))
                        {
                            using (Graphics graphics = Graphics.FromImage(bitmap))
                            {
                                graphics.CompositingQuality = CompositingQuality.HighQuality;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.SmoothingMode = SmoothingMode.HighQuality;
                                graphics.DrawImage(image, 0, 0, nw, nh);
                            }

                            ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);
                            Encoder encoder = Encoder.Quality;
                            EncoderParameters encoderParameters = new EncoderParameters(1);
                            EncoderParameter encoderParameter = new EncoderParameter(encoder, _quality);
                            encoderParameters.Param[0] = encoderParameter;
                            bitmap.Save(this._save, imageCodecInfo, encoderParameters);

                            this.Properties(this._save);
                        }
                    }
                }
                else
                {
                    throw new Exception("썸네일 이미지를 만들 수 없는 파일입니다. 파일 속성을 확인해주세요.");
                }
            }
            else
            {
                throw new Exception("파일이 존재하지 않습니다.");
            }
        }

        /// <summary>
        /// 속성 셋팅
        /// </summary>
        /// <param name="path">파일 경로</param>
        private void Properties(string path)
        {
            FileInfo info = new FileInfo(path);

            if (info.Exists)
            {
                using (Bitmap image = new Bitmap(path))
                {
                    this._width = image.Width;
                    this._height = image.Height;
                }
                this._result = true;
                this._size = info.Length;
                this._saveFileName = info.Name;
                this._saveFilePath = info.FullName;
            }
        }

        /// <summary>
        /// 이미지 인코딩 코덱
        /// </summary>
        /// <param name="format">이미지 포맷</param>
        /// <returns>ImageCodecInfo 코덱</returns>
        public static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
        #endregion
    }
}

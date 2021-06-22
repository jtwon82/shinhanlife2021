using MLib.Util;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MLib.Auth
{
    public class Captcha
    {
        private string _text = string.Empty;
        private string _data = string.Empty;
        private int _width = 0;
        private int _height = 0;
        private Random _ran = new Random();

        #region [ 속성 ]
        public string Data
        {
            get
            {
                return this._data;
            }
        }
        #endregion

        #region [ 함수 ]
        /// <summary>
        /// 자동입력 방지 문자열 이미지 생성
        /// </summary>
        /// <param name="text">자동입력 방지 문자열</param>
        /// <param name="size">폰트사이즈(px)</param>
        public Captcha(string text, int width, int height)
        {
            this._text = text;
            this._width = width;
            this._height = height;

            if (Check.IsNone(this._text))
            {
                throw new Exception("설정할 문자가 없습니다.");
            }
        }

        /// <summary>
        /// 캡차 이미지 생성
        /// </summary>
        public void Create()
        {
            int size = this._width / this._text.Length;
            Font font = new Font(new FontFamily("Verdana"), size, FontStyle.Italic, GraphicsUnit.Pixel);
            font = new Font(font, FontStyle.Underline);
            int reminder = (this._height - size) / 2;
            int random = 0;

            using (Bitmap bitmap = new Bitmap(this._width, this._height))
            {
                Color color = RandomColor(Color.White);
                using (Graphics grap = Graphics.FromImage(bitmap))
                {
                    grap.Clear(color);
                    for (int i = 0; i < this._text.Length; i++)
                    {
                        string txt = this._text[i].ToString();
                        using (Bitmap temp = new Bitmap(size - 2, size + reminder))
                        {
                            using (Graphics g = Graphics.FromImage(temp))
                            {
                                random = _ran.Next(-20, 20);
                                g.Clear(color);
                                g.RotateTransform(random);

                                SolidBrush brush = new SolidBrush(Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B));
                                g.DrawString(txt, font, brush, 0, 0);
                            }
                            grap.DrawImage(temp, i * size, reminder);
                        }
                    }

                    for (int i = 0; i < _ran.Next(5, 10); i++)
                    {
                        Point start = new Point(_ran.Next(0, this._width), _ran.Next(0, this._height));
                        Point end = new Point(_ran.Next(0, this._width), _ran.Next(0, this._height));

                        Pen pen = new Pen(Color.Black, 1);
                        grap.DrawLine(pen, start, end);
                    }
                }

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    this._data = "data:image/jpeg;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// 랜덤 색상
        /// </summary>
        /// <param name="fcolor"></param>
        /// <returns></returns>
        private Color RandomColor(Color fcolor)
        {
            byte[] bytes = new byte[3];
            this._ran.NextBytes(bytes);

            Color color = Color.FromArgb(bytes[0], bytes[1], bytes[2]);
            if (color.Equals(fcolor))
            {
                color = Color.FromArgb(bytes[0], bytes[1], bytes[2]);
            }

            return color;
        }
        #endregion
    }
}

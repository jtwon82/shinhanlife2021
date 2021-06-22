using System.Net.Mail;
using System.Text;

namespace MLib.Util
{
    public class Email
    {
        private string _server = string.Empty;
        private int _port;

        #region [ 속성 ]
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string Attach { get; set; }
        #endregion

        #region [ 생성자 ]
        /// <summary>
        /// 이메일 발송 생성자
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        public Email(string server, int port)
        {
            this._server = server;
            this._port = port;
        }
        #endregion

        #region [ 함수 ]
        /// <summary>
        /// 이메일 발송
        /// </summary>
        public void Send()
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(this.From);
            mail.To.Add(new MailAddress(this.To));
            mail.IsBodyHtml = true;
            mail.Subject = this.Title;
            mail.Body = this.Contents;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;

            // 첨부파일
            if (!Check.IsNone(this.Attach) && System.IO.File.Exists(this.Attach))
                mail.Attachments.Add(new Attachment(this.Attach));

            SmtpClient client = new SmtpClient(_server, _port);
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;
            client.Send(mail);
        }
        #endregion
    }
}

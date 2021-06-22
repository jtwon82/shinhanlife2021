using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using MimeKit;

namespace MLib.Util
{
    public class AWSEmail
    {
        private string _access = string.Empty;
        private string _secret = string.Empty;

        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public List<Attachment> Attachments { get; set; }


        public AWSEmail(string access, string secret)
        {
            _access = access;
            _secret = secret;
        }

        public void Send()
        {
            using (var client = new AmazonSimpleEmailServiceClient(_access, _secret, RegionEndpoint.USEast1))
            {
                List<string> address = this.To.Replace(", ", ",").Split(',').ToList();
                if (this.Attachments != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        MimeMessage message = new MimeMessage();
                        BodyBuilder builder = new BodyBuilder() { HtmlBody = this.Contents };

                        message.From.Add(new MailboxAddress(this.From));
                        foreach (string item in address)
                            message.To.Add(new MailboxAddress(item));
                        message.Subject = this.Title;

                        foreach (Attachment item in this.Attachments)
                        {
                            using (FileStream stream = File.Open(item.Path, FileMode.Open))
                                builder.Attachments.Add(item.Filename, stream);
                        }

                        message.Body = builder.ToMessageBody();
                        message.WriteTo(ms);

                        var request = new SendRawEmailRequest()
                        {
                            RawMessage = new RawMessage() { Data = ms }
                        };

                        client.SendRawEmail(request);
                    }
                }
                else
                {
                    SendEmailRequest request = new SendEmailRequest
                    {
                        Source = this.From,
                        Destination = new Destination
                        {
                            ToAddresses = address
                        },
                        Message = new Message
                        {
                            Subject = new Content(this.Title),
                            Body = new Body
                            {
                                Html = new Content
                                {
                                    Charset = "UTF-8",
                                    Data = this.Contents
                                }
                            }
                        },
                    };

                    client.SendEmail(request);
                }
            }
        }

        public class Attachment
        {
            public string Filename { get; set; }
            public string Path { get; set; }
        }
    }
}

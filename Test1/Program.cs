using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Test1
{
    class Program
    {

        static void Main(string[] args)
        {
            string message = "mail i want to read from pop";
            List<string> recievers = new List<string>();
            recievers.Add("");
            //recievers.Add("");
            //recievers.Add("");
            Email(message, recievers);
            //EmailWithAttachment(message, recievers);
        }
        public static void Email(string messageString, List<string> recievers)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                string FromMail = "";
                string FromMailPW = "";
                message.From = new MailAddress(FromMail);
                //message.To.Add(new MailAddress(""));
                foreach (var reciever in recievers)
                {
                    message.To.Add(new MailAddress(reciever));
                }
                message.Subject = "Testing";
                //message.IsBodyHtml = true; //to make message body as html  
                message.Body = messageString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(FromMail, FromMailPW);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                Console.WriteLine("email(s) sent");
            }
            catch (Exception) { }
        }
        public static void EmailWithAttachment(string messageString, List<string> recievers)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                string FromMail = "";
                string FromMailPW = "";
                message.From = new MailAddress(FromMail);
                //message.To.Add(new MailAddress(""));
                foreach (var reciever in recievers)
                {
                    message.To.Add(new MailAddress(reciever));
                }

                #region attachment
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //url til desktop
                string file = System.IO.Path.Combine(desktopPath, "CV til praktik.docx");//url+filnavn fra desktop
                Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                // Add the file attachment to this email message.
                message.Attachments.Add(data);
                #endregion

                message.Subject = "Test";
                //message.IsBodyHtml = true; //to make message body as html  
                message.Body = messageString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(FromMail, FromMailPW);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                Console.WriteLine("email(s) sent");
            }
            catch (Exception) { }
        }
    }
}

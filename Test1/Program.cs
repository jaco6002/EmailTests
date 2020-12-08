using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "Testing sending multiple Emails";
            List<string> recievers = new List<string>();
            recievers.Add("");
            //recievers.Add("");
            //recievers.Add("");
            Email(message, recievers);
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
                //message.To.Add(new MailAddress("jacob-madvig@hotmail.com"));
                foreach (var reciever in recievers)
                {
                    message.To.Add(new MailAddress(reciever));
                }
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

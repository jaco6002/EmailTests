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
            recievers.Add("jacob13madvig@gmail.com");
            recievers.Add("jacob97madvig@gmail.com");
            recievers.Add("jacob-madvig@hotmail.com");
            Email(message, recievers);
        }
        public static void Email(string htmlString,List<string> recievers)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                string FromMail = "TestingEmailProgram123@gmail.com";
                message.From = new MailAddress(FromMail);
                //message.To.Add(new MailAddress("jacob-madvig@hotmail.com"));
                foreach (var reciever in recievers)
                {
                    message.To.Add(new MailAddress(reciever));
                }
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(FromMail, "vxWaG3BUAHFbd8E2");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                Console.WriteLine("email(s) sent");
            }
            catch (Exception) { }
        }
    }
}

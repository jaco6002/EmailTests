using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading;
using Limilabs.Client.IMAP;
using Limilabs.Mail;

namespace IMAP2
{
    class Program
    {
        private static string Email = "";
        private static string PW = "";

        static void Main(string[] args)
        {
            while (true)
            {
                using (Imap imap = new Imap())
                {
                    imap.ConnectSSL("imap.gmail.com"); // or ConnectSSL for SSL
                    imap.UseBestLogin(Email, PW);

                    imap.SelectInbox();
                    List<long> uidList = imap.Search(Flag.Unseen);
                    foreach (long uid in uidList)
                    {
                        IMail email = new MailBuilder()
                            .CreateFromEml(imap.GetMessageByUID(uid));
                        SendEmail(email.Text, email.ReturnPath);
                        Console.WriteLine(email.Text);
                        Console.WriteLine(email.ReturnPath);
                        Console.WriteLine(
                            "////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                    }

                    //Console.WriteLine("done press a key");
                    //Console.ReadKey();
                    imap.Close();
                }
                Thread.Sleep(1000*10);
            }
        }
        public static void SendEmail(string messageString, string reciever)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                string FromMail = Email;
                string FromMailPW = PW;
                message.From = new MailAddress(FromMail);
                message.To.Add(new MailAddress(reciever));
                message.Subject = "reply with same body";
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
            catch (Exception)
            {
                Console.WriteLine("error");
            }
        }
    }
}

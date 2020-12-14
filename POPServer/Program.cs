using System;
using System.Collections.Generic;
using Limilabs.Client.IMAP;
using Limilabs.Client.POP3;
using Limilabs.Mail;

namespace POPServer
{
    class Program
    {
        private static string Email = "";
        private static string PW = "";

        static void Main(string[] args)
        {
            using (Pop3 pop3 = new Pop3())
            {
                pop3.ConnectSSL("pop.gmail.com");  // or ConnectSSL for SSL      
                pop3.UseBestLogin(Email, PW);
                List<string> uids = pop3.GetAll();
                foreach (string uid in uids)
                {
                    IMail email = new MailBuilder()
                        .CreateFromEml(pop3.GetMessageByUID(uid));
                    Console.WriteLine(email.Subject);
                }

                Console.ReadKey();
                pop3.Close();
            }

            
        }
    }
}

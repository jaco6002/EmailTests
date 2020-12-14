using Limilabs.Client.IMAP;
using System;
using System.Collections.Generic;
using Limilabs.Mail;

namespace IMAPServer
{
    class Program
    {
        private static string Email = "";
        private static string PW = "";
        static void Main(string[] args)
        {
            using (Imap imap = new Imap())
            {
                imap.ConnectSSL("imap.gmail.com");       // or ConnectSSL for SSL
                imap.UseBestLogin(Email, PW);

                imap.SelectInbox();
                List<long> uidList = imap.Search(Flag.All);
                foreach (long uid in uidList)
                {
                    IMail email = new MailBuilder()
                        .CreateFromEml(imap.GetMessageByUID(uid));

                    Console.WriteLine(email.Subject);
                    Console.WriteLine(email.Text);
                    Console.WriteLine("////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                }

                Console.ReadKey();
                imap.Close();
            }
        }
    }
}

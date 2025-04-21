using Ikea.DAL.Entities.Identity;
using System.Net;
using System.Net.Mail;

namespace Ikea.BLL.Common.EmailSettings
{
    public static class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("mo5195278@gmail.com", "egueraeavwntyoap");
            Client.Send("mo5195278@gmail.com", email.To, email.Subject, email.Body);

        }

    }
}

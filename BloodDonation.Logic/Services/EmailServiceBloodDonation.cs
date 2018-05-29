using System;
using System.Net.Mail;

namespace BloodDonation.Logic.Services
{
    public class EmailServiceBloodDonation
    {

        public void SendEmail(String text, String title, String doctorName, String destinationEmail)
        {

            MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new MailAddress("blooddonationiss@gmail.com", "Blood Donation App");
            message.To.Add(new MailAddress(destinationEmail));
            message.Subject = title;
            message.Priority = MailPriority.High;
            message.IsBodyHtml = false;
            message.Sender = new MailAddress("blooddonationiss@gmail.com", doctorName);
            message.Body = text;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential("blooddonationiss@gmail.com", "bdisspass8");

            try
            {
                smtp.Send(message);
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }

    }
}

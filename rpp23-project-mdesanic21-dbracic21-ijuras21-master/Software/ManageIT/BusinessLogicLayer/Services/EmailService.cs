using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    ///<remarks>Darijo Bračić </remarks>
    public class EmailService
    {
        ///<remarks>Darijo Bračić </remarks>
        public async Task  SendEmail(string to, string subject, string body, bool IsBodyHTML)
        {
            try
            {
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "dbracic21@student.foi.hr";
                string smtpPassword = "Telka123!";

                using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("dbracic21@student.foi.hr");
                    mailMessage.To.Add(to);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    client.SendCompleted += (sender, e) => {
                        if (e.Cancelled)
                        {
                            Console.WriteLine("Slanje e-maila otkazano.");
                        }
                        if (e.Error != null)
                        {
                            Console.WriteLine($"Greška prilikom slanja e-maila: {e.Error.Message}");
                        }
                        else
                        {
                            Console.WriteLine("E-mail je uspješno poslan.");
                        }

                    };

                   await client.SendMailAsync(mailMessage);
                }
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"Greška prilikom slanja e-maila: {ex.Message}");
            }
        }


    }
}


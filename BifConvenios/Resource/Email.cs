using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace Resource
{
    public class Email
    {
        public static bool SendEmail(clsMail oclsMail)
        {
            try
            {
                return SendEmail(
                                         oclsMail.vSMTPServer,
                                           oclsMail.iSMTPPort,
                                           oclsMail.vSMTPUserName,
                                           oclsMail.vSMTPPassword,
                                           oclsMail.vDefaultCredentials,
                                           oclsMail.vEmailFrom,
                                           oclsMail.vEmailTo,
                                           oclsMail.vEmailCC,
                                           oclsMail.vEmailCCo,
                                           oclsMail.vEmailSubject,
                                           oclsMail.vEmailBody,
                                           oclsMail.bEnabledSSL,
                                           oclsMail.bIsBodyHtml,
                                           oclsMail.oAttachments);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool SendEmail(
                                      string pstrSMTPServer
                                    , int pintSMTPPort
                                    , string pstrSMTPUserName
                                    , string pstrSMTPPassword
                                    , bool pbUseDefaultCredentials
                                    , string pstrEmailFrom
                                    , List<string> pstrEmailTo
                                    , List<string> pstrEmailCC
                                    , List<string> pstrEmailCCo
                                    , string pstrEmailSubject
                                    , string pstrEmailBody
                                    , bool pbEnabledSSL
                                    , bool pbIsBodyHtml
                                    , List<string> plstAttachments
                                    )
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(pstrEmailFrom);

                foreach (string item in pstrEmailTo)
                {
                    if (item != "")
                        mail.To.Add(item);
                }

                foreach (string item in pstrEmailCC)
                {
                    if (item != "")
                        mail.CC.Add(item);
                }

                foreach (string item in pstrEmailCCo)
                {
                    if (item != "")
                        mail.Bcc.Add(item);
                }

                if (plstAttachments != null)
                {
                    foreach (string item in plstAttachments)
                    {
                        mail.Attachments.Add(new Attachment(item));
                    }
                }

                mail.Subject = pstrEmailSubject;
                mail.Body = pstrEmailBody;
                mail.IsBodyHtml = pbIsBodyHtml;
                mail.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();

                smtp.Host = pstrSMTPServer;
                smtp.UseDefaultCredentials = pbUseDefaultCredentials;

                smtp.EnableSsl = pbEnabledSSL;
                smtp.Port = pintSMTPPort;
                smtp.Credentials = new System.Net.NetworkCredential(pstrSMTPUserName, pstrSMTPPassword);
                smtp.Send(mail);
                mail.Dispose();

                return true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

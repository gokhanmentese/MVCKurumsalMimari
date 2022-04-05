using Business.BusinessAspects.Autofac;
using Core.AppSettings;
using Core.Aspects.Autofac.Caching;
using Core.Enums;
using DataAccess.Abstract;
using Entities.Concrete;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Business.Concrete
{
    public class EmailManager : IEmailService
    {
        private readonly IEmailDal _emailDal;
        private readonly IEmailServerDal _emailServerDal;

        public EmailManager(IEmailDal emailDal, IEmailServerDal emailServerDal)
        {
            _emailDal = emailDal;
            _emailServerDal = emailServerDal;
        }

        public Email Add(Email user)
        {
            return _emailDal.Add(user);
        }

        [SecuredOperation("Admin,User")]
        public void Delete(Email user)
        {
            _emailDal.Delete(user);
        }

        public List<Email> GetAll()
        {
            return _emailDal.GetList().ToList();
        }

        public List<Email> GetErrorEmails()
        {
            return _emailDal.GetList(t=>t.Status !=null && t.Status.Value == (int)Enumarations.EmailStatus.Error).ToList();
        }

        public Email GetById(Guid id)
        {
            return _emailDal.Get(i => i.Id == id);
        }

        public void Update(Email user)
        {
            _emailDal.Update(user);
        }

        public void SendMailForNotSended()
        {
            var errorEmails = GetErrorEmails();
            if (errorEmails !=null && errorEmails.Count !=0)
            {
                foreach (var item in errorEmails)
                {
                    SendEmail(item);
                }
            }
        }

        public bool SendEmail(Email sendemail)
        {
            bool flag = false;
            try
            {
                var emailServer = _emailServerDal.GetList().FirstOrDefault();

                sendemail.From = emailServer.SenderEmail;
                sendemail.Status = (int)Enumarations.EmailStatus.Sending;

                Email email = null;

                if (sendemail.Id != Guid.Empty)
                    email = Add(sendemail);
                else
                    email = sendemail;


                MailMessage mailMessage = new MailMessage(emailServer.SenderEmail, email.To)
                {
                    From = new MailAddress(emailServer.SenderEmail, emailServer.SenderDisplayName),
                    Subject = email.Subject,
                    Body = email.Body,
                    BodyEncoding = Encoding.GetEncoding("ISO-8859-9"),
                    IsBodyHtml = email.IsHtml,
                    Priority = MailPriority.High
                };

                if (!string.IsNullOrEmpty(email.Cc))
                    mailMessage.CC.Add(email.Cc);

                if (!string.IsNullOrEmpty(email.Bcc))
                    mailMessage.Bcc.Add(email.Bcc);


                if (email.FileStream != null)
                {
                    for (int i = 0; i < (int)email.FileStream.Length; i++)
                    {
                        ContentType contentType = new ContentType(email.FileType);
                        //mailMessage.Attachments.Add(new Attachment(mail.FileStream, contentType));
                    }
                }

                try
                {
                    SmtpClient smtpClient = new SmtpClient
                    {
                        Host = emailServer.Host,
                        //UseDefaultCredentials = false,
                        Port = emailServer.Port != null ? emailServer.Port.Value : 537,
                        EnableSsl = emailServer.EnableSsl != null ? emailServer.EnableSsl.Value : false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new System.Net.NetworkCredential(emailServer.SenderEmail, emailServer.SenderEmailPassword),
                        Timeout = 2 * 60 * 1000,
                    };

                    smtpClient.Send(mailMessage);
                    smtpClient.Dispose();
                    flag = true;

                    email.Status = (int)Enumarations.EmailStatus.Sended;
                    _emailDal.Update(email);
                }
                catch (Exception ex)
                {
                    email.Status = (int)Enumarations.EmailStatus.Error;
                    email.StatusReason = ex.Message;
                    _emailDal.Update(email);

                    throw ex;
                }
            }
            catch
            {
                flag = false;
            }
            finally
            {
            }
            return flag;
        }

        public List<Email> GetByUserId(Guid userid)
        {
            return _emailDal.GetList(x => x.SenderId == userid).ToList();
        }

        public List<Email> GetByEmail(string email)
        {
            return _emailDal.GetList(x => x.To == email || x.Cc == email || x.Bcc == email).ToList();
        }
    }
}

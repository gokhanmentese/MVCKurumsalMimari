using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IEmailService
    {
        List<Email> GetAll();

        List<Email> GetByUserId(Guid userid);

        List<Email> GetByEmail(string email);

        Email GetById(Guid id);

        Email Add(Email email);

        void Update(Email email);

        void Delete(Email email);

        bool SendEmail(Email email);

        void SendMailForNotSended();

        List<Email> GetErrorEmails();
    }
}

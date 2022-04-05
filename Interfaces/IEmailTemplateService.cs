using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IEmailTemplateService
    {
        List<EmailTemplate> GetAll();

        EmailTemplate GetById(int id);

        string GetTemplateContent(EmailTemplate emailTemplate);
    }
}

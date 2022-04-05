using DataAccess.Abstract;
using Entities.Concrete;
using Interfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class EmailTemplateManager : IEmailTemplateService
    {
        private readonly IEmailTemplateDal _emailTemplateDal;
      
        public EmailTemplateManager(IEmailTemplateDal emailTemplateDal)
        {
            _emailTemplateDal = emailTemplateDal;
        }

        public List<EmailTemplate> GetAll()
        {
            return _emailTemplateDal.GetList().ToList();
        }

        public EmailTemplate GetById(int id)
        {
            return _emailTemplateDal.Get(e => e.Id == id);
        }

        public string GetTemplateContent(EmailTemplate emailTemplate)
        {
            if (!string.IsNullOrEmpty(emailTemplate.Path))
            {
                string newPath = Path.Combine(emailTemplate.ContentRootPath, emailTemplate.Path);
                //string defaultPath = Path.Combine(emailTemplate.ContentRootPath, emailTemplate.DefaultPath);

                FileInfo file = new FileInfo(newPath);
                string end = string.Empty;

                if (file.Exists)
                {
                    using (StreamReader stream = (new FileInfo(newPath)).OpenText())
                    {
                        end = stream.ReadToEnd();
                    }
                }
                else
                {
                    //file = new FileInfo(defaultPath);
                    //using (StreamReader stream = (new FileInfo(defaultPath)).OpenText())
                    //{
                    //    end = stream.ReadToEnd();
                    //}
                }
                return end;
            }
            else
                return string.Empty;
        }
    }
}

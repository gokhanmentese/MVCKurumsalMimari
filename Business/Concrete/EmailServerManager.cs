using DataAccess.Abstract;
using Entities.Concrete;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class EmailServerManager : IEMaiIServerService
    {
        private readonly IEmailServerDal _emailServerDal;

        public EmailServerManager(IEmailServerDal emailServerDal)
        {
            _emailServerDal = emailServerDal;
        }

        public List<EmailServerInfo> GetAll()
        {
            return _emailServerDal.GetList().ToList();
        }

        public EmailServerInfo GetById(int id)
        {
            return _emailServerDal.Get(x=>x.Id==id);
        }
    }
}

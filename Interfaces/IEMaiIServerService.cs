using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IEMaiIServerService
    {
        List<EmailServerInfo> GetAll();

        EmailServerInfo GetById(int id);


    }
}

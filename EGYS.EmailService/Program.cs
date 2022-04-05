using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;
using System.ServiceProcess;

namespace EGYS.EmailService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new EmailServ(new EmailManager(new EfEmailDal(),new EfEmailServerDal()))
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}

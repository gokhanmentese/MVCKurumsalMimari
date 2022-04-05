using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleDal _roleDal;

        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }

        public Role Add(Role role)
        {
            return _roleDal.Add(role);
        }

        public void Delete(Role role)
        {
             _roleDal.Delete(role);
        }

        public List<Role> GetAll()
        {
            return _roleDal.GetList().ToList();
        }

        public Role GetById(Guid id)
        {
            return _roleDal.Get(x=>x.Id==id);
        }

        public Role GetByName(string name)
        {
            return _roleDal.Get(x => x.Name.ToLower() == name.ToLower());
        }

        public void Update(Role role)
        {
            _roleDal.Update(role);
        }
    }
}

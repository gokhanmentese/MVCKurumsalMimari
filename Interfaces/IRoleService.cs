using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
   public  interface IRoleService
    {
        List<Role> GetAll();

        Role GetById(Guid id);

        Role GetByName(string name);

        Role Add(Role role);

        void Update(Role role);

        void Delete(Role role);
    }
}

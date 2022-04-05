using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUserRoleService
    {
        List<UserRole> GetAll();

        UserRole GetById(Guid id);

        List<UserRole> GetByUserId(Guid userid);

        UserRole Add(UserRole user);

        void Update(UserRole user);

        void Delete(UserRole user);
    }
}

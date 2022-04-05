using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();

        User GetById(Guid id);

        User GetByEmail(string email);

        User GetByIdentityNumber(string identityNumber);

        User Add(User user);

        void Update(User user);

        void Delete(User user);

        List<Role> GetRoles(User user);



    }
}

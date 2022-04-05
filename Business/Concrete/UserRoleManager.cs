using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserRoleManager : IUserRoleService
    {
        private readonly IUserRoleDal _userRoleDal;

        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }

        public UserRole Add(UserRole user)
        {
            return _userRoleDal.Add(user);
        }

        public void Delete(UserRole user)
        {
            _userRoleDal.Delete(user);
        }

        public List<UserRole> GetAll()
        {
            return _userRoleDal.GetList().ToList();
        }

        public UserRole GetById(Guid id)
        {
            return _userRoleDal.Get(r=>r.RoleId==id);
        }

        public List<UserRole> GetByUserId(Guid userid)
        {
            return _userRoleDal.GetList(r => r.UserId == userid).ToList();
        }

        public void Update(UserRole user)
        {
            _userRoleDal.Update(user);
        }
    }
}

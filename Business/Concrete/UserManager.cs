using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<User> GetAll()
        {
            return _userDal.GetList().ToList();
        }

        public User GetById(Guid id)
        {
            return _userDal.Get(b => b.UserId == id);
        }

        public User GetByIdentityNumber(string identityNumber)
        {
            return _userDal.Get(u=>u.IdentityNumber==identityNumber);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(u => u.IdentityNumber == email);
        }

        public List<Role> GetRoles(User user)
        {
            return _userDal.GetRoles(user);
        }

        public UserProfile GetUserProfile(User user)
        {
            return _userDal.GetUserProfile(user);
        }

        public User Add(User user)
        {
            if (_userDal.Get(b => b.IdentityNumber == user.IdentityNumber) == null)
            {
               return _userDal.Add(user);
            }
            else
            {
                throw new Exception("Bu kullanıcı daha önceden sisteme eklenmiştir.");
            }
        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }

      
    }
}

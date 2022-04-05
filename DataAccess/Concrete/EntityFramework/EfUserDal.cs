using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, EGYSContext>, IUserDal
    {
        public List<Role> GetRoles(User user)
        {
            using (var context = new EGYSContext())
            {
                var result = from operationClaim in context.Roles
                             join userOperationClaim in context.UserRoles
                                 on operationClaim.Id equals userOperationClaim.RoleId
                             where userOperationClaim.UserId == user.UserId
                             select new Role { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public UserProfile GetUserProfile(User user)
        {
            using (var context = new EGYSContext())
            {
                var result = from userprofile in context.UserProfiles
                             join users in context.Users on userprofile.UserId equals users.UserId
                             where users.UserId == user.UserId
                             select new UserProfile {
                                 Id = userprofile.Id, 
                                 FirstName = userprofile.FirstName,
                                 LastName=userprofile.LastName,
                                 Email=userprofile.Email
                             };
                return result.ToList().FirstOrDefault();

            }
        }

        
    }
}

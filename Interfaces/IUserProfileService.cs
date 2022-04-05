using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUserProfileService
    {
        List<UserProfile> GetAll();

        List<UserProfile> GetByUnitId(int unitid);

        List<UserProfile> GetUsersForAssignedTask(Guid taskid);

        UserProfile GetById(Guid id);

        UserProfile GetByUserId(Guid userid);

        UserProfile Add(UserProfile user);

        void Update(UserProfile user);

        void Delete(UserProfile user);

        bool CheckUser(UserProfile user);
    }
}

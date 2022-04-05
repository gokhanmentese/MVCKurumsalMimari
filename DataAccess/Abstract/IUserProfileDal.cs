using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IUserProfileDal : IEntityRepository<UserProfile>
    {
        List<UserProfile> GetUsersForAssignedTask(Guid taskid);
    }
}

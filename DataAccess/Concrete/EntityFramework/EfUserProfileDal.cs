using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserProfileDal : EfEntityRepositoryBase<UserProfile, EGYSContext>, IUserProfileDal
    {
        public List<UserProfile> GetUsersForAssignedTask(Guid taskid)
        {
            using (var context = new EGYSContext())
            {
                var result = from userprofile in context.UserProfiles
                             join assigntask in context.AssignTasks
                                 on userprofile.UserId equals assigntask.AssignedUserId
                             where assigntask.TaskId == taskid
                             select new UserProfile
                             {
                                 Id = userprofile.Id,
                                 UserId = userprofile.UserId,
                                 FirstName = userprofile.FirstName,
                                 LastName = userprofile.LastName,
                                 Email = userprofile.Email,
                                 PhoneNumber = userprofile.PhoneNumber,
                                 DirectorshipId = userprofile.DirectorshipId,
                                 DepartmentId = userprofile.DepartmentId,
                                 UnitId = userprofile.UnitId
                             };
                return result.ToList();

            }
        }
    }

}

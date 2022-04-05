using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAssignTaskDal : EfEntityRepositoryBase<AssignTask, EGYSContext>, IAssignTaskDal
    {
        public List<AssignTaskDTO> GetUsersForAssignedTask(Guid taskid)
        {
            using (var context = new EGYSContext())
            {
                var result = from userprofile in context.UserProfiles
                             join assigntask in context.AssignTasks
                                 on userprofile.UserId equals assigntask.AssignedUserId
                             where assigntask.TaskId == taskid
                             select new AssignTaskDTO
                             {
                                 Id = assigntask.Id,
                                 AssignedUserId = userprofile.UserId,
                                 FirstName = userprofile.FirstName,
                                 LastName = userprofile.LastName,
                                 StartDate = assigntask.StartDate,
                                 EndDate = assigntask.EndDate,
                                 AssignUserId = assigntask.AssignUserId,
                                 StatusCode = assigntask.StatusCode,
                                 AssignDate=assigntask.AssignDate,
                                 TaskId=taskid
                             };
                return result.ToList();

            }
        }
    }
}

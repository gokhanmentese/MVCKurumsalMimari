using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
   public  interface IAssignTaskService
    {
        AssignTask GetById(Guid id);

        AssignTask Add(AssignTask task);

        List<AssignTaskDTO> GetUsersForAssignedTask(Guid taskid);

        AssignTask GetByTaskIdAndUserId(Guid taskid, Guid userid,DateTime date);

        void Update(AssignTask task);

        void Delete(AssignTask task);
    }
}

using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IAssignTaskDal : IEntityRepository<AssignTask>
    {
        List<AssignTaskDTO> GetUsersForAssignedTask(Guid taskid);
    }
}

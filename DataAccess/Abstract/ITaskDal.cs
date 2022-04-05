using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ITaskDal : IEntityRepository<Task>
    {
        List<Task> GetUserOpenTasks(Guid userid);

        List<Task> GetUserClosedTasks(Guid userid);
    }
}

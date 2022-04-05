using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Interfaces
{
   public  interface ITaskService
    {
        List<Task> GetAll();

        List<Task> GetWaitingTasks();

        List<Task> GetWaitingTasksByUserId(Guid userid);

        List<Task> GetClosedTasks();

        List<Task> GetClosedTasksByUserId(Guid userid);

        Task GetById(Guid id);

        Task Add(Task task);

        void Update(Task task);

        void Delete(Task task);

        string TransactionalOperation(Task task);
    }
}

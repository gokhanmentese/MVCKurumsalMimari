using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Enums;
using DataAccess.Concrete.EntityFramework.Comparer;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTaskDal : EfEntityRepositoryBase<Task, EGYSContext>, ITaskDal
    {
        public List<Task> GetUserOpenTasks(Guid userid)
        {
            using (var context = new EGYSContext())
            {
                var result = (from task in context.Tasks
                              join assginTask in context.AssignTasks
                                  on task.Id equals assginTask.TaskId into gj
                              from assginTask in gj.DefaultIfEmpty()
                              where
                              (assginTask.StartDate <= DateTime.Now && assginTask.EndDate >= DateTime.Now &&
                              assginTask.AssignedUserId == userid && assginTask.StatusCode == (int)Enumarations.AssignTaskStatus.Assigned &&
                              task.StatusCode == (int)Enumarations.TaskStatus.Assigned)
                              ||
                              (task.OwnerId == userid && task.StartDate <= DateTime.Now && task.EndDate >= DateTime.Now && 
                              (task.StatusCode == (int)Enumarations.TaskStatus.Assigned || task.StatusCode == (int)Enumarations.TaskStatus.Open))
                              select task)
                               //.Select(std => new Entities.Concrete.Task { Id = std.Id, Subject=std.Subject , st })
                               .Distinct().ToList();

                return result.ToList();
            }
        }

        public List<Task> GetUserClosedTasks(Guid userid)
        {
            TaskComparer taskComparer = new TaskComparer();

            using (var context = new EGYSContext())
            {
                var result = (from task in context.Tasks
                             join assginTask in context.AssignTasks
                                 on task.Id equals assginTask.TaskId
                             where assginTask.StatusCode == (int)Enumarations.AssignTaskStatus.Closed &&
                                  (assginTask.AssignedUserId == userid || task.OwnerId==userid) 
                             select task)
                              //.Select(std => new  Entities.Concrete.Task{ Id=std.Id })
                              .Distinct().ToList();

                return result.ToList();
            }
        }
    }
}

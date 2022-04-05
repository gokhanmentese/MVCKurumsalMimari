using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AssignTaskManager : IAssignTaskService
    {
        private readonly IAssignTaskDal _assignTaskDal;

        public AssignTaskManager(IAssignTaskDal assignTaskDal)
        {
            _assignTaskDal = assignTaskDal;
        }

        [ValidationAspect(typeof(AssignTaskValidator), Priority = 1)]
        public AssignTask Add(AssignTask task)
        {
            return _assignTaskDal.Add(task);
        }

        public void Delete(AssignTask task)
        {
            _assignTaskDal.Delete(task);
        }

        public AssignTask GetById(Guid id)
        {
            return _assignTaskDal.Get(a=>a.Id==id);
        }

        public AssignTask GetByTaskIdAndUserId(Guid taskid, Guid userid,DateTime date)
        {
            return _assignTaskDal.Get(a=>a.TaskId==taskid && a.AssignedUserId==userid && a.StartDate<date && a.EndDate> date);
        }

        public List<AssignTaskDTO> GetUsersForAssignedTask(Guid taskid)
        {
            return _assignTaskDal.GetUsersForAssignedTask(taskid);
        }

        public void Update(AssignTask task)
        {
             _assignTaskDal.Update(task);
        }
    }
}

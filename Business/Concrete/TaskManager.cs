using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.EGYSLogger.Loggers;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    [SecuredOperation("Admin,User")]
    public class TaskManager : ITaskService
    {
        private readonly ITaskDal _taskDal;
        //private readonly ICategoryService _categoryService;
        public TaskManager(ITaskDal taskDal)
        {
            _taskDal = taskDal;
            //_categoryService = categoryService;
        }

        [ValidationAspect(typeof(TaskValidator),Priority =10)]
        [CacheRemoveAspect("ITaskService.Get")]
        [LogAspect(typeof(FileLogger))]
        public Task Add(Task task)
        {
            //ValidationTool.Validate(new TaskValidator(), task); /*Aspect yerıne bu satırda kullanılabılır*/

            IResult result = BusinessRules.Run(CheckIfProductNameExists(task.Subject),CheckIfCategoryIsEnabled()); /*Clean code yazılımı*/
            if (result !=null)
            {
               // return result;
            }

            return _taskDal.Add(task);
        }

        private IResult CheckIfProductNameExists(string subject)
        {
            if (_taskDal.Get(t => t.Subject == subject) != null)
            {
                return new ErrorResult(Messages.TaskTitleAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryIsEnabled()
        {
            //var result = _categoryService.GetAll();
            //if (result.Count <10)
            //{
            //    return new ErrorResult("");
            //}

            return new SuccessResult();
        }

        [LogAspect(typeof(FileLogger))]
        public void Delete(Task task)
        {
            _taskDal.Delete(task);
        }

        //[PerformanceAspect(5)]
       
       // [CacheAspect(duration:10)]
       
      //  [ExceptionLogAspect(typeof(DatabaseLogger))] /*Bunu tum opersyonlara koymamız lazım. Merkezı noktada yapılacak*/
        public List<Task> GetAll()
        {
            //Thread.Sleep(5000);
            return _taskDal.GetList().ToList();
        }

        [LogAspect(typeof(FileLogger))]
        public Task GetById(Guid id)
        {
            return _taskDal.Get(b => b.Id == id);
        }

        //[SecuredOperation("Task List")]
        //[CacheAspect(duration: 10)]
        public List<Task> GetClosedTasks()
        {
            return _taskDal.GetList(t=>t.StatusCode==(int)Enumarations.TaskStatus.Closed).ToList();
        }

        [PerformanceAspect(5)]
        //[CacheAspect(duration: 10)]
        public List<Task> GetWaitingTasks()
        {
            return _taskDal.GetList(t => t.StatusCode == (int)Enumarations.TaskStatus.Open).ToList();
        }

        [TransactionScopeAspect]
        public string TransactionalOperation(Task task)
        {
            _taskDal.Update(task);
            _taskDal.Add(task);

            return Messages.TaskUpdated;
        }

        [LogAspect(typeof(FileLogger))]
        public void Update(Task task)
        {
            _taskDal.Update(task);
        }

        public List<Task> GetWaitingTasksByUserId(Guid userid)
        {
            return _taskDal.GetUserOpenTasks(userid);
            //return _taskDal.GetList(t => (t.StatusCode == (int)Enumarations.TaskStatus.Open || t.StatusCode == (int)Enumarations.TaskStatus.Assigned) && (t.OwnerId==userid || t.AssignUserId==userid)).ToList();
        }

        public List<Task> GetClosedTasksByUserId(Guid userid)
        {
            return _taskDal.GetUserClosedTasks(userid);
            //return _taskDal.GetList(t => t.StatusCode == (int)Enumarations.TaskStatus.Closed && (t.OwnerId == userid || t.AssignUserId == userid)).ToList();
        }
    }
}

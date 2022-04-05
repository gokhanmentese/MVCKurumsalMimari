using Core.Aspects.Autofac.Transaction;
using Core.Enums;
using DataAccess.Abstract;
using Entities.Concrete;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class TaskFileManager : ITaskFileService
    {
        private readonly ITaskFileDal _taskFileDal;
        private readonly IFileService _fileService;

        public TaskFileManager(ITaskFileDal taskFileDal, IFileService fileService)
        {
            _taskFileDal = taskFileDal;
            _fileService = fileService;
        }

        public TaskFile Add(TaskFile taskFile)
        {
            return _taskFileDal.Add(taskFile);
        }

       // [TransactionScopeAspect]
        public void Delete(TaskFile taskFile)
        {
            _taskFileDal.Delete(taskFile);

            if (taskFile.FileId != Guid.Empty)
            {
                var file = _fileService.GetById(taskFile.FileId);
                if (file != null)
                {
                    if (System.IO.File.Exists(file.FilePath))
                        System.IO.File.Delete(file.FilePath);

                    _fileService.Delete(file);
                }
            }
        }

        public List<TaskFile> GetAll()
        {
            return _taskFileDal.GetList().ToList();
        }

        public TaskFile GetById(Guid id)
        {
            return _taskFileDal.Get(t=>t.Id==id);
        }

        public List<TaskFile> GetByTaskId(Guid taskid)
        {
            return _taskFileDal.GetList(t=>t.TaskId==taskid).ToList();
        }

        public List<TaskFile> GetOwnerTaskFilesByTaskId(Guid taskid)
        {
            return _taskFileDal.GetList(t => t.TaskId == taskid && t.OwnerShip==(int)Enumarations.FileOwnerShip.Owner).ToList();

        }

        public List<TaskFile> GetUserTaskFilesByTaskId(Guid taskid)
        {
            return _taskFileDal.GetList(t => t.TaskId == taskid && t.OwnerShip == (int)Enumarations.FileOwnerShip.User).ToList();
        }

        public void Update(TaskFile taskFile)
        {
            _taskFileDal.Update(taskFile);
        }
    }
}

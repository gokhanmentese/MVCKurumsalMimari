using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ITaskFileService
    {
        List<TaskFile> GetAll();

        List<TaskFile> GetByTaskId(Guid taskid);

        List<TaskFile> GetOwnerTaskFilesByTaskId(Guid taskid);

        List<TaskFile> GetUserTaskFilesByTaskId(Guid taskid);

        TaskFile GetById(Guid id);

        TaskFile Add(TaskFile taskFile);

        void Update(TaskFile taskFile);

        void Delete(TaskFile taskFile);
    }
}

using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IFileService
    {
        List<File> GetAll();

        File GetById(Guid id);

        File Add(File file);

        void Update(File file);

        void Delete(File file);
    }
}

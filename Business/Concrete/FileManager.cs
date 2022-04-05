using DataAccess.Abstract;
using Entities.Concrete;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class FileManager : IFileService
    {
        private readonly IFileDal _fileDal;

        public FileManager(IFileDal fileDal)
        {
            _fileDal = fileDal;
        }

        public File Add(File file)
        {
            return _fileDal.Add(file);
        }

        public void Delete(File file)
        {
            _fileDal.Delete(file);
        }

        public List<File> GetAll()
        {
            return _fileDal.GetList().ToList();
        }

        public File GetById(Guid id)
        {
            return _fileDal.Get(f=>f.Id==id);
        }

        public void Update(File file)
        {
            _fileDal.Update(file);
        }
    }
}

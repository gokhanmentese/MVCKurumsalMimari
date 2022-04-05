using Core.Aspects.Autofac.Caching;
using DataAccess.Abstract;
using Entities.Concrete;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentDal _departmentDal;

        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        [CacheAspect(duration: 10)]
        public List<Department> GetAll()
        {
            return _departmentDal.GetList().ToList();
        }

        [CacheAspect(duration: 10)]
        public List<Department> GetByDirectorhipId(int directorshipid)
        {
            return _departmentDal.GetList(i => i.DirectorshipId == directorshipid).ToList();
        }

        [CacheAspect(duration: 10)]
        public Department GetById(int id)
        {
            return _departmentDal.Get(i => i.Id == id);

        }

        public Department GetByName(string name)
        {
            return _departmentDal.Get(i => i.Name.ToLower() == name.ToLower());
        }
    }
}

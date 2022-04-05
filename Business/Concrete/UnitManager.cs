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
    public class UnitManager : IUnitService
    {
        private readonly IUnitDal _unitDal;

        public UnitManager(IUnitDal unitDal)
        {
            _unitDal = unitDal;
        }

        [CacheAspect(duration: 10)]
        public List<Unit> GetAll()
        {
            return _unitDal.GetList().ToList();
        }

        [CacheAspect(duration: 10)]
        public List<Unit> GetByDepartmentId(int departmentid)
        {
            return _unitDal.GetList(u => u.DepartmentId == departmentid).ToList();
        }

        [CacheAspect(duration: 10)]
        public Unit GetById(int id)
        {
            return _unitDal.Get(u => u.Id == id);
        }

        [CacheAspect(duration: 10)]
        public Unit GetByName(string name)
        {
            return _unitDal.Get(u => u.Name.ToLower() == name.ToLower());
        }
    }
}

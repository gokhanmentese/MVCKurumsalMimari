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
    public class DirectorshipManager : IDirectorshipService
    {
        private readonly IDirectorshipDal _directorshipDal;

        public DirectorshipManager(IDirectorshipDal directorshipDal)
        {
            _directorshipDal = directorshipDal;
        }

        [CacheAspect(duration: 10)]
        public List<Directorship> GetAll()
        {
            return _directorshipDal.GetList().ToList();
        }

        [CacheAspect(duration: 10)]
        public Directorship GetById(int id)
        {
            return _directorshipDal.Get(i => i.Id == id);
        }

        public Directorship GetByName(string name)
        {
            return _directorshipDal.Get(i => i.Name.ToLower() == name.ToLower());
        }
    }
}

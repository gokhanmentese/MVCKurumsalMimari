using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUnitService
    {
        List<Unit> GetAll();

        List<Unit> GetByDepartmentId(int departmentid);

        Unit GetById(int id);

        Unit GetByName(string name);
    }
}

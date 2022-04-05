using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IDepartmentService
    {
        List<Department> GetAll();

        List<Department> GetByDirectorhipId(int directorshipid);

        Department GetById(int id);

        Department GetByName(string name);
    }
}

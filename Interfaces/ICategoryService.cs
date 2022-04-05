using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll();

        Category GetById(int id);
    }
}

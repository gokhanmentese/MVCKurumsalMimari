using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IDirectorshipService
    {
        List<Directorship> GetAll();

        Directorship GetById(int id);

        Directorship GetByName(string name);
    }
}

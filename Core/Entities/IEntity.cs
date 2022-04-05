using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public interface IEntity
    {
    }

    public interface ICrmEntity
    {
        Guid Id { get; set; }

       // string LogicalName { get; set; }
    }
}

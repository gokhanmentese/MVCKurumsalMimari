using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Level : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Baskanlik { get; set; }

        public string Bolum { get; set; }

        public string Birim { get; set; }

        public string Unvan { get; set; }
    }
}

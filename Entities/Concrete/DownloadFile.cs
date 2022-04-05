using Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Entities.Concrete
{
    public class DownloadFile : IEntity
    {
        public MemoryStream MemoryStream { get; set; }

        public string  ContentType { get; set; }

        public string  DownloadName { get; set; }
    }
}

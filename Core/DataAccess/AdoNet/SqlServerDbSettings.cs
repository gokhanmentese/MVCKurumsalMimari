using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.AdoNet
{
    public interface ISqlServerDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
    public class SqlServerDbSettings : ISqlServerDbSettings
    {
        public string ConnectionString { get; set; }
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}

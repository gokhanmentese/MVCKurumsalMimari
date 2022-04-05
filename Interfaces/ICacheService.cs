using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ICacheService
    {
        void CacheCreate();

        void CacheRemoved();

        object GetCacheVal(string key);
    }
}

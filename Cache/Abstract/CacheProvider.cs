using System;

namespace EGYS.Cache.Abstract
{
    public abstract class CacheProvider
    {
        public static int CacheDuration = 60;

        public static CacheProvider Instance;

        public abstract void Set(string key,object value);

        public abstract object Get(string key);

        public abstract void Remove(string key);

        public abstract bool IsExist(string key);
    }
}

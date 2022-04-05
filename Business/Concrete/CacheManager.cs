using Core.Enums;
using Interfaces;
using System;
using System.Diagnostics;

namespace Business.Concrete
{

    public class CacheManager : ICacheService
    {

        private readonly CacheProvider _cacheProvider;
        private readonly ICategoryService _categoryService;

        public CacheManager(DefaultCacheProvider cacheProvider, ICategoryService categoryService)
        {
            _cacheProvider = cacheProvider;
            _categoryService = categoryService;
        }

        public void CacheRemoved()
        {
            _cacheProvider.Remove(Enumarations.CacheKey.Kategoriler.ToString());

        }

        public void CacheCreate()
        {
            #region Category
            object categoryCache = null;

            try
            {
                var categories = _categoryService.GetAll();

                if (categories != null)
                    categoryCache = categories;
                else
                    throw new Exception("Kategori Cache doldurulamadı.");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Kategori Cache doldurulma sırasında hata oluştu." + ex.Message);
                throw new Exception("Kategori Cache doldurulma sırasında hata oluştu.", ex);
            }

            _cacheProvider.Set(Enumarations.CacheKey.Kategoriler.ToString(), categoryCache);
            #endregion
        }

        public object GetCacheVal(string key)
        {
            object val = null;

            try
            {
                var categories = _cacheProvider.Get(key);
                if (categories != null)
                    val = categories;
            }
            catch (Exception ex)
            {
                val = null;
                Trace.WriteLine(key +" Cache ten okunamadı." + ex.Message);
                throw new Exception(key +" Cache ten okuma sırasında hata oluştu.", ex);
            }
            return val;
        }
    }
}

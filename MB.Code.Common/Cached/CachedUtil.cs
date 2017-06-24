using MB.Code.Caching;
using System;
using System.Collections;
using System.Configuration;



namespace MB.Code.Common.Cached
{
    public class CachedUtil
    {
        private static ICacheProvider Provider
        {
            get;
            set;
        }
        private static int Timeout
        {
            get;
            set;
        }
        static CachedUtil()
        {
            CachedSection cached = ConfigurationManager.GetSection("cached") as CachedSection;
            if (cached == null)
            {
                throw new Exception("没有cached节点配置");
            }
            CachedUtil.Provider = new CacheProviderFactory().Create(cached.Type);
            CachedUtil.Provider.SetServerInfo(cached.Host, cached.MaxPoolSize);
            CachedUtil.Timeout = cached.Timeout;
        }
        public static bool Contains(string key)
        {
            return CachedUtil.Provider.Contains(key);
        }
        public static T Get<T>(string key)
        {
            return CachedUtil.Provider.Get<T>(key);
        }
        public static IDictionary Gets(string[] keys)
        {
            return CachedUtil.Provider.Gets(keys);
        }
        public static void Set<T>(string key, T value)
        {
            DateTime dt = DateTime.Now.AddMinutes((double)CachedUtil.Timeout);
            CachedUtil.Provider.Set<T>(key, value, dt);
        }
        public static void Set<T>(string key, T value, DateTime expr)
        {
            CachedUtil.Provider.Set<T>(key, value, expr);
        }
        public static void Set<T>(string key, T value, TimeSpan ts)
        {
            CachedUtil.Provider.Set<T>(key, value, ts);
        }
        public static void SetExprieTime(string key, DateTime expr)
        {
            CachedUtil.Provider.SetExprieTime(key, expr);
        }
        public static void SetExprieTime(string key, TimeSpan expr)
        {
            CachedUtil.Provider.SetExprieTime(key, expr);
        }
        public static TimeSpan GetExprieTime(string key)
        {
            return CachedUtil.Provider.GetExprieTime(key);
        }
        public static void Remove(string key)
        {
            CachedUtil.Provider.Remove(key);
        }
        public static void Dispose()
        {
            CachedUtil.Provider.Dispose();
        }
    }
}

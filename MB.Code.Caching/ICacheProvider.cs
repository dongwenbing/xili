using System;
using System.Collections;

namespace MB.Code.Caching
{
    public interface ICacheProvider
    {
        bool Contains(string key);
        T Get<T>(string key);
        System.Collections.IDictionary Gets(string[] keys);
        void Set<T>(string key, T value);
        void Set<T>(string key, T value, System.DateTime expr);
        void Set<T>(string key, T value, System.TimeSpan ts);
        void SetExprieTime(string key, System.DateTime expr);
        void SetExprieTime(string key, System.TimeSpan expr);
        System.TimeSpan GetExprieTime(string key);
        void Remove(string key);
        void Dispose();
        void SetServerInfo(string host, int maxPoolSize);
    }
}
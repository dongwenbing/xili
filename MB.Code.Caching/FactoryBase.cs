using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
namespace MB.Code.Caching
{
    public abstract class FactoryBase<T> where T : class, ICacheProvider
    {
        private static System.Collections.Hashtable _cachedInstances;
        protected abstract string AssemblyString
        {
            get;
        }
        protected abstract System.Collections.Generic.List<string> ValidTypeNames
        {
            get;
        }
        protected abstract string DefaultTypeName
        {
            get;
        }
        static FactoryBase()
        {
            FactoryBase<T>._cachedInstances = new System.Collections.Hashtable();
            FactoryBase<T>._cachedInstances = System.Collections.Hashtable.Synchronized(FactoryBase<T>._cachedInstances);
        }
        protected static bool ExistsProduct(string typeName)
        {
            return FactoryBase<T>._cachedInstances.ContainsKey(typeName);
        }
        protected static void AddProduct(string typeName, object instance)
        {
            if (string.IsNullOrWhiteSpace(typeName) || instance == null)
            {
                return;
            }
            if (FactoryBase<T>.ExistsProduct(typeName))
            {
                return;
            }
            FactoryBase<T>._cachedInstances.Add(typeName, instance);
        }
        protected static T CreateProductInternal(string assemblyString, string typeName)
        {
            if (!FactoryBase<T>.ExistsProduct(typeName))
            {
                object product = System.Reflection.Assembly.Load(assemblyString).CreateInstance(typeName);
                FactoryBase<T>.AddProduct(typeName, product);
            }
            return FactoryBase<T>._cachedInstances[typeName] as T;
        }
        protected bool IsValidTypeName(string typeName)
        {
            return !string.IsNullOrWhiteSpace(typeName) && this.ValidTypeNames.Contains(typeName);
        }
        public abstract T CreateDefault();
        public abstract T Create(string typeName);
    }
}

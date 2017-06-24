using System;
using System.Collections.Generic;

namespace MB.Code.Caching
{
    public class CacheProviderFactory : FactoryBase<ICacheProvider>
    {
        private System.Collections.Generic.List<string> _validTypeNames;
        protected override string AssemblyString
        {
            get
            {
                return "MB.Code.Caching";
            }
        }
        protected override System.Collections.Generic.List<string> ValidTypeNames
        {
            get
            {
                if (this._validTypeNames == null)
                {
                    this._validTypeNames = new System.Collections.Generic.List<string>
                    {
                        "Redis",
                        "Memcache",
                        "Mysql"
                    };
                }
                return this._validTypeNames;
            }
        }
        protected override string DefaultTypeName
        {
            get
            {
                return "Mysql";
            }
        }
        public static ICacheProvider DefaultWriter
        {
            get
            {

                return null;
              //  return new RedisProvider(); 我自己修改的，去除使用redis数据库
            }
        }
        public override ICacheProvider CreateDefault()
        {
            string typeName = this.DefaultTypeName;
            return FactoryBase<ICacheProvider>.CreateProductInternal(this.AssemblyString, string.Format("{0}.{1}Provider", this.AssemblyString, typeName));
        }
        public override ICacheProvider Create(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName) || typeName.Length <= 1 || !base.IsValidTypeName(typeName))
            {
                throw new System.ArgumentException("The type is invalid!");
            }
            return FactoryBase<ICacheProvider>.CreateProductInternal(this.AssemblyString, string.Format("{0}.{1}Provider", this.AssemblyString, typeName));
        }
    }
}

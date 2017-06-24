using System;
using System.Configuration;

namespace MB.Code.Common.Cached
{


        public class CachedSection : ConfigurationSection
        {
            [ConfigurationProperty("type", DefaultValue = "redis")]
            public string Type
            {
                get
                {
                    return (string)base["type"];
                }
                set
                {
                    base["type"] = value;
                }
            }
            [ConfigurationProperty("host", IsRequired = true)]
            public string Host
            {
                get
                {
                    return (string)base["host"];
                }
                set
                {
                    base["host"] = value;
                }
            }
            [ConfigurationProperty("maxPoolSize", DefaultValue = 128)]
            public int MaxPoolSize
            {
                get
                {
                    return (int)base["maxPoolSize"];
                }
                set
                {
                    base["maxPoolSize"] = value;
                }
            }
            [ConfigurationProperty("timeout", DefaultValue = 20)]
            public int Timeout
            {
                get
                {
                    return (int)base["timeout"];
                }
                set
                {
                    base["timeout"] = value;
                }
            }
        }
    

}

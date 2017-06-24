using System;
using System.Runtime.Serialization;

namespace MB.Code.Common
{
        [DataContract]
        [Serializable]
        public class Result<T> : BaseResult
        {
            [DataMember]
            public T Data
            {
                get;
                set;
            }
            [DataMember]
            public int TotalCount
            {
                get;
                set;
            }
        }
    }

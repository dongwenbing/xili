using System;
using System.Runtime.Serialization;

namespace MB.Code.Common
{


        [DataContract]
        [Serializable]
        public class BaseResult
        {
            [DataMember]
            public ResultStatus Status
            {
                get;
                set;
            }
            [DataMember]
            public Error Error
            {
                get;
                set;
            }
            public BaseResult()
            {
                this.Status = ResultStatus.Success;
            }
        }
    

}

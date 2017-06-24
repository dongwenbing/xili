using System;
using System.Runtime.Serialization;


namespace MB.Code.Common
{
    [DataContract]
    [Serializable]
    public class Error
    {
        [DataMember]
        public int ErrCode
        {
            get;
            set;
        }
        [DataMember]
        public string Titles
        {
            get;
            set;
        }
        [DataMember]
        public string Message
        {
            get;
            set;
        }
        [DataMember]
        public string Detail
        {
            get;
            set;
        }
        [DataMember]
        public object Tag
        {
            get;
            set;
        }
    }
}

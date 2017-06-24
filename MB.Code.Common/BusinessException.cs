using System;


namespace MB.Code.Common
{
    using System;

        public class BusinessException : Exception
        {
            public int ErrCode
            {
                get;
                private set;
            }
            public Error Error
            {
                get;
                private set;
            }
            private BusinessException(string message) : this(message, 0)
            {
            }
            private BusinessException(string message, int errCode) : base(message)
            {
                this.ErrCode = errCode;
            }
            private BusinessException(string message, int errCode, Exception innerException) : base(message, innerException)
            {
                this.ErrCode = errCode;
            }
            public static BusinessException GetException(string message)
            {
                return BusinessException.GetException(message, null);
            }
            public static BusinessException GetException(string message, Exception innerException)
            {
                return BusinessException.GetException(message, 0, innerException);
            }
            public static BusinessException GetException(string message, int errCode)
            {
                return BusinessException.GetException(message, errCode, null);
            }
            public static BusinessException GetException(string message, int errCode, Exception innerException)
            {
                BusinessException ex = new BusinessException(message, errCode, innerException);
                ex.Error = new Error
                {
                    Detail = ex.ToString(),
                    Message = ex.Message,
                    ErrCode = ex.ErrCode
                };
                return ex;
            }
            public static BusinessException GetException(Error err)
            {
                return new BusinessException(err.Message, err.ErrCode)
                {
                    Error = err
                };
            }
        }
    

}

using System;

namespace Saleman.Service.Exceptions
{
    public class CommonException : BusinessException
    {
        public CommonException(string message, Exception inner) : base(message, inner)
        {
        }

        public override int Code
        {
            get
            {
                return (int)ErrorCode.Common;
            }
        }
    }
}
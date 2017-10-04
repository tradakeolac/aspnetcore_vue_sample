using System;

namespace Saleman.Service.Exceptions
{
    public class NotFoundObjectException : BusinessException
    {
        public NotFoundObjectException(string message, Exception inner) : base(message, inner)
        {
        }

        public override int Code
        {
            get
            {
                return (int)ErrorCode.NotFoundObject;
            }
        }
    }
}

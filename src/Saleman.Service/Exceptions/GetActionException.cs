using System;

namespace Saleman.Service.Exceptions
{

    public class GetActionException : BusinessException
    {
        public GetActionException(string message, Exception inner) : base(message, inner)
        {
        }

        public override int Code
        {
            get
            {
                return (int)ErrorCode.Get;
            }
        }
    }
}
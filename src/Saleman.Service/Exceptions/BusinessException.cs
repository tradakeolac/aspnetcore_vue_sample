using System;

namespace Saleman.Service.Exceptions
{
    public abstract class BusinessException : Exception
    {
        protected BusinessException(string message, Exception inner) : base(message, inner)
        {
        }

        public abstract int Code { get; }

        public string Type
        {
            get { return this.GetType().Name; }
        }
        public override string HelpLink
        {
            get
            {
                return $"http://api.saleman.com/references/errors/{Type.ToLower()}.html";
            }
        }
    }
}
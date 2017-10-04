namespace Saleman.Service.Exceptions
{
    using System;
    using System.Reflection;

    public static class ExceptionExtensions
    {
        public static TBusinessException ToBusinessException<TBusinessException>(this Exception exception)
            where TBusinessException : BusinessException
        {
            return exception.ToBusinessException<TBusinessException>(exception.Message);
        }

        public static TBusinessException ToBusinessException<TBusinessException>(this Exception exception, string message)
            where TBusinessException : BusinessException
        {
            return Activator.CreateInstance(typeof(TBusinessException), message, exception) as TBusinessException;
        }
    }
}
namespace Saleman.Data.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PersistanceException : Exception
    {
        public static PersistanceException CreateDefault(Exception inner)
        {
            return new PersistanceException("Entityframework update exception", inner);
        }

        public PersistanceException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}

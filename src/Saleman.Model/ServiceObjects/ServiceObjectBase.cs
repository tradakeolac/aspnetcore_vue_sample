using System;

namespace Saleman.Model.ServiceObjects
{
    public abstract class ServiceObjectBase
    {

    }

    public abstract class ServiceObjectBase<TKey, TNullObject> : ServiceObjectBase
        where TNullObject : ServiceObjectBase, new()
    {
        public TKey Id { get; set; }

        public static TNullObject NullServiceObject { get; } = Activator.CreateInstance<TNullObject>();

        public virtual bool IsNullObject()
        {
            return this == NullServiceObject;
        }
    }
}

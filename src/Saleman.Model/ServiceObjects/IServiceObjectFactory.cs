namespace Saleman.Model.ServiceObjects
{
    public interface IServiceObjectFactory
    {
        TServiceObject Create<TServiceObject>(object source) where TServiceObject : class;
    }
}
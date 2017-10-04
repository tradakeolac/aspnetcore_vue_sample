namespace Saleman.Web.ViewModel
{
    public abstract class ViewModelBase
    {
    }

    public abstract class ViewModelBase<TKey>
    {
        public TKey Id { get; set; }
    }
}

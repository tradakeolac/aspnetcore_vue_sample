namespace Saleman.Web.ViewModel
{
    public interface IViewModelFactory
    {
        TViewModel Create<TViewModel>(object serviceObject) where TViewModel : class;
    }
}
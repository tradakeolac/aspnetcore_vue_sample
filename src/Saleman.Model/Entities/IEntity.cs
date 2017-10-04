namespace Saleman.Model.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
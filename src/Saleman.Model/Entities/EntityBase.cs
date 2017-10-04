namespace Saleman.Model.Entities
{
    using System.ComponentModel.DataAnnotations;

    public abstract class EntityBase
    {

    }

    public abstract class EntityBase<TKey> : EntityBase, IEntity<TKey>
    {
        [Required]
        [Key]
        public virtual TKey Id { get; set; }
    }
}
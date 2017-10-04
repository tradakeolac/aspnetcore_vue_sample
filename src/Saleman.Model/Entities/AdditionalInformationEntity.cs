namespace Saleman.Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class AdditionalInformationEntity : EntityBase<Guid>
    {
        [MaxLength(300)]
        public virtual string Name { get; set; }
        
        public virtual Guid StoreDetailId { get; set; }
    }

    public class GenericInformationEntity : AdditionalInformationEntity
    {
        [MaxLength(1000)]
        public virtual string Information { get; set; }
    }

    public class PhoneInformationEntity : AdditionalInformationEntity
    {
        [DataType(DataType.PhoneNumber)]
        [Required]
        [MaxLength(1000)]
        public virtual string Information { get; set; }
    }
    public class EmailInformationEntity : AdditionalInformationEntity
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        [MaxLength(1000)]
        public virtual string Information { get; set; }
    }

    public class SocialInformationEntity : AdditionalInformationEntity
    {
        [DataType(DataType.Url)]
        [Required]
        [MaxLength(1000)]
        public virtual string Information { get; set; }
    }
}

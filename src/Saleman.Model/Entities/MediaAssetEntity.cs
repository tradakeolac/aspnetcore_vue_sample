namespace Saleman.Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class MediaAssetEntity : EntityBase<Guid>
    {
        [MaxLength(300)]
        [Required]
        public virtual string Name { get; set; }

        [MaxLength(1000)]
        public virtual string Description { get; set; }

        [MaxLength(1000)]
        public virtual string Link { get; set; }
        
        [MaxLength(1000)]
        public string FileName { get; set; }

        public virtual string CreatedById { get; set; }

        public virtual UserEntity CreatedBy { get; set; }

        public static class MediaType
        {
            public const string Video = "video";
            public const string Image = "image";
            public const string Pdf = "pdf";
            public const string Unknow = "unknow";
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Saleman.Web.ViewModel
{
    public class UnitViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Symbol { get; set; }

        public string Description { get; set; }
    }
}

namespace Saleman.Web.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class SaleViewModel : ViewModelBase<long>
    {
    }

    public class SaleAuditViewModel : ViewModelBase<Guid>
    {

    }

    public class CreateAuditRequestViewModel : ViewModelBase
    {
        [Required]
        public decimal TotalAmount { get; set; }
    }
}

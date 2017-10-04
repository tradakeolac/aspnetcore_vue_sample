namespace Saleman.Web.ViewModel.Application
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MenuItemViewModel : ViewModelBase<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<MenuItemViewModel> SubItems { get; set; }
    }
}

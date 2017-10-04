namespace Saleman.Web.ViewModel
{
    using System;

    public class MediaViewModel : ViewModelBase<Guid>
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}

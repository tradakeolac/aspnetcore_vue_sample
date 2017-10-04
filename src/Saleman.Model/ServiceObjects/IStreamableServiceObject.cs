namespace Saleman.Model.ServiceObjects
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IStreamableServiceObject
    {
        Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken));
        string Name { get; set; }
    }
}

namespace Saleman.Model.ServiceObjects
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class MediaServiceObjectBase<TNullObject> : ServiceObjectBase<Guid, TNullObject>
        where TNullObject : MediaServiceObjectBase<TNullObject>, new()
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public string FileName { get; set; }
    }

    public class MediaServiceObject : MediaServiceObjectBase<MediaServiceObject>
    {
        public string MediaType { get; set; }
        public string CreatedById { get; set; }
    }


    public class MediaStreamableServiceObject : MediaServiceObject, IStreamableServiceObject
    {
        private readonly Func<Stream, CancellationToken, Task> CopyAction;

        public MediaStreamableServiceObject()
        {

        }

        public MediaStreamableServiceObject(Func<Stream, CancellationToken, Task> save)
        {
            this.CopyAction = save;
        }

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (this.CopyAction != null)
                await this.CopyAction.Invoke(target, cancellationToken);

            await Task.FromResult<object>(null);
        }
    }
}

namespace Saleman.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task SendAsync(string email, string subject, string message);
    }
}

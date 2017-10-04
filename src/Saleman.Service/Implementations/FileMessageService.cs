namespace Saleman.Service.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class FileMessageService : IMessageService
    {
        public async Task SendAsync(string email, string subject, string message)
        {
            await Task.Factory.StartNew(() =>
            {
                var emailMessage = $"To: {email}\nSubject: {subject}\nMessage: {message}\n\n";

                File.AppendAllText("emails.txt", emailMessage);
            });
        }
    }
}

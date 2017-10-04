namespace Saleman.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Saleman.Model.ServiceObjects;
    using System.Threading.Tasks;

    public interface IAdditionalInformationService : IService<AdditionalInformationServiceObject, Guid>
    {
        Task<IEnumerable<AdditionalInformationServiceObject>> GetSocialInformationAsync(Guid storeId);

        /// <summary>
        /// Method to get store information (Email, phone, social media url : facebook, linkedin, google+, twitter etc..)
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        Task<IEnumerable<AdditionalInformationServiceObject>> GetStoreContactAsync(Guid storeId);
    }
}

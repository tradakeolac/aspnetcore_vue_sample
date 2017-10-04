namespace Saleman.Service
{
    using Model.ServiceObjects;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAddressService : IService<AddressServiceObject, Guid>
    {
        Task<IEnumerable<LocationServiceObject>> GetAllDistrictsAsync();
        Task<IEnumerable<LocationServiceObject>> GetAllProvincesAsync();
        Task<IEnumerable<LocationServiceObject>> GetDistrictsInProvinceAsync(Guid provinceId);
        Task<LocationServiceObject> AddProvinceAsync(LocationServiceObject provinceServiceObject);
        Task<LocationServiceObject> AddDistrictAsync(LocationServiceObject districtServiceObject);
    }
}

namespace Saleman.Service.Implementations
{
    using Model.Entities;
    using Model.ServiceObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Repositories;
    using WebFramework.Infrastructure.Configurations;
    using Queries;

    public class AddressService : SalemanServiceBase<AddressServiceObject, Guid, AddressEntity>, IAddressService
    {
        protected readonly IAddressRepository AddressRepository;

        public AddressService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader, IWebFrameworkConfiguration configuration,
            IAddressRepository repository, IServiceObjectFactory objectFactory, IEntityFactory entityFactory)
            : base(unitOfWork, dataLoader, configuration, repository, objectFactory, entityFactory)
        {
            this.AddressRepository = repository;
        }

        public async Task<LocationServiceObject> AddDistrictAsync(LocationServiceObject districtServiceObject)
        {
            var district = this.EntityFactory.Create<DistrictEntity>(districtServiceObject);

            this.AddressRepository.Add(district);

            await this.UnitOfWork.SaveChangeAsync();

            return this.ObjectFactory.Create<LocationServiceObject>(district);
        }

        public async Task<LocationServiceObject> AddProvinceAsync(LocationServiceObject provinceServiceObject)
        {
            var province = this.EntityFactory.Create<ProvinceEntity>(provinceServiceObject);

            this.AddressRepository.Add(province);

            await this.UnitOfWork.SaveChangeAsync();

            return this.ObjectFactory.Create<LocationServiceObject>(province);
        }

        public async Task<IEnumerable<LocationServiceObject>> GetAllDistrictsAsync()
        {
            var districts = await this.AddressRepository.GetAllAsync<DistrictEntity>();

            var districtServiceObjects = new List<LocationServiceObject>();

            if (districts != null && districts.Any())
                districtServiceObjects.AddRange(districts.Select(d => this.ObjectFactory.Create<LocationServiceObject>(d)));

            return await Task.FromResult(districtServiceObjects).ConfigureAwait(false);
        }

        public async Task<IEnumerable<LocationServiceObject>> GetAllProvincesAsync()
        {
            var provinces = await this.AddressRepository.GetAllAsync<ProvinceEntity>();

            var provinceServiceObjects = new List<LocationServiceObject>();

            if (provinces != null && provinces.Any())
                provinceServiceObjects.AddRange(provinces.Select(d => this.ObjectFactory.Create<LocationServiceObject>(d)));

            return await Task.FromResult(provinceServiceObjects).ConfigureAwait(false);
        }

        public async Task<IEnumerable<LocationServiceObject>> GetDistrictsInProvinceAsync(Guid provinceId)
        {
            var getDistrictByProvinceIdQuery = new GetDistrictByProvinceIdQuery(provinceId);

            var districts = await this.AddressRepository.FindAsync(getDistrictByProvinceIdQuery);

            if (districts == null || !districts.Any())
                return Enumerable.Empty<LocationServiceObject>();

            return districts.Select(ObjectFactory.Create<LocationServiceObject>).AsEnumerable();
        }
    }
}

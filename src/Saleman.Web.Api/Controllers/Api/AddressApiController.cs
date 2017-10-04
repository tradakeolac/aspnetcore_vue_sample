using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Saleman.Web.ViewModel;
using Saleman.Model.ServiceObjects;
using Saleman.Service;
using Saleman.Service.Exceptions;

namespace Saleman.Web.Api.Controllers.Api
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [Authorize]
    public class AddressApiController : SalemanApiControllerBase<AddressViewModel, AddressServiceObject, Guid>
    {
        protected readonly IAddressService AddressService;

        public AddressApiController(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
            IAddressService addressService, IUserService userService) : base(objectFactory, viewModelFactory, addressService, userService)
        {
            this.AddressService = addressService;
        }

        [HttpGet]
        [Route("addresses")]
        public override Task<IActionResult> Get()
        {
            return base.Get();  
        }

        [HttpGet]
        [Route("addresses/{id}")]
        public override Task<IActionResult> Get(Guid id)
        {
            return base.Get(id);
        }

        [HttpPost]
        [Route("addresses")]
        public override Task<IActionResult> Post([FromBody] AddressViewModel viewModel)
        {
            return base.Post(viewModel);
        }

        [HttpPut]
        [Route("addresses/{id}")]
        public override Task<IActionResult> Put([FromBody] AddressViewModel updateModel)
        {
            return base.Put(updateModel);
        }

        [HttpDelete]
        [Route("addresses/{id}")]
        public override Task<IActionResult> Delete(Guid id)
        {
            return base.Delete(id);
        }

        [HttpPost]
        [Route("provinces")]
        public async Task<IActionResult> AddProvince([FromBody] LocationViewModel province)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var serviceObject = await this.AddressService.AddProvinceAsync(this.ObjectFactory.Create<LocationServiceObject>(province));

                if (!serviceObject.IsNullObject())
                    return Ok(this.ViewModelFactory.Create<LocationViewModel>(serviceObject));
                return Forbid();
            }
            catch(BusinessException exception)
            {
                return InternalErrorResult(exception);
            }
        }

        [HttpPost]
        [Route("districts")]
        public async Task<IActionResult> AddDistrict([FromBody] LocationViewModel district)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var serviceObject = await this.AddressService.AddDistrictAsync(this.ObjectFactory.Create<LocationServiceObject>(district));

                if (!serviceObject.IsNullObject())
                    return Ok(this.ViewModelFactory.Create<LocationViewModel>(serviceObject));

                return Forbid();
            }
            catch(BusinessException exception)
            {
                return InternalErrorResult(exception);
            }
        }
    }
}

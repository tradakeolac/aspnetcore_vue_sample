using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saleman.Web.ViewModel;
using Saleman.Model.ServiceObjects;
using Saleman.Service;
using Microsoft.AspNetCore.Authorization;
using Saleman.Service.Exceptions;

namespace Saleman.Web.Api.Controllers.Api.Store
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/")]
    public class StoreInformationApiController : SalemanApiControllerBase<AdditionalInformationViewModel, AdditionalInformationServiceObject, Guid>
    {
        protected readonly IAdditionalInformationService AdditionalInformationService;

        public StoreInformationApiController(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
            IAdditionalInformationService salemanService, IUserService userService) 
            : base(objectFactory, viewModelFactory, salemanService, userService)
        {
            this.AdditionalInformationService = salemanService;
        }

        [Authorize]
        [HttpPost]
        [Route("additionalinformation")]
        public override Task<IActionResult> Post([FromBody] AdditionalInformationViewModel viewModel)
        {
            return base.Post(viewModel);
        }

        [Route("stores/{storeId}/contactinformation")]
        [HttpGet]
        public async Task<IActionResult> GetContactInformation([FromRoute] Guid storeId)
        {
            try
            {
                var info = await this.AdditionalInformationService.GetStoreContactAsync(storeId);

                return Ok(info.Select(ViewModelFactory.Create<AdditionalInformationViewModel>));
            }
            catch(GetActionException ex)
            {
                // Log
                throw;
            }
            catch (MappingException ex)
            {
                // Log
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saleman.Service;
using Saleman.Web.ViewModel;
using Saleman.Model.ServiceObjects;
using Microsoft.AspNetCore.Authorization;


namespace Saleman.Web.Api.Controllers.Api
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/stores")]
    public class StoreApiController : SalemanApiControllerBase<StoreViewModel, StoreServiceObject, Guid>
    {
        protected readonly IStoreService StoreService;
        public StoreApiController(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
            IStoreService storeService, IUserService userService) : base(objectFactory, viewModelFactory, storeService, userService)
        {
            this.StoreService = storeService;
        }


        [Authorize]
        [Route("owner")]
        public async Task<IActionResult> GetCurrentUserStores()
        {
            var stores = await this.StoreService.GetStoreBy(GetCurrentUserId());

            if(stores != null && stores.Any())
            {
                return Ok(stores.Select(ViewModelFactory.Create<StoreViewModel>));
            }

            return Ok(Enumerable.Empty<StoreViewModel>());
        }

        [Authorize]
        [HttpPost]
        public async override Task<IActionResult> Post([FromBody] StoreViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.OwnerId = this.GetCurrentUserId();

            return await base.Post(viewModel);
        }
    }
}

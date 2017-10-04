using Saleman.Model.ServiceObjects;
using Saleman.Web.ViewModel;
using System;
using Saleman.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using Saleman.Service.Exceptions;

namespace Saleman.Web.Api.Controllers.Api
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoryApiController : SalemanApiControllerBase<CategoryViewModel, CategoryServiceObject, Guid>
    {
        protected readonly ICategoryService CategoryService;

        public CategoryApiController(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
            ICategoryService salemanService, IUserService userService)
            : base(objectFactory, viewModelFactory, salemanService, userService)
        {
            this.CategoryService = salemanService;
        }

        [HttpGet]
        [Route("stores/{storeId}")]
        public async Task<IActionResult> CategoriesByStore([FromRoute] Guid storeId)
        {
            if (storeId == Guid.Empty)
                return BadRequest();

            var categories = await this.CategoryService.GetCategoryByStoreAsync(storeId);

            var ret = categories != null && categories.Any() ? categories.Select(ViewModelFactory.Create<CategoryViewModel>) : Enumerable.Empty<CategoryViewModel>();

            return Ok(ret);
        }

        [HttpGet]
        [Route("")]
        public override async Task<IActionResult> Get()
        {
            try
            {
                var categories = await this.CategoryService.GetCategoryByUserIdAsync(this.GetCurrentUserId());

                if (categories != null && categories.Any())
                    return Ok(categories.Select(ViewModelFactory.Create<CategoryFullInformationViewModel>));

                return Ok(Enumerable.Empty<CategoryFullInformationViewModel>());
            }
            catch(BusinessException ex)
            {
                return InternalErrorResult(ex);
            }
        }
    }
}

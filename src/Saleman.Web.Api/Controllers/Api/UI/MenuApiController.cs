using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saleman.Web.ViewModel.Application;
using Saleman.Model.ServiceObjects;
using Saleman.Service;
using Saleman.Web.ViewModel;
using Saleman.Service.Exceptions;

namespace Saleman.Web.Api.Controllers.Api.UI
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ui/menu")]
    public class MenuApiController : SalemanApiControllerBase<MenuItemViewModel, CategoryServiceObject, Guid>
    {
        protected readonly ICategoryService CategoryService;

        public MenuApiController(IServiceObjectFactory objectFactory, 
            IViewModelFactory viewModelFactory, ICategoryService salemanService, IUserService userService)
            : base(objectFactory, viewModelFactory, salemanService, userService)
        {
            this.CategoryService = salemanService;
        }

        [HttpGet]
        [Route("")]
        public override async Task<IActionResult> Get()
        {
            try
            {
                var categories = await this.CategoryService.GetCategoryShownOnMenuAsync();
                var result = categories != null && categories.Any()
                    ? categories.Select(ViewModelFactory.Create<MenuItemViewModel>)
                    : Enumerable.Empty<MenuItemViewModel>();

                return Ok(result);
            }
            catch(BusinessException ex)
            {
                return InternalErrorResult(ex);
            }
        }
    }
}

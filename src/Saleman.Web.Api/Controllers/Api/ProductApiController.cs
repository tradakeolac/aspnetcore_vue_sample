using Saleman.Model.ServiceObjects;
using Saleman.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saleman.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Saleman.Web.Api.Controllers.Api
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductApiController : SalemanApiControllerBase<ProductViewModel, ProductServiceObject, Guid>
    {
        protected readonly IProductService ProductService;

        public ProductApiController(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
            IProductService productService, IUserService userService)
            : base(objectFactory, viewModelFactory, productService, userService)
        {
            this.ProductService = productService;
        }
    }
}

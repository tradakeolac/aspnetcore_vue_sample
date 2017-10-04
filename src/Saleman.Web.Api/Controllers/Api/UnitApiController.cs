using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saleman.Web.ViewModel;
using Saleman.Service;
using Saleman.Model.ServiceObjects;
using Saleman.Service.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Saleman.Web.Api.Controllers.Api
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/units")]
    [Authorize]
    public class UnitApiController : SalemanApiControllerBase<UnitViewModel, UnitServiceObject, Guid>
    {
        protected readonly IUnitService UnitService;

        public UnitApiController(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
            IUnitService unitService, IUserService userService)
            : base(objectFactory, viewModelFactory, unitService, userService)
        {
            this.UnitService = unitService;
        }
    }
}

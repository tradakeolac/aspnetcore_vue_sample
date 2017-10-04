using Saleman.Model.ServiceObjects;
using Saleman.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saleman.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saleman.Service.Exceptions;
using System.Security.Claims;
using Saleman.Web.Infrastructure.Extensions;

namespace Saleman.Web.Api.Controllers.Api
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/sales")]
    public class SaleApiController : SalemanApiControllerBase<SaleViewModel, SaleServiceObject, long>
    {
        protected readonly ISaleService SaleService;
        protected readonly ISaleAuditService SaleAuditService;

        public SaleApiController(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
            ISaleService saleService, ISaleAuditService auditService, IUserService userService)
            : base(objectFactory, viewModelFactory, saleService, userService)
        {
            this.SaleService = saleService;
            this.SaleAuditService = auditService;
        }

        [HttpPost]
        [Route("{saleId}/logs")]
        public async Task<IActionResult> CreateAuditRequest([FromRoute]long saleId, [FromBody] CreateAuditRequestViewModel auditRequestViewModel)
        {
            if (saleId <= 0)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var auditServiceObject = this.ObjectFactory.Create<SaleAuditServiceObject>(auditRequestViewModel);

                auditServiceObject = await this.SaleAuditService.AddAsync(auditServiceObject);

                return Ok(new { success = !auditServiceObject.IsNullObject() });
            }
            catch (BusinessException ex)
            {
                return InternalErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("{saleId}/logs/{logId}/approve")]
        public async Task<IActionResult> ApproveSaleAudit([FromRoute] long saleId, [FromRoute] Guid logId)
        {
            if (saleId <= 0 || logId == Guid.Empty)
                return BadRequest();

            try
            {
                var approve = await this.SaleAuditService.ApproveAsync(logId, User.GetUserId());

                return Ok(new { success = approve });
            }
            catch (BusinessException ex)
            {
                // Log or process another business
                return InternalErrorResult(ex);
            }
        }
    }
}

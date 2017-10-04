using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saleman.Model.ServiceObjects;
using Saleman.Web.ViewModel;
using Saleman.Service;
using Saleman.Service.Exceptions;
using Microsoft.AspNetCore.Authorization;
using AspNet.Security.OpenIdConnect.Primitives;

namespace Saleman.Web.Api.Controllers.Api
{
    [Route("api/[controller]")]
    public abstract class SalemanApiControllerBase : Controller
    {
        protected readonly IServiceObjectFactory ObjectFactory;
        protected readonly IViewModelFactory ViewModelFactory;
        protected readonly IUserService UserService;

        protected SalemanApiControllerBase(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
            IUserService userService)
        {
            this.ObjectFactory = objectFactory;
            this.ViewModelFactory = viewModelFactory;
            this.UserService = userService;
        }

        protected virtual IActionResult InternalErrorResult(BusinessException exception)
        {
            return StatusCode(500, exception);
        }

        protected virtual IActionResult InternalErrorResult()
        {
            return StatusCode(500);
        }

        protected virtual string GetCurrentUserId()
        {
            if (this.User.Identity != null && this.User.Identity.IsAuthenticated)
            {
                var userIdClaim = this.User.FindFirst(s => s.Type == OpenIdConnectConstants.Claims.Subject);
                return userIdClaim?.Value;
            }

            return null;
        }
    }

    public abstract class SalemanApiControllerBase<TViewModel, TServiceObject, TKey> : SalemanApiControllerBase
        where TServiceObject : ServiceObjectBase<TKey, TServiceObject>, new() where TViewModel : class
    {
        protected readonly IService<TServiceObject, TKey> SalemanService;

        protected SalemanApiControllerBase(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
            IService<TServiceObject, TKey> salemanService, IUserService userService) 
            : base(objectFactory, viewModelFactory, userService)
        {
            this.SalemanService = salemanService;
        }


        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            try
            {
                var result = new List<TViewModel>();
                var serviceObjects = await this.SalemanService.GetAllAsync();
                if (serviceObjects != null && serviceObjects.Any())
                {
                    result = serviceObjects.Select(u => this.ViewModelFactory.Create<TViewModel>(u)).ToList();
                }

                return Ok(result);
            }
            catch (BusinessException ex)
            {
                // Log or process another business
                return InternalErrorResult(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> Get([FromRoute] TKey id)
        {
            try
            {
                var serviceObject = await this.SalemanService.GetByIdAsync(id);

                if (serviceObject.IsNullObject())
                    return NotFound($"Can not get the entity");

                return Ok(this.ViewModelFactory.Create<TViewModel>(serviceObject));
            }
            catch (BusinessException ex)
            {
                // Log or process another business
                return InternalErrorResult(ex);
            }
        }

        [HttpPost]
        [Authorize]
        public virtual async Task<IActionResult> Post([FromBody]TViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var requestServiceObject = this.ObjectFactory.Create<TServiceObject>(viewModel);
            try
            {
                requestServiceObject = await this.SalemanService.AddAsync(requestServiceObject);
                return Ok(this.ViewModelFactory.Create<TViewModel>(requestServiceObject));
            }
            catch (BusinessException ex)
            {
                // Log or process another business
                return InternalErrorResult(ex);
            }
        }
        
        [Authorize]
        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody]TViewModel updateModel)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var serviceObjectRequest = ObjectFactory.Create<TServiceObject>(updateModel);
            try
            {
                var result = await this.SalemanService.UpdateAsync(serviceObjectRequest);
                return Ok(this.ViewModelFactory.Create<TViewModel>(result));
            }
            catch (BusinessException ex)
            {
                // Log or process another business
                return InternalErrorResult(ex);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public virtual async Task<IActionResult> Delete([FromRoute] TKey id)
        {
            try
            {
                var result = await this.SalemanService.DeleteAsync(id);
                return Ok(this.ViewModelFactory.Create<ResultViewModel>(result));
            }
            catch (BusinessException ex)
            {
                // Log or process another business
                return InternalErrorResult(ex);
            }
        }

        [HttpDelete("entities")]
        [Authorize]
        public virtual async Task<IActionResult> Delete([FromBody] IEnumerable<TKey> ids)
        {
            try
            {
                var result = await this.SalemanService.DeleteAsync(ids);

                return Ok(this.ViewModelFactory.Create<ResultViewModel>(result));
            }
            catch(BusinessException ex)
            {
                // Log or process another business
                return InternalErrorResult(ex);
            }
        }
    }
}

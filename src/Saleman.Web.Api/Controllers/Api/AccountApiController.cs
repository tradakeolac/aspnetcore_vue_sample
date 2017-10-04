using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saleman.Model.ServiceObjects;
using Saleman.Web.ViewModel;
using Saleman.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Server;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Extensions;

namespace Saleman.Web.Api.Controllers.Api
{
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/accounts")]
	public class AccountApiController : SalemanApiControllerBase
	{
		protected readonly IAuthenticationService AuthenticationService;
		protected readonly IMessageService MessageService;

		public AccountApiController(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
			IAuthenticationService authenticationService, IUserService userService, IMessageService messageService)
            : base(objectFactory, viewModelFactory, userService)
        {
			this.AuthenticationService = authenticationService;
			this.MessageService = messageService;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
		{
			if (!ModelState.IsValid)
				return await Task.FromResult(BadRequest(ModelState));

			var user = await this.UserService.CreateUserAsync(model.Email, model.Password);

            return Ok(user.IsNullObject() ? ResultViewModel.Fail : ResultViewModel.Success);
		}

		[Route("verify")]
		[HttpPost]
		public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var verifiedResult = await this.AuthenticationService.VerifyEmailAsync(model.Id, model.Token);

			return Ok(this.ViewModelFactory.Create<ResultViewModel>(verifiedResult));
		}

		[HttpPost]
		[Route("forgotpassword")]
		public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var token = await this.AuthenticationService.GenerateConfirmTokenAsync(model.Email);

			var passwordResetUrl = Url.Action(nameof(ResetPassword), "Account", new { id = token.Id, token = token.Token }, Request.Scheme);

			await this.MessageService.SendAsync(model.Email, "Password reset", $"Click <a href=\"" + passwordResetUrl + "\">here</a> to reset your password");

			return Ok(ResultViewModel.Success);
		}

		[HttpPost]
		[Route("resetpassword")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var request = this.ObjectFactory.Create<ResetPasswordServiceObject>(model);

			var resultServiceObject = await this.AuthenticationService.ResetPasswordAsync(request);

			return Ok(this.ViewModelFactory.Create<ResultViewModel>(resultServiceObject));
		}

		[HttpPost]
		[Authorize]
		[Route("logout")]
		public async Task<IActionResult> Logout()
		{
			await this.AuthenticationService.SignoutAsync();
            
            // Returning a SignOutResult will ask OpenIddict to redirect the user agent
            // to the post_logout_redirect_uri specified by the client application.
            return SignOut(OpenIdConnectServerDefaults.AuthenticationScheme);
		}
	}
}

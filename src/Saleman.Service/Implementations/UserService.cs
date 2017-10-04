namespace Saleman.Service.Implementations
{
    using Data.Repositories;
    using WebFramework.Infrastructure.Configurations;
    using Model.ServiceObjects;
    using System.Threading.Tasks;
    using Model.Entities;
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Saleman.Service.Exceptions;

    public class UserService : AuthenticationServiceBase, IUserService, IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader, UserManager<IdentityUser> userManager,
            IWebFrameworkConfiguration configuration, IRepository repository, IServiceObjectFactory objectFactory, SignInManager<IdentityUser> signinManager,
            IEntityFactory entityFactory) : base(unitOfWork, dataLoader, configuration, repository, objectFactory, entityFactory)
        {
            this._userManager = userManager;
            this._signInManager = signinManager;
        }

        public async Task<AuthenticationServiceObject> AuthenticateAsync(string userName, string password)
        {
            // Validate the user credentials.
            // Note: to mitigate brute force attacks, you SHOULD strongly consider
            // applying a key derivation function like PBKDF2 to slow down
            // the password validation process. You SHOULD also consider
            // using a time-constant comparer to prevent timing attacks.

            var user = await this._userManager.FindByEmailAsync(userName);

            if (user == null)
            {
                return AuthenticationServiceObject.NullServiceObject;
            }

            var verifiedPassword = await this._userManager.CheckPasswordAsync(user, password);

            return verifiedPassword
                ? this.ObjectFactory.Create<AuthenticationServiceObject>(user)
                : AuthenticationServiceObject.NullServiceObject;
        }

        public async Task<UserServiceObject> CreateUserAsync(string email, string password)
        {
            var newUser = new IdentityUser
            {
                UserName = email,
                Email = email
            };

            var userCreationResult = await this._userManager.CreateAsync(newUser, password);

            if (userCreationResult.Succeeded)
            {
                var user = await this._userManager.FindByEmailAsync(email);
                if (user != null && await this._userManager.CheckPasswordAsync(user, password))
                {
                    var extendedUser = new UserEntity() { Id = user.Id, User = user };

                    this.Repository.Add(extendedUser);

                    await this.UnitOfWork.SaveChangeAsync();

                    return this.ObjectFactory.Create<UserServiceObject>(user);    
                }
            }

            return UserServiceObject.NullServiceObject;
        }
        
        public async Task<TokenServiceObject> GenerateConfirmTokenAsync(string email)
        {
            var user = await this._userManager.FindByEmailAsync(email);

            if (user == null)
                return TokenServiceObject.NullServiceObject;

            var token = await this._userManager.GeneratePasswordResetTokenAsync(user);

            if (string.IsNullOrEmpty(token))
                return TokenServiceObject.NullServiceObject;

            return new TokenServiceObject { Id = user.Id, Token = token };
        }

        public async Task<UserServiceObject> GetUserByUserNameAsync(string userName)
        {
            var user = await this._userManager.FindByNameAsync(userName);

            return user != null ? ObjectFactory.Create<UserServiceObject>(user) : UserServiceObject.NullServiceObject;
        }

        public async Task<ResultServiceObject> ResetPasswordAsync(ResetPasswordServiceObject request)
        {
            Guard.EnsureRequestNotNull(request, "ChangePasswordRequest", nameof(UserService));

            var user = await this._userManager.FindByIdAsync(request.Id);

            if (user == null)
                throw new NotFoundObjectException("Can not find the user with id " + request.Id, null);

            var resetPasswordResult = await this._userManager.ResetPasswordAsync(user, request.Token, request.Password);

            return this.ObjectFactory.Create<ResultServiceObject>(resetPasswordResult);
        }

        public async Task SignoutAsync()
        {
            await this._signInManager.SignOutAsync();
        }

        public async Task<ResultServiceObject> VerifyEmailAsync(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException();

            var emailConfirmationResult = await _userManager.ConfirmEmailAsync(user, token);

            return this.ObjectFactory.Create<ResultServiceObject>(emailConfirmationResult);
        }
    }
}
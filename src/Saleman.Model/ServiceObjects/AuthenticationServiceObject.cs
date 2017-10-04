namespace Saleman.Model.ServiceObjects
{
    public class AuthenticationServiceObject : ServiceObjectBase<string, AuthenticationServiceObject>
    {
        public string Email { get; set; }
    }

    public class UserServiceObject : ServiceObjectBase<string, UserServiceObject>
    {
        public string Email { get; set; }
    }

    public class ResetPasswordServiceObject : ServiceObjectBase<string, ResetPasswordServiceObject>
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }

    public class TokenServiceObject : ServiceObjectBase<string, TokenServiceObject>
    {
        public string Token { get; set; }
    }
}

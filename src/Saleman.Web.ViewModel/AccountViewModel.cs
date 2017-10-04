using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Saleman.Web.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Email is invalid!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required!")]
        [Compare("Password", ErrorMessage = "Confirm password is not matched with password!")]
        public string ConfirmPassword { get; set; }
    }
    public class LoginViewModel
    {
        public string Uri { get; set; }

        [EmailAddress(ErrorMessage = "Email is invalid format!")]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }

    public class ForgotPasswordViewModel
    {

        [EmailAddress(ErrorMessage = "Email is invalid format!")]
        [Required]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required!")]
        [Compare("Password", ErrorMessage = "Confirm password is not matched with password!")]
        public string ConfirmPassword { get; set; }
    }

    public class VerifyEmailViewModel
    {
        [Required]
        [JsonProperty("id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Token is required!")]
        [JsonProperty("token")]
        public string Token { get; set; }
    }

    public class UserViewModel : ViewModelBase<string>
    {
        public string Email { get; set; }
    }
}

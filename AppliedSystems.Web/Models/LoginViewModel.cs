using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace NIPF.Web.Models
{
    public sealed class LoginViewModel
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address.")]
        [Required(ErrorMessage = "Email Address is required", AllowEmptyStrings = false)]
        [JsonProperty(PropertyName = "email", Required = Required.Always)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [JsonProperty(PropertyName = "password", Required = Required.Always)]
        public string Password { get; set; }
    }
}
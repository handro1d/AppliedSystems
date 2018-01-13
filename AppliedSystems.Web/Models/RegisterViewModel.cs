using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AppliedSystems.Web.Models
{
    public class RegisterViewModel
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address.")]
        [Required(ErrorMessage = "Email Address is required", AllowEmptyStrings = false)]
        [JsonProperty(PropertyName = "email", Required = Required.Always)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [JsonProperty(PropertyName = "password", Required = Required.Always)]
        public string Password { get; set; }

        [DisplayName("Verify Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Confirmation is required", AllowEmptyStrings = false)]
        [JsonProperty(PropertyName = "passwordConfirmation", Required = Required.Always)]
        public string PasswordConfirmation { get; set; }

        [DisplayName("First Name")]
        [MaxLength(50)]
        [Required(ErrorMessage = "First name is required", AllowEmptyStrings = false)]
        [JsonProperty(PropertyName = "firstName", Required = Required.Always)]
        public string FirstName { get; set; }

        [DisplayName("Surname")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Surname is required", AllowEmptyStrings = false)]
        [JsonProperty(PropertyName = "surname", Required = Required.Always)]
        public string Surname { get; set; }
    }
}
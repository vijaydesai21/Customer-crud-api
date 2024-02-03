using System.ComponentModel.DataAnnotations;

namespace CustomerModule.Api.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Mobile Number must contain only numeric values")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EmailId { get; set; }
    }
}
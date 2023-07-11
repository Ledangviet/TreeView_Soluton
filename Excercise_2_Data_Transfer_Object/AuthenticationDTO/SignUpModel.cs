using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Transfer_Object.AuthenticationDTO
{
    public class SignUpModel
    {
        [Required]

        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression("(^0[0-9]+$)" ,
            ErrorMessage = "Please enter valid phone number(start with 0 and all are digit)!")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

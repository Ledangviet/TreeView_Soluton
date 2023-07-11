using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Transfer_Object.AuthenticationDTO
{
    public class SignInResponseModel
    {
        [Required]
        public bool Succeeded { get; set; } = false;
        public string? Status { get; set; } 
        public TokenModel? Token { get; set; }

    }
}

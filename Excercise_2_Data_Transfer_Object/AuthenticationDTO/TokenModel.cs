using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Transfer_Object.AuthenticationDTO
{
    public class TokenModel
    {
        public string UserName { get; set; }
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set;}
    }
}

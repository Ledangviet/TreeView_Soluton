using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Transfer_Object.AuthenticationDTO
{
    public class TokenResponse
    {
        public bool Status { get; set; }
        public string? StatusMessage { get; set; }
        public string? Token { get; set; }   
    }
}

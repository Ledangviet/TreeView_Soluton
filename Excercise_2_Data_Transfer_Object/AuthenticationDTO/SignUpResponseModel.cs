using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Transfer_Object.AuthenticationDTO
{
    public class SignUpResponseModel
    {
        public bool Succeeded { get; set; } = false;
        public string? Status { get; set; }
    }
}

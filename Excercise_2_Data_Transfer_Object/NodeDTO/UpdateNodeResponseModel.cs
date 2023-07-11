using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Transfer_Object.NodeDTO
{
    public class UpdateNodeResponseModel
    {
        [Required]
        public bool Succeed { get; set; } = false;
        public string? Status { get; set; }
        public GetNodeModel? NodeModel { get; set; }
    }
}

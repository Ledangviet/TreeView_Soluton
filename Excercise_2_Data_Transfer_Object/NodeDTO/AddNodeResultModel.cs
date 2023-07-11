using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Transfer_Object.NodeDTO
{
    public class AddNodeResultModel
    {
        [Required]
        public bool Succeed { get; set; } = false;
        [Required]
        public string? Status { get; set; }
        [Required]
        public GetNodeModel? Nodes { get; set; }
    }
}

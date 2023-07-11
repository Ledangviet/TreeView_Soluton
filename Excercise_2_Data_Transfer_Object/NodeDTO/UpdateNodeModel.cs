using Excercise_2_Data_Transfer_Object.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_2_Data_Transfer_Object.NodeDTO
{
    public class UpdateNodeModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public NodeType NodeType { get; set; }
        [Required]
        public int ParrentId { get; set; }
        [Required]
        public string Owner { get; set; } = "";
        [Required]
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}

using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using Excercise_2_Data_Transfer_Object.Enums;

namespace Excercise_2_Data_Access_Layer.Data.Entities
{
    public class Node
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public NodeType NodeType { get; set; }
        [Required]
        public int ParrentId { get; set; }
        [Required]
        public string Owner { get; set; } = "";
        [Required]
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        [Required]
        public bool IsDeleted { get; set; } = false;
        
    }
}

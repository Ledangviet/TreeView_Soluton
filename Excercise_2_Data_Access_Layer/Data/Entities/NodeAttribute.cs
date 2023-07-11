using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excercise_2_Data_Access_Layer.Data.Entities
{
    public class NodeAttribute
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";      
        [Required]
        public int NodeId { get; set; }
        [ForeignKey("NodeId")]
        public Node? Node { get; set; }
        public bool IsDeleted { get; set; } = false;
        
    }
}

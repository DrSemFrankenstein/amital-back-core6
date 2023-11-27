using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimonP_amital.Models
{
    [Table("Tasks")]
    public class Tasks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("subject")]
        public string Subject { get; set; }

        [Column("IsCompleted")]
        public bool IsCompleted { get; set; }

        [Column("TargetDate")]
        public DateTime? TargetDate { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        private Users User { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend_COMP375.Models
{
    [Table("orders")]
    public partial class Order
    {
        [Key]
        [Column("OrderId")]
        public int OrderId { get; set; }

        [ForeignKey("User")]
        [Column("UserId")]
        public int? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Club")]
        [Column("ClubId")]
        public int? ClubId { get; set; }
        public Club? Club { get; set; }

        [Column("OrderNumber")]
        public int OrderNumber { get; set; }

        [Column("OrderDate")]
        public required DateOnly OrderDate { get; set; }
    }
}

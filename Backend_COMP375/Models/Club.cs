using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend_COMP375.Models
{
    [Table("clubs")]
    public partial class Club
    {
        [Key]
        public int ClubId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

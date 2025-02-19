using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend_COMP375.Models
{
    [Table("users")]
    public partial class User
    {
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend_COMP375.DTO
{
    public class ClubDTO
    {
        public int ClubId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}

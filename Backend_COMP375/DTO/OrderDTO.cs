namespace Backend_COMP375.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? ClubId { get; set; }
        public int OrderNumber { get; set; }
        public DateOnly OrderDate { get; set; }
        public string? UserName { get; set; }
        public string? ClubName { get; set; }
    }
}

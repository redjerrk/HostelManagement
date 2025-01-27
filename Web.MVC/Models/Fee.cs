namespace Web.MVC.Models
{
    public class Fee
    {
        public int Id { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? LastRentGeneratedDate { get; set; }
        public int DueAmount { get; set; }
        public int? MemberId { get; set; }
        public int? HostelId { get; set; }
        public int? RoomId { get; set; }
    }
}

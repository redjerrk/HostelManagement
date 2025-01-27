using Web.MVC.Services.HostelRepo;

namespace Web.MVC.Services.RentCollection
{
    public class VmRentCollection
    {
        public int? MemberId { get; set; }
        public string? MemberName { get; set; }
        public string? MemberPhone { get; set; }
        public int? HostelId { get; set; }
        public string? HostelName { get; set; }
        public int? RoomId { get; set; }
        public string? RoomNumber { get; set;}
        public int? RoomTypeId { get; set; }
        public string? RoomType { get; set; }
        public int? FeeId { get; set; }
        public int? RentPerMonth { get; set; }
        public string? PaymentStatus { get; set; }
        public int? DueAmount { get; set; }
        public string? LastPaymentDate { get; set; }
    }
}

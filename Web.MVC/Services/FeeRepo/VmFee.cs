namespace Web.MVC.Services.FeeRepo
{
    public class VmFee
    {
        public int Id { get; set; }
        public string PaymentDate { get; set; }
        public string LastGeneratedDate { get; set; }
        public int DueAmount { get; set; }
        public int? MemberId { get; set; }
        public string MemberName { get; set; }
        public int? HostelId { get; set; }
        public string HostelName { get; set; }
        public int? RoomId { get; set; }
        public string RoomNumber { get; set;}
    }
}

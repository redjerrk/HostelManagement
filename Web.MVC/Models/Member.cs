namespace Web.MVC.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationMembershipId { get; set; }
        public int? RoomId { get; set; }
        public int? HostelId { get; set; }
    }
}

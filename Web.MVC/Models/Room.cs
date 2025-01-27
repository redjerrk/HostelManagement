namespace Web.MVC.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Capacity { get; set; }
        public int? RoomTypeId { get; set; }
        public int? HostelId { get; set; }
    }
}

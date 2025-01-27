namespace Web.MVC.Services.RoomRepo
{
    public class VmRoom
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Capacity { get; set; }
        public int? RoomTypeId { get; set; }
        public string? Type { get; set; }
        public int? HostelId { get; set; }
        public string? HostelName { get; set; }
    }
}

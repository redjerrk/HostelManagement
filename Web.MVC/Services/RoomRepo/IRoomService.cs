namespace Web.MVC.Services.RoomRepo
{
    public interface IRoomService
    {
        bool Create(Models.Room model);
        bool Update(Models.Room model);
        Models.Room GetById(int id);
        List<Models.Room> GetAll();
        List<VmRoom> GetRoomList();
        bool Delete(int id);
        int TotalCount();
    }
}

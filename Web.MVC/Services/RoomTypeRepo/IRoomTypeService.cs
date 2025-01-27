using Web.MVC.Models;

namespace Web.MVC.Services.RoomTypeRepo
{
    public interface IRoomTypeService
    {
        bool Create(RoomType model);
        bool Update(RoomType model);
        RoomType GetById(int id);
        List<RoomType> GetAll();
        bool Delete(int id);
        int TotalCount();
    }
}

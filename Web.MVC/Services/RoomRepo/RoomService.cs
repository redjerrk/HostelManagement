using Microsoft.EntityFrameworkCore;
using Web.MVC.Data;
using Web.MVC.Models;
using Web.MVC.Services.Hostel;
using Web.MVC.Services.RoomTypeRepo;

namespace Web.MVC.Services.RoomRepo
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RoomService> _logger;
        private readonly IHostelService _hostelService;
        private readonly IRoomTypeService _roomTypeService;

        public RoomService(ApplicationDbContext appDbContext, 
            ILogger<RoomService> logger,
            IHostelService hostelService,
            IRoomTypeService roomTypeService)
        {
            _dbContext = appDbContext;
            _logger = logger;
            _hostelService = hostelService;
            _roomTypeService = roomTypeService;
        }
        public bool Create(Room model)
        {
            try
            {
                if (model != null)
                {
                    _dbContext.Rooms.Add(model);
                    var result = _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public Models.Room GetById(int id) => _dbContext.Rooms.FirstOrDefault(x => x.Id == id);

        public bool Update(Room model)
        {
            var data = _dbContext.Rooms.FirstOrDefault(n => n.Id == model.Id);

            if (data != null)
            {
                data.RoomNumber = model.RoomNumber;
                data.Capacity = model.Capacity;
                data.HostelId = model.HostelId;
                data.RoomTypeId = model.RoomTypeId;

                _dbContext.Rooms.Update(data);
                _dbContext.SaveChanges();

                return true;
            }
            return false;
        }

        public List<Room> GetAll() => _dbContext.Rooms.ToList();
        public List<VmRoom> GetRoomList()
        {
            var hostelList = _hostelService.GetAll().ToDictionary(n => n.Id);
            //var roomTypeList = _roomTypeService.GetAll().ToDictionary(n => n.Id);
            var roomTypeList = _dbContext.RoomType.AsEnumerable().ToDictionary(n => n.Id);
            var data = _dbContext.Rooms.AsEnumerable().Select(s => new VmRoom
            {
                Id = s.Id,
                Capacity = s.Capacity,
                RoomNumber = s.RoomNumber,
                RoomTypeId = s.RoomTypeId,
                Type = roomTypeList.ContainsKey((int)s.RoomTypeId) ? roomTypeList[(int)s.RoomTypeId].Type : "",
                HostelId = s.HostelId,
                HostelName = hostelList.ContainsKey((int)s.HostelId) ? hostelList[(int)s.HostelId].Name : ""
            }).ToList();
            return data;
        }

        public bool Delete(int id)
        {
            var data = GetById(id);
            if (data != null)
            {
                _dbContext.Rooms.Remove(data);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public int TotalCount()
        {
            var totalData = _dbContext.Rooms.Where(n => n.Id > 0).Count();
            return totalData;
        }
    }
}

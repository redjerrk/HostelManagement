using Microsoft.EntityFrameworkCore;
using Web.MVC.Data;
using Web.MVC.Models;

namespace Web.MVC.Services.RoomTypeRepo
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RoomTypeService> _logger;

        public RoomTypeService(ApplicationDbContext appDbContext, ILogger<RoomTypeService> logger)
        {
            _dbContext = appDbContext;
            _logger = logger;
        }
        public bool Create(RoomType model)
        {
            try
            {
                if (model != null)
                {
                    _dbContext.RoomType.Add(model);
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

        public RoomType GetById(int id) => _dbContext.RoomType.FirstOrDefault(x => x.Id == id);

        public bool Update(RoomType model)
        {
            var data = _dbContext.RoomType.FirstOrDefault(n => n.Id == model.Id);

            if (data != null)
            {
                data.Type = model.Type;
                data.Facilities = model.Facilities;
                data.RentPerMonth = model.RentPerMonth;

                _dbContext.RoomType.Update(data);
                _dbContext.SaveChanges();

                return true;
            }
            return false;
        }

        public List<RoomType> GetAll() => _dbContext.RoomType.ToList();

        public bool Delete(int id)
        {
            var data = GetById(id);
            if (data != null)
            {
                _dbContext.RoomType.Remove(data);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public int TotalCount()
        {
            var totalData = _dbContext.RoomType.Where(n => n.Id > 0).Count();
            return totalData;
        }
    }
}

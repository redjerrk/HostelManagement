using Microsoft.EntityFrameworkCore;
using Web.MVC.Data;
using Web.MVC.Services.Hostel;

namespace Web.MVC.Services.HostelRepo
{
    public class HostelService: IHostelService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<HostelService> _logger;

        public HostelService(ApplicationDbContext appDbContext, ILogger<HostelService> logger)
        {
            _dbContext = appDbContext;
            _logger = logger;
        }

        public bool Create(Models.Hostel model)
        {
            try
            {
                if (model != null)
                {
                    _dbContext.Hostels.Add(model);
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

        public Models.Hostel GetById(int id) => _dbContext.Hostels.FirstOrDefault(x => x.Id == id);

        public bool Update(Models.Hostel model)
        {
            var data = _dbContext.Hostels.FirstOrDefault(n => n.Id == model.Id);

            if (data != null)
            {
                data.Name = model.Name;
                data.Address = model.Address;
                data.LicenseNo = model.LicenseNo;
                data.OwnerName = model.OwnerName;
                data.OwnerPhoneNumber = model.OwnerPhoneNumber;
                data.OwnerEmail = model.OwnerEmail;
                data.OwnerAddress = model.OwnerAddress;
                data.OwnerNid = model.OwnerNid;
                data.OpeningTime = model.OpeningTime;
                data.ClosingTime = model.ClosingTime;
                _dbContext.Hostels.Update(data);
                _dbContext.SaveChanges();

                return true;
            }
            return false;
        }

        public List<Models.Hostel> GetAll() => _dbContext.Hostels.ToList();

        public bool Delete(int id)
        {
            var data = GetById(id);
            if (data != null)
            {
                _dbContext.Hostels.Remove(data);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public int TotalCount()
        {
            var totalData = _dbContext.Hostels.Where(n => n.Id > 0).Count();
            return totalData;
        }
    }
}

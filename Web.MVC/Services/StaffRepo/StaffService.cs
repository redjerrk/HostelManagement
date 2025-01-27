using Web.MVC.Data;
using Web.MVC.Models;
using Web.MVC.Services.Hostel;

namespace Web.MVC.Services.StaffRepo
{
    public class StaffService : IStaffService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<StaffService> _logger;
        private readonly IHostelService _hostelService;


        public StaffService(ApplicationDbContext appDbContext, ILogger<StaffService> logger, IHostelService hostelService)
        {
            _dbContext = appDbContext;
            _logger = logger;
            _hostelService = hostelService;
        }

        public bool Create(VmStaff model)
        {
            try
            {
                if (model != null)
                {
                    var staff = new Staff()
                    {
                        Name = model.Name,
                        Gender = model.Gender,
                        DateOfBirth = DateTime.Parse(model.DateOfBirth),
                        Address = model.Address,
                        Phone = model.Phone,
                        Email = model.Email,
                        Position = model.Position,
                        Salary = model.Salary,
                        HostelId = model.HostelId
                    };
                    _dbContext.Staffs.Add(staff);
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

        public VmStaff GetById(int id)
        {
            var staffInfo = _dbContext.Staffs.FirstOrDefault(x => x.Id == id);
            var result = new VmStaff();
            if (staffInfo != null)
            {
                result.Name = staffInfo.Name;
                result.Gender = staffInfo.Gender;
                result.DateOfBirth = staffInfo.DateOfBirth?.ToString();
                result.Address = staffInfo.Address;
                result.Phone = staffInfo.Phone;
                result.Email = staffInfo.Email;
                result.Position = staffInfo.Position;
                result.Salary = staffInfo.Salary;
                result.HostelId = staffInfo.HostelId;
            }
            return result;
        }

        public bool Update(VmStaff model)
        {
            var data = _dbContext.Staffs.FirstOrDefault(n => n.Id == model.Id);

            if (data != null)
            {
                data.Name = model.Name;
                data.Gender = model.Gender;
                data.DateOfBirth = DateTime.Parse(model.DateOfBirth);
                data.Address = model.Address;
                data.Phone = model.Phone;
                data.Email = model.Email;
                data.Position = model.Position;
                data.Salary = model.Salary;
                data.HostelId = model.HostelId;

                _dbContext.Staffs.Update(data);
                _dbContext.SaveChanges();

                return true;
            }
            return false;
        }

        public List<VmStaff> GetAll()
        {
            var hostelList = _hostelService.GetAll().ToDictionary(n => n.Id);
            var data = _dbContext.Staffs.AsEnumerable().Select(s => new VmStaff
            {
                Id = s.Id,
                Name = s.Name,
                Gender = s.Gender,
                DateOfBirth = s.DateOfBirth.ToString(),
                Address = s.Address,
                Phone = s.Phone,
                Email = s.Email,
                Position = s.Position,
                Salary = s.Salary,
                HostelId = s.HostelId,
                HostelName = hostelList.ContainsKey((int)s.HostelId) ? hostelList[(int)s.HostelId].Name : ""
        }).ToList();
            return data;
        }

        public bool Delete(int id)
        {
            var data = _dbContext.Staffs.FirstOrDefault(n => n.Id == id);
            if (data != null)
            {
                _dbContext.Staffs.Remove(data);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public int TotalCount()
        {
            var totalData = _dbContext.Staffs.Where(n => n.Id > 0).Count();
            return totalData;
        }
    }
}

using Web.MVC.Data;
using Web.MVC.Models;
using Web.MVC.Services.FeeRepo;
using Web.MVC.Services.Hostel;
using Web.MVC.Services.RoomRepo;

namespace Web.MVC.Services.MemberRepo
{
    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<MemberService> _logger;
        private readonly IHostelService _hostelService;
        private readonly IRoomService _roomService;
        private readonly IFeeService _feeService;

        public MemberService(ApplicationDbContext appDbContext, 
            ILogger<MemberService> logger,
            IHostelService hostelService,
            IRoomService roomService)
        {
            _dbContext = appDbContext;
            _logger = logger;
            _roomService = roomService;
            _hostelService = hostelService;
        }

        public bool Create(VmMember model)
        {
            try
            {
                if (model != null)
                {
                    var member = new Member()
                    {
                        Name = model.Name,
                        Gender = model.Gender,
                        DateOfBirth = DateTime.Parse(model.DateOfBirth),
                        Address = model.Address,
                        Phone = model.Phone,
                        Email = model.Email,
                        OrganizationName = model.OrganizationName,
                        OrganizationMembershipId = model.OrganizationMembershipId,
                        RoomId = model.RoomId,
                        HostelId = model.HostelId
                    };
                    _dbContext.Members.Add(member);
                    var result = _dbContext.SaveChanges();

                    var feeInfo = new Fee();
                    feeInfo.MemberId = member.Id;
                    feeInfo.HostelId = member.HostelId;
                    feeInfo.RoomId = member.RoomId;
                    feeInfo.DueAmount = 0;
                    _dbContext.Fees.Add(feeInfo);
                    _dbContext.SaveChanges();
                    
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

        public VmMember GetById(int id)
        {
            var memberInfo = _dbContext.Members.FirstOrDefault(x => x.Id == id);
            var result = new VmMember();
            if (memberInfo != null)
            {
                result.Name = memberInfo.Name;
                result.Gender = memberInfo.Gender;
                result.DateOfBirth = memberInfo.DateOfBirth?.ToString();
                result.Address = memberInfo.Address;
                result.Phone = memberInfo.Phone;
                result.Email = memberInfo.Email;
                result.OrganizationName = memberInfo.OrganizationName;
                result.OrganizationMembershipId = memberInfo.OrganizationMembershipId;
                result.RoomId = memberInfo.RoomId;
                result.HostelId = memberInfo.HostelId;
            }
            return result;
        }

        public bool Update(VmMember model)
        {
            var data = _dbContext.Members.FirstOrDefault(n => n.Id == model.Id);

            if (data != null)
            {
                data.Name = model.Name; 
                data.Gender = model.Gender; 
                data.DateOfBirth = DateTime.Parse(model.DateOfBirth);
                data.Address = model.Address;
                data.Phone = model.Phone;
                data.Email = model.Email;
                data.OrganizationName = model.OrganizationName;
                data.OrganizationMembershipId = model.OrganizationMembershipId;
                data.RoomId = model.RoomId;
                data.HostelId = model.HostelId;

                _dbContext.Members.Update(data);
                _dbContext.SaveChanges();

                return true;
            }
            return false;
        }

        public List<VmMember> GetAll()
        {
            var hostelList = _hostelService.GetAll().ToDictionary(n => n.Id);
            var roomList = _roomService.GetAll().ToDictionary(n => n.Id);
            var data = _dbContext.Members.AsEnumerable().Select(s => new VmMember
            {
                Id = s.Id,
                Name = s.Name,
                Gender = s.Gender,
                DateOfBirth = s.DateOfBirth.ToString(),
                Address = s.Address,
                Phone = s.Phone,
                Email = s.Email,
                OrganizationName = s.OrganizationName,
                OrganizationMembershipId = s.OrganizationMembershipId,
                RoomId = s.RoomId,
                RoomNumber = roomList.ContainsKey((int)s.RoomId) ? roomList[(int)s.RoomId].RoomNumber : "",
                HostelId = s.HostelId,
                HostelName = hostelList.ContainsKey((int)s.HostelId) ? hostelList[(int)s.HostelId].Name : ""
            }).ToList();
            return data;
        }

        public bool Delete(int id)
        {
            var data = _dbContext.Members.FirstOrDefault(n => n.Id == id);
            if (data != null)
            {
                _dbContext.Members.Remove(data);
                _dbContext.SaveChanges();
                var feeData = _dbContext.Fees.FirstOrDefault(fe => fe.MemberId == id);
                if(feeData != null)
                {
                    _dbContext.Fees.Remove(feeData);
                    _dbContext.SaveChanges();
                }
                return true;
            }
            return false;
        }
        public int TotalCount()
        {
            var totalData = _dbContext.Members.Where(n => n.Id > 0).Count();
            return totalData;
        }
    }
}

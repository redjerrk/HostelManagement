using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Web.MVC.Data;
using Web.MVC.Models;

namespace Web.MVC.Services.RentCollection
{
    public class RentCollectionService : IRentCollectionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RentCollectionService> _logger;
        public RentCollectionService(ApplicationDbContext dbContext,
            ILogger<RentCollectionService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public List<VmRentCollection> GetAll()
        {
            var hostelList = _dbContext.Hostels.AsEnumerable().ToDictionary(n => n.Id);
            var query = from member in _dbContext.Members
                          join hostel in _dbContext.Hostels on member.HostelId equals hostel.Id
                          join fee in _dbContext.Fees on member.Id equals fee.MemberId
                          join room in _dbContext.Rooms on member.RoomId equals room.Id
                          join roomType in _dbContext.RoomType on room.RoomTypeId equals roomType.Id
                          select new VmRentCollection
                          {
                              MemberId = member.Id,
                              MemberName = member.Name,
                              MemberPhone = member.Phone,
                              RoomId = member.RoomId,
                              RoomNumber = room.RoomNumber,
                              HostelId = member.HostelId,
                              HostelName = hostel.Name,
                              FeeId = fee.Id,
                              DueAmount = fee.DueAmount,
                              LastPaymentDate = fee.PaymentDate.ToString(),
                              RoomTypeId = roomType.Id,
                              RoomType = roomType.Type,
                              RentPerMonth = roomType.RentPerMonth,
                              //PaymentStatus = PayStatus(fee.DueAmount, roomType.RentPerMonth)
                          };
            var _result = query.AsEnumerable().Select(s => new VmRentCollection
                        {
                            MemberId = s.MemberId,
                            MemberName = s.MemberName,
                            MemberPhone = s.MemberPhone,
                            RoomId = s.RoomId,
                            RoomNumber = s.RoomNumber,
                            HostelId = s.HostelId,
                            HostelName = s.HostelName,
                            FeeId = s.FeeId,
                            DueAmount = s.DueAmount,
                            LastPaymentDate = s.LastPaymentDate,
                            RoomTypeId = s.RoomTypeId,
                            RoomType = s.RoomType,
                            RentPerMonth = s.RentPerMonth,
                            PaymentStatus = PayStatus(s.DueAmount, s.RentPerMonth)
                        }).OrderBy(n => n.MemberName).ToList();
            return _result;
        }

        public string PayStatus(int? dueAmount, int? RentPerMonth)
        {
            if(dueAmount == 0)
                return "PAID";
            if (dueAmount < 0)
                return "ADVANCED";
            if (dueAmount == RentPerMonth)
                return "UNPAID";
            if (dueAmount > RentPerMonth)
                return "DUE";
            if (dueAmount > 0 && dueAmount < RentPerMonth)
                return "PARTIAL DUE";
            return "";
        }
    }
}

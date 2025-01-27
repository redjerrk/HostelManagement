using Web.MVC.Data;
using Web.MVC.Models;

namespace Web.MVC.Services.FeeRepo
{
    public class FeeService : IFeeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<FeeService> _logger;

        public FeeService(ApplicationDbContext appDbContext, ILogger<FeeService> logger)
        {
            _dbContext = appDbContext;
            _logger = logger;
        }

        public bool Create(VmFee model)
        {
            try
            {
                if (model != null)
                {
                    var fee = new Fee()
                    {
                        PaymentDate = DateTime.Parse(model.PaymentDate),
                        LastRentGeneratedDate = DateTime.Parse(model.LastGeneratedDate),
                        DueAmount = model.DueAmount,
                        MemberId = model.MemberId,
                        RoomId = model.RoomId,
                        HostelId = model.HostelId
                    };
                    _dbContext.Fees.Add(fee);
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

        public VmFee GetById(int id)
        {
            var feeInfo =  _dbContext.Fees.FirstOrDefault(x => x.Id == id);
            var result = new VmFee();
            if(feeInfo != null)
            {
                result.PaymentDate = feeInfo.PaymentDate?.ToString();
                result.LastGeneratedDate = feeInfo.LastRentGeneratedDate?.ToString();
                result.DueAmount = feeInfo.DueAmount;
                result.HostelId = feeInfo.HostelId;
                result.RoomId = feeInfo.RoomId;
                result.MemberId = feeInfo.MemberId;
            }  
            return result;
        } 

        public bool Update(VmFee model)
        {
            var data = _dbContext.Fees.FirstOrDefault(n => n.Id == model.Id);

            if (data != null)
            {
                data.PaymentDate = DateTime.Parse(model.PaymentDate);
                data.LastRentGeneratedDate = DateTime.Parse(model.LastGeneratedDate);
                data.DueAmount = model.DueAmount; 
                data.HostelId = model.HostelId; 
                data.RoomId = model.RoomId;   
                data.MemberId = model.MemberId;

                _dbContext.Fees.Update(data);
                _dbContext.SaveChanges();

                return true;
            }
            return false;
        }

        public List<VmFee> GetAll()
        {
            var data = _dbContext.Fees.AsEnumerable().Select(s => new VmFee
            {
                PaymentDate = s.PaymentDate?.ToString(),
                LastGeneratedDate = s.LastRentGeneratedDate?.ToString(),
                DueAmount = s.DueAmount,
                HostelId = s.HostelId,
                RoomId = s.RoomId,
                MemberId = s.MemberId
            }).ToList();
            return data;
        }

        public bool Delete(int id)
        {
            var data = _dbContext.Fees.FirstOrDefault(n => n.Id == id);
            if (data != null)
            {
                _dbContext.Fees.Remove(data);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public VmMakePayment GetByFeeId(int id)
        {
            var feeInfo = _dbContext.Fees.FirstOrDefault(x => x.Id == id);
            var result = new VmMakePayment();
            if (feeInfo != null)
            {
                result.Id = feeInfo.Id;
            }
            return result;
        }

        public bool MakePayment(VmMakePayment model)
        {
            var data = _dbContext.Fees.FirstOrDefault(n => n.Id == model.Id);
            if (data != null)
            {
                data.DueAmount = data.DueAmount - (int)model.PayAmount;
                data.PaymentDate = DateTime.Now;
                _dbContext.Fees.Update(data);
                _dbContext.SaveChanges();
            }
            return true;
        }

        public bool GenerateRent()
        {
            try
            {
                var query = from member in _dbContext.Members
                            join fee in _dbContext.Fees on member.Id equals fee.MemberId
                            join room in _dbContext.Rooms on member.RoomId equals room.Id
                            join roomType in _dbContext.RoomType on room.RoomTypeId equals roomType.Id
                            select new
                            {
                                MemberId = member.Id,
                                FeeId = fee.Id,
                                RentPerMonth = roomType.RentPerMonth
                            };
                var rentPerMonthList = query.AsEnumerable().ToDictionary(n => n.FeeId);
                var currentDate = DateTime.Now;
                var rentNeedToGenerate = _dbContext.Fees.Where(f => f.LastRentGeneratedDate.Value.Month != currentDate.Month ||
                                                                    f.LastRentGeneratedDate.Value.Year != currentDate.Year).ToList();
                rentNeedToGenerate.ForEach(x => {
                    x.DueAmount = x.DueAmount + (rentPerMonthList.ContainsKey(x.Id) ? rentPerMonthList[x.Id].RentPerMonth : 0);
                    x.LastRentGeneratedDate = currentDate;
                });
                _dbContext.Fees.UpdateRange(rentNeedToGenerate);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
        public int TotalCount()
        {
            var totalDue = _dbContext.Fees.AsEnumerable().Where(n => n.DueAmount > 0).Sum(s => s.DueAmount);
            return totalDue;
        }
    }
}

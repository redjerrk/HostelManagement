namespace Web.MVC.Services.FeeRepo
{
    public interface IFeeService
    {
        bool Create(VmFee model);
        bool Update(VmFee model);
        bool MakePayment(VmMakePayment model);
        VmMakePayment GetByFeeId(int id);
        VmFee GetById(int id);
        List<VmFee> GetAll();
        bool Delete(int id);
        bool GenerateRent();
        int TotalCount();
    }
}

namespace Web.MVC.Services.StaffRepo
{
    public interface IStaffService
    {
        bool Create(VmStaff model);
        bool Update(VmStaff model);
        VmStaff GetById(int id);
        List<VmStaff> GetAll();
        bool Delete(int id);
        int TotalCount();
    }
}

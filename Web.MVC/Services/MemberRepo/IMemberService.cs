namespace Web.MVC.Services.MemberRepo
{
    public interface IMemberService
    {
        bool Create(VmMember model);
        bool Update(VmMember model);
        VmMember GetById(int id);
        List<VmMember> GetAll();
        bool Delete(int id);
        int TotalCount();
    }
}

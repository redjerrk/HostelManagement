namespace Web.MVC.Services.Hostel;

public interface IHostelService
{
    bool Create(Models.Hostel model);
    bool Update(Models.Hostel model);
    Models.Hostel GetById(int id);
    List<Models.Hostel> GetAll();
    bool Delete(int id);
    int TotalCount();
}

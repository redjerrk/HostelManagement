using Microsoft.AspNetCore.Mvc;
using Web.MVC.Services.RentCollection;

namespace Web.MVC.Controllers
{
    public class RentCollectionController : Controller
    {
        private readonly IRentCollectionService _service;

        public RentCollectionController(IRentCollectionService service) {
            _service = service;
        }
        public IActionResult Index()
        {
            return View(_service.GetAll());
        }
    }
}

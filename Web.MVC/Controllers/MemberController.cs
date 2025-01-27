using Microsoft.AspNetCore.Mvc;
using Web.MVC.Services.Hostel;
using Web.MVC.Services.MemberRepo;
using Web.MVC.Services.RoomRepo;
using Web.MVC.Utility;

namespace Web.MVC.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _service;
        private readonly IHostelService _hostelService;
        private readonly IRoomService _roomService;
        public MemberController(IMemberService service,
            IHostelService hostelService,
            IRoomService roomService)
        {
            _service = service;
            _hostelService = hostelService;
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult Index() => View(_service.GetAll());

        [HttpGet]
        public IActionResult Create()
        {
            var hostels = _hostelService.GetAll();
            ViewBag.Hostels = hostels;
            var rooms = _roomService.GetAll();
            ViewBag.Rooms = rooms;
            return View();
        }

        [HttpPost]
        public IActionResult Create(VmMember model)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                result = _service.Create(model);
            }

            if (result)
            {
                TempData["isSucceed"] = ValidationMessages.CREATE_SUCCESS;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["isFailed"] = ValidationMessages.CREATE_FAILED;
                return RedirectToAction("Create");
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var hostels = _hostelService.GetAll();
            ViewBag.Hostels = hostels;
            var rooms = _roomService.GetAll();
            ViewBag.Rooms = rooms;
            return View(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(VmMember model)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                result = _service.Update(model);
            }

            if (result)
            {
                TempData["isSucceed"] = ValidationMessages.UPDATE_SUCCESS;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["isFailed"] = ValidationMessages.UPDATE_FAILED;
                return RedirectToAction("Update");
            }
        }
        public IActionResult Get(int id)
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            bool result = false;
            if (
                ModelState.IsValid)
            {
                if (id > 0)
                {
                    result = _service.Delete(id);
                }
            }
            TempData["isDeleted"] = result;

            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Web.MVC.Models;
using Web.MVC.Services.Hostel;
using Web.MVC.Services.RoomRepo;
using Web.MVC.Services.RoomTypeRepo;
using Web.MVC.Utility;

namespace Web.MVC.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _service;
        private readonly IHostelService _hostelService;
        private readonly IRoomTypeService _roomTypeService;
        public RoomController(IRoomService service,
            IHostelService hostelService,
            IRoomTypeService roomTypeService)
        {
            _service = service;
            _hostelService = hostelService;
            _roomTypeService = roomTypeService;
        }

        [HttpGet]
        //public IActionResult Index() => View(_service.GetAll());
        public IActionResult Index() => View(_service.GetRoomList());

        [HttpGet]
        public IActionResult Create()
        {
            var hostels = _hostelService.GetAll();
            ViewBag.Hostels = hostels;
            var roomTypes = _roomTypeService.GetAll();
            ViewBag.RoomTypes = roomTypes;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Room model)
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
            var roomTypes = _roomTypeService.GetAll();
            ViewBag.RoomTypes = roomTypes;
            return View(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(Room model)
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
            if (ModelState.IsValid)
            {
                if (id > 0)
                {
                    result = _service.Delete(id);
                }
            }

            if (result)
                TempData["isSucceed"] = ValidationMessages.DELETE_SUCCESS;
            else
                TempData["isFailed"] = ValidationMessages.DELETE_FAILED;

            return RedirectToAction("Index");
        }
    }
}

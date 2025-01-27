using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.MVC.Models;
using Web.MVC.Services.FeeRepo;
using Web.MVC.Services.Hostel;
using Web.MVC.Services.MemberRepo;
using Web.MVC.Services.RoomRepo;
using Web.MVC.Services.RoomTypeRepo;
using Web.MVC.Services.StaffRepo;

namespace Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHostelService _hostelService;
        private readonly IStaffService _staffService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IRoomService _roomService;
        private readonly IMemberService _memberService;
        private readonly IFeeService _feeService;

        public HomeController(ILogger<HomeController> logger,
            IHostelService hostelService,
            IStaffService staffService,
            IRoomTypeService roomTypeService,
            IRoomService roomService,
            IMemberService memberService,
            IFeeService feeService)
        {
            _logger = logger;
            _hostelService = hostelService;
            _staffService = staffService;
            _roomTypeService = roomTypeService;
            _roomService = roomService;
            _memberService = memberService;
            _feeService = feeService;
        }

        public IActionResult Index()
        {
            ViewBag.TotalHostel = _hostelService.TotalCount();
            ViewBag.TotalStaff = _staffService.TotalCount();
            ViewBag.TotalRoomType = _roomTypeService.TotalCount();
            ViewBag.TotalRoom = _roomService.TotalCount();
            ViewBag.TotalMember = _memberService.TotalCount();
            ViewBag.TotalDue = _feeService.TotalCount();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
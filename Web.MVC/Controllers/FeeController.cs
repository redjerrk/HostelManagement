using Microsoft.AspNetCore.Mvc;
using Web.MVC.Services.FeeRepo;
using Web.MVC.Utility;

namespace Web.MVC.Controllers;

public class FeeController : Controller
{
    private readonly IFeeService _service;
    public FeeController(IFeeService service) => _service = service;

    [HttpGet]
    public IActionResult Index()
    {
        return View(_service.GetAll());
    }

    [HttpGet]
    public IActionResult MakePayment(int id)
    {
        return View(_service.GetByFeeId(id));
    }
    [HttpPost]
    public IActionResult MakePayment(VmMakePayment model)
    {
        bool result = false;
        if (ModelState.IsValid)
        {
            result = _service.MakePayment(model);
        }

        if (result)
        {
            TempData["isSucceed"] = ValidationMessages.PAYMENT_SUCCESS;
            return RedirectToAction("Index", "RentCollection");
        }
        else
        {
            TempData["isFailed"] = ValidationMessages.PAYMENT_FAILED;
            return RedirectToAction("MakePayment");
        }
    }

    [HttpGet]
    public IActionResult RentGenerate() 
    {
        var result = _service.GenerateRent();
        if(result)
        {
            TempData["isSucceed"] = ValidationMessages.RENT_GENERATE_SUCCESS;
        }
        else
        {
            TempData["isFailed"] = ValidationMessages.RENT_GENERATE_FAILED;
        }
        return RedirectToAction("Index", "RentCollection");
    }
}

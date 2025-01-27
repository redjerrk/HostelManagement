using Microsoft.AspNetCore.Mvc;
using Web.MVC.Models;
using Web.MVC.Services.Hostel;
using Web.MVC.Utility;

namespace Web.MVC.Controllers;

public class HostelController : Controller
{
    private readonly IHostelService _service;
    public HostelController(IHostelService service) => _service = service;

    [HttpGet]
    public IActionResult Index()
    {
        return View(_service.GetAll());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Hostel model)
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
        return View(_service.GetById(id));
    }

    [HttpPost]
    public IActionResult Update(Hostel model)
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

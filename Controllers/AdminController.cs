using AspNetCoreHero.ToastNotification.Abstractions;
using BlogApp.DAL.Contracts;
using BlogApp.ViewModels.BlogViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;
[Authorize]

public class AdminController : Controller
{
    private readonly IAdmin _admin;
    private readonly INotyfService _notyfService;

    public AdminController(IAdmin admin, INotyfService notyfService)
    {
        this._admin = admin;
        this._notyfService = notyfService;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var allPosts = await _admin.GetAll();
        return View(allPosts);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateBlogViewModel model)
    {
        await _admin.Create(model);
        return RedirectToAction("Index", "Admin");
    }
    [HttpGet]
    public IActionResult Edit()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(EditBlogViewModel model)
    {
        var result = await _admin.Update(model);
        if (result == true)
        {
            _notyfService.Success("Edited Succesfully!!!!");
        }
        else
        {
            _notyfService.Warning("An error Occured");
        }
        return View();
    }
    [HttpGet]
    public IActionResult Delete(Guid id)
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteBlogViewModel model)
    {
        var result = await _admin.Delete(model.Id);
        if (result == true)
        {
            _notyfService.Success("Deleted Succesfully");
        }
        else
        {
            _notyfService.Warning("An error Occured");
        }
        return View();
    }
}

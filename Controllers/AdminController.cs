using AspNetCoreHero.ToastNotification.Abstractions;
using BlogApplication.DAL.Contracts;
using BlogApplication.ViewModels.BlogViewModel;
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

    public async Task<IActionResult> Details(Guid Id)
    {
        var blog = await _admin.GetById(Id);
        return View(blog);
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
    public async Task<IActionResult> Edit(Guid Id)
    {
        var blog = await _admin.GetById(Id);
        if (blog == null)
        {
            _notyfService.Error("Null");
            return View();
        }
        else{
      var editBlog = new EditBlogViewModel(){
            ID = blog.Id,
            Name = blog.Name,
            Body =blog.Body 
      };
        return View(editBlog);
        }
      
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
        return View("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var blog = await _admin.GetById(id);
        var deleteblog = new DeleteBlogViewModel(){
            Id= blog!.Id,
            Name = blog.Name,
        };
        return View(deleteblog);
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
        return View("Index");
    }
}

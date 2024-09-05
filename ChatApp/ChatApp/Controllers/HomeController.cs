using ChatApp.DAL;
using ChatApp.Models;
using ChatApp.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public HomeController(AppDbContext context,
        UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        HomeVM homeVM = new HomeVM
        {
            Users = _context.Users.Where(u => u.UserName != User.Identity.Name).Include(u => u.Image),
            CurrentUser = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name)
        };
        //var test = homeVM.CurrentUser.Image.ImageUrl;
        return View(homeVM);
    }

    public IActionResult Privacy()
    {
        return View();
    }


}

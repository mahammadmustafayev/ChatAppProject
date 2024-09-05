using ChatApp.DAL;
using ChatApp.Models;
using ChatApp.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IWebHostEnvironment _env;
    private readonly AppDbContext _context;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager,
                             IWebHostEnvironment env,
                             AppDbContext context,
                             SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _env = env;
        _context = context;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        AppUser user = _context.Users.SingleOrDefault(u => u.UserName == loginVM.UserName);
        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Invalid Credentials");
            return View();
        }
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        AppUser user = new AppUser
        {
            FullName = registerVM.FullName,
            UserName = registerVM.UserName
        };
        await _userManager.CreateAsync(user, registerVM.Password);
        string fileName = registerVM.UserName + registerVM.Image.FileName;
        using (FileStream fs = new FileStream(Path.Combine(_env.WebRootPath, "img", fileName), FileMode.Create))
        {
            registerVM.Image.CopyTo(fs);
        }
        UserImage userImage = new UserImage
        {
            AppUser = user,
            ImageUrl = fileName
        };
        await _context.UserImages.AddAsync(userImage);
        await _context.SaveChangesAsync();
        return RedirectToAction("Login", "Account");
    }
    public async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}

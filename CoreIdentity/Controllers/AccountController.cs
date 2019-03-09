using CoreIdentity.Data;
using CoreIdentity.Models.IdentityModels;
using CoreIdentity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityCore.Controllers
{
    public class AccountController : Controller
    {
        // Manager ve Context nesnelerine ihitiyacımız var onları constructor içerisinde bir kere newliyoruz. 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<ApplicationRole> _roleManager;

        //Dependency Injection
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _roleManager = roleManager;

            var roleNames = Enum.GetNames(typeof(IdentityRoles));
            foreach (var roleName in roleNames)
            {
                if (!_roleManager.RoleExistsAsync(roleName).Result)
                {
                    var role = new ApplicationRole()
                    {
                        Name=roleName,
                        Description=""
                    };
                    var task = _roleManager.CreateAsync(role).Result;
                    Task.Run(()=> task);
                }
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();  // Sadece Register view ekranını göstermek için.
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) // Viewdan buraya formum post işlemi ile bir model gelecek.
        {
            if (!ModelState.IsValid)    // Eger model validation işlemlerinden geçemezse modeli olduğu gibi view a gonderecek.
            {
                return View(model);
            }
            // Geçerse de modelden gelen bilgileri ApplicationUser içerisindeki propertylere atıyoruz ve yeni bir kullanıcı ekliyoruz.
            var user = new ApplicationUser()
            {
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password); // Create metodu bir user bir de password istiyor. User ı modelden gelen verilerle oluştuduk ve parametre olarak onu verdik. Password u de yine model içerisinden verdik. Böylece yeni bir kullanıcı oluşturmuş olduk.

            if (result.Succeeded) // Eğer kayıt başarılı ise
            {
                return RedirectToAction(nameof(Login)); // Giriş sayfasına yonlendirecek.
            }
            else // Değilse hata mesajı verecek.
            {
                var errMsg = "";
                foreach (var identityError in result.Errors)
                {
                    errMsg += identityError.Description;
                }
                ModelState.AddModelError(String.Empty, errMsg); // Mesajı modelState içerisine ekleyecek. Mesaj summaryde gözükecek.
                return View(model); // Ve modeli olduğu gibi geri dondurecek.
            }
        }

        public IActionResult Login()
        {
            return View(); // Sadece Login view ekranını göstermek için.
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)    // Viewdan bir model gelecek.
        {
            if (!ModelState.IsValid)    // Eger model validation işlemlerinden geçemezse modeli olduğu gibi view a gonderecek.
            {
                return View(model);
            }

            // Giriş işlemi yapacağımız için signinmanager kullanıyoruz.
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
            if (result.Succeeded)   // Giriş başarılı ise
            {
                return RedirectToAction("Index", "Home"); // Home controllerdaki index sayfasına gönder.
            }
            ModelState.AddModelError(String.Empty, "Kullanıcı adı veya şifre hatalı"); // Başarılı değilse hata mesajı ver 
            return View(model); // Ve modeli olduğu gibi iew a gonder.
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
    public enum IdentityRoles
    {
        Admin,
        User
    }
}
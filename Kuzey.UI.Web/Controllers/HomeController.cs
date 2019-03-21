using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kuzey.UI.Web.Models;
using Kuzey.BLL.Repository.Abstracts;
using Kuzey.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Kuzey.BLL.Account;

namespace Kuzey.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        // DependencyInjection işlemleri
        private readonly IRepository<Category, int> _categoryRepo;
        private readonly IRepository<Product, string> _productRepo;
        private readonly MembershipTools _membershipTools;
        public HomeController(IRepository<Category,int> categRepo,IRepository<Product, string> productRepo, MembershipTools membershipTools)
        {
            _categoryRepo = categRepo;
            _productRepo = productRepo;
            _membershipTools = membershipTools;
        }
        public IActionResult Index()
        {
            // Category ve product ı buradan ekledik.
            if(!_categoryRepo.GetAll().Any())
            {
                _categoryRepo.Insert(new Category() {
                    CategoryName="Beverages"
                });
                _categoryRepo.Insert(new Category()
                {
                    CategoryName = "Condiments"
                });
            }
            if (!_productRepo.GetAll().Any())   // // Incluede değişikliği Queryable iken GetAll olarak değiştirdik
            {
                var catId = _categoryRepo.GetAll().FirstOrDefault().Id; // Incluede değişikliği Queryable iken GetAll olarak değiştirdik
                _productRepo.Insert(new Product()
                {
                    CategoryId=catId,
                    ProductName="Chai",
                    UnitPrice=18.5m
                });
                _productRepo.Insert(new Product()
                {
                    CategoryId = catId,
                    ProductName = "Chang",
                    UnitPrice = 20
                });
            }

            //var data = _productRepo.GetAll("Category"); Incluede değişikliği
            var data = _productRepo.GetAll().Include(x => x.Category).ToList(); // View kısmında categoryname i kullanacağımız için burada ıncluede işlemini gerçekleştirdik. Çünkü core da lazy loading yok.

            foreach (var product in data)
            {
                product.UnitPrice *= 1.05m;
                _productRepo.Update(product);
            }
            return View(data);
        }

        public IActionResult About()
        {
            // MembershipTools içerisindeki nesneleri burada çağırdık.
            var userManager = _membershipTools.UserManager;
            var signInManager = _membershipTools.SignInManager;
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

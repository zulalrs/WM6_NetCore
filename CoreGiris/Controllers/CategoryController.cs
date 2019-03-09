using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGiris.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreGiris.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            // Mevcut kategorilerimizi listeleme
            var db = new MyContext();   // Database imize ulaştık.

            var data = db.Categories
                .Include(x => x.Products)   // Core da Lazy Loding olmadığı için burada böyle birşey yazdık. İhtiyac oldugunda navigation propertyler üzerinden products tablosunuda kullanacak.
                //.ThenInclude(x=>x.Suppliers)
                .OrderBy(x => x.CategoryName).ToList();
            return View(data);  // View kısmına datamızı sorguya uygun şekilde gönderdik.
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();  // Sadece ekranı gorüntüleyeceği için hrgangi birşey gondermiyoruz.
        }
        [HttpPost]
        public IActionResult Add(Category model)    // Viewdan bize Category tipinde bir model gelecek.
        {
            if (!ModelState.IsValid)    // Modelimiz validation işlemlerinden geçemediyse
            {
                return View(model);     // Modeli oldugu gibi geri gonderecegiz.
            }
            // Geeçtiyse
            var db = new MyContext(); // Databaseimize erişiyoruz ve viewdan gelen modeli categories tablosuna ekliyoruz.
            db.Categories.Add(new Category()
            {
                CategoryName = model.CategoryName
            });
            db.SaveChanges(); // Değişiklikleri kaydediyoruz.
            return RedirectToAction(nameof(Index)); // Index view ına yonlendirme. Bir başka yazımı RedirectToAction("Index")
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            var db = new MyContext();   // Database e bağlandık.

            var category = db.Categories.Include(x => x.Products).FirstOrDefault(x => x.Id == id); // Gelen id ye göre kategoriyi bulmak istiyoruz fakat seçilen kategori altında ürün olup olmadığının kontrolu için product tablosuna da ihtiyac olacak lazy loading olmadığı için de burada yine include kullandık.

            if (category == null)   // Eger id si gelen id ye eşit kategori yoksa hata mesajı verecek ve Index sayfasına geri yonlendirilecek. 
            {
                TempData["Message"] = "Silinecek kategori bulunamadı";
                return RedirectToAction("Index");
            }

            if (category.Products.Count > 0) // Eger altında ürünler varsa bu kategorinin silinmesini istemiyoruz onun için burada bir kontrol yaptık.
            {
                TempData["Message"] = $"{category.CategoryName} isimli kategoriye bağlı ürün olduğundan silemezsiniz";
                return RedirectToAction("Index");
            }
            // Tüm koşullardan geçtiğinde Remove ile kategoriyi siliyoruz ve değişiklikleri kaydediyoruz.
            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["Message"] = $"{category.CategoryName} isimli silinmiştir";

            return RedirectToAction(nameof(Index)); // Silme işlemi başarılıysa tekrar Index sayfasına yonlendirecek.
        }

        [HttpGet]
        public IActionResult Edit(int id = 0)
        {
            var db = new MyContext();

            var category = db.Categories.FirstOrDefault(x => x.Id == id);   // Product ile ilgil bir işlemimi olmadığı için burada Include  kullanmadık. Sadece gelen id den kategoriyi bulduk.

            if (category == null)   // Category null gelirse hata mesajı verecek.
            {
                TempData["Message"] = "Kategori bulunamadı";
                return RedirectToAction("Index");
            }

            return View(category);  // Değilsede buldugumuz kategori nesnesini View e gondereceğiz böylelikle bu kategoriyle ilgili bilgiler oradaki form içerisindeki alanlarda gözükecek.
        }

        [HttpPost]
        public IActionResult Edit(Category model)   // Post işlemi ile view dan bir Category tipinde model gelecek.
        {
            if (!ModelState.IsValid) // Model Valid değilse olduğu gibi View a geri gönderilecek.
            {
                return View(model);
            }
            // Model valid ise 
            var db = new MyContext(); // Database a bağlandık.

            var category = db.Categories.FirstOrDefault(x => x.Id == model.Id); // Kategoriyi Id sinden bulduk.
            if (category == null)   // Null gelirse hata mesajı verilecek.
            {
                TempData["Message"] = "Kategori bulunamadı";
                return RedirectToAction("Index");
            }
            // Null gelmezse
            category.CategoryName = model.CategoryName; // Değişiklikler atanacak
            db.SaveChanges(); // Ve kaydedilecek.
            TempData["Message"] = "Kategori Güncelleme İşlemi Başarılı";

            return RedirectToAction("Index");
        }
    }
}
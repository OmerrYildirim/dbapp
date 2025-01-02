using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dbapp.Models;

namespace dbapp.Controllers;

public class HomeController : Controller {

    [HttpGet]
    public IActionResult Login() {
        
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Giriş işlemleri
            // Örneğin: Kullanıcı doğrulama
        }

        return View(model);
    }
    
    [HttpGet]
    public IActionResult Register() {
        
        return View();
    }
    
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Kullanıcıyı veritabanına ekle (örnek işlem)
            // db.Users.Add(new User { ... });
            // db.SaveChanges();

            // Başarılı kayıt işlemi -> Login ekranına yönlendir.
            return RedirectToAction("Login", "Home");
        }

        // Eğer kayıt başarısızsa, aynı Register ekranında kal ve hataları göster.
        return View(model);
    }
  


}
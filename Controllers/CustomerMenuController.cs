using Microsoft.AspNetCore.Mvc;

namespace dbapp.Controllers;

public class CustomerMenuController : Controller{
    
    public IActionResult Dashboard() {
        
        return View();
    }
}
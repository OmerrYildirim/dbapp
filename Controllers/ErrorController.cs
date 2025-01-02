using System.Diagnostics;
using dbapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace dbapp.Controllers;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class ErrorController : Controller {
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    public IActionResult HandleError(int statusCode) => statusCode switch {
        401 => View("Unauthorized"),
        403 => View("Forbidden"),
        404 => View("NotFound"),
        _ => View("HandleError"),
    };
}
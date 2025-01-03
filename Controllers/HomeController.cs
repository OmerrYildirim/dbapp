using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using dbapp.Helpers;
using dbapp.Models;
using dbapp.Services;

namespace dbapp.Controllers;

public class HomeController(SqlHelper sqlHelper, JwtService jwtService) : Controller {
    public IActionResult ChoosePersonType() {
        return View();
    }

    // Login - GET
    [HttpGet]
    public IActionResult Login() {
        var token = Request.Cookies["JWT"];
        if (token == null) return View();
        var principal = jwtService.ValidateToken(token);
        if (principal == null) return View();
        var role = principal.FindFirst(ClaimTypes.Role);
        if (role == null) return View();
        return role.Value == "Employee"
            ? RedirectToAction("Index", "Employee")
            : RedirectToAction("CustomerDashboard", "Customer");
    }

    // Login - POST
    [HttpPost]
    public IActionResult Login(LoginViewModel model) {
        if (!ModelState.IsValid) return View(model);

        try {
            using (var connection = sqlHelper.OpenConnection()) {
                string query = @"
            SELECT PersonID 
            FROM [dbo].[PERSON] 
            WHERE Email = @Email AND PASSWORD_ = @Password AND PERSONTYPE = @PersonType";

                using (var command = new SqlCommand(query, connection)) {
                    // Add parameters
                    SqlHelper.AddParameter(command, "@Email", SqlDbType.VarChar, model.UserEmail);
                    SqlHelper.AddParameter(command, "@Password", SqlDbType.VarChar, model.Password);
                    SqlHelper.AddParameter(command, "@PersonType", SqlDbType.VarChar, model.UserType);

                    var result = SqlHelper.ExecuteScalar(command);

                    if (result == null) {
                        ModelState.AddModelError(string.Empty, "Invalid email or password. Please try again.");
                        return View(model);
                    }

                    var userId = Convert.ToInt32(result);

                    var token = jwtService.GenerateToken(userId, model.UserEmail, model.UserType);

                    Response.Cookies.Append("JWT", token, new CookieOptions {
                        HttpOnly = true,
                        Secure = HttpContext.Request.IsHttps,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.UtcNow.AddMinutes(120)
                    });

                    // Redirect to a secured page (e.g., Dashboard) upon successful login
                    return model.UserType == "Employee"
                        ? RedirectToAction("Index", "Employee")
                        : RedirectToAction("CustomerDashboard", "Customer");
                }
            }
        } catch {
            ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
            return View(model);
        }
    }

    // Register - GET
    [HttpGet]
    public IActionResult CustomerRegister() {
        return View();
    }

    // Register - POST
    [HttpPost]
    public IActionResult CustomerRegister(CustomerRegisterViewModel model) {
        if (!ModelState.IsValid) {
            ViewBag.ErrorMessage = "Lütfen tüm alanları doldurun.";
            Console.WriteLine("Model state is not valid");
            return View(model);
        }

        try {
            using (var connection = sqlHelper.OpenConnection()) {
                string storedProcedure = "pro_CreateCustomer";
                Console.WriteLine(storedProcedure);

                using (var command = new SqlCommand(storedProcedure, connection)) {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlHelper.AddParameter(command, "@FullNamePar", SqlDbType.NVarChar, model.FullName);
                    SqlHelper.AddParameter(command, "@HireDatePar", SqlDbType.Date, DateTime.Now);
                    SqlHelper.AddParameter(command, "@EmailPar", SqlDbType.VarChar, model.UserEmail);
                    SqlHelper.AddParameter(command, "@PasswordPar", SqlDbType.VarChar, model.Password);
                    SqlHelper.AddParameter(command, "@CompanyName", SqlDbType.NVarChar, model.CompanyName);

                    if (connection.State != ConnectionState.Open) connection.Open();

                    command.ExecuteNonQuery();
                }
            }

            TempData["SuccessMessage"] = "Kayıt başarılı! Lütfen giriş yapın.";
            return RedirectToAction("Login", "Home");
        } catch (Exception ex) {
            ModelState.AddModelError(string.Empty, "Beklenmedik bir hata oluştu. Lütfen tekrar deneyin.");
            ModelState.AddModelError(string.Empty, ex.Message);
            Console.WriteLine(ex.Message);

            return View(model);
        }
    }

    // Register - GET
    [HttpGet]
    public IActionResult EmployeeRegister() {
        return View();
    }


    [HttpPost]
    public IActionResult EmployeeRegister(EmployeeRegisterViewModel model) {
        if (!ModelState.IsValid) {
            ViewBag.ErrorMessage = "Lütfen tüm alanları doldurun.";
            Console.WriteLine("Model state is not valid");
            return View(model);
        }

        try {
            using (var connection = sqlHelper.OpenConnection()) {
                string storedProcedure = "pro_CreateEmployee";
                Console.WriteLine(storedProcedure);

                using (var command = new SqlCommand(storedProcedure, connection)) {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlHelper.AddParameter(command, "@FullNamePar", SqlDbType.NVarChar, model.FullName);
                    SqlHelper.AddParameter(command, "@HireDatePar", SqlDbType.Date, DateTime.Now);
                    SqlHelper.AddParameter(command, "@EmailPar", SqlDbType.VarChar, model.UserEmail);
                    SqlHelper.AddParameter(command, "@PasswordPar", SqlDbType.VarChar, model.Password);
                    SqlHelper.AddParameter(command, "@ManagerMail", SqlDbType.VarChar, model.UserEmail);

                    if (connection.State != ConnectionState.Open) connection.Open();

                    command.ExecuteNonQuery();
                }
            }

            TempData["SuccessMessage"] = "Kayıt başarılı! Lütfen giriş yapın.";
            return RedirectToAction("Login", "Home");
        } catch (Exception ex) {
            ModelState.AddModelError(string.Empty, "Beklenmedik bir hata oluştu. Lütfen tekrar deneyin.");
            ModelState.AddModelError(string.Empty, ex.Message);
            Console.WriteLine(ex.Message);

            return View(model);
        }
    }
}
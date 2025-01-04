using System.Data;
using System.Security.Claims;
using dbapp.Helpers;
using dbapp.Models;
using dbapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dbapp.Controllers;

[Authorize(Roles = "Employee")]
public class EmployeeController(SqlHelper sqlHelper, JwtService jwtService) : Controller {
    
    [HttpGet]
    public IActionResult EmployeeDashboard() {
        return View();
    }
    
    
    [HttpGet]
    public IActionResult GetProjects() {
        var token = Request.Cookies["JWT"];
        if (token == null) return View();

        var principal = jwtService.ValidateToken(token);
        if (principal == null) return View();


        var employeeEmail = principal.FindFirst(ClaimTypes.Email)?.Value;
        var projects = new List<EmployeeProjectsViewModel>();

        using (var command = sqlHelper.CreateCommand("pro_VIEW_ALL_PROJECT_FOR_EMPLOYEE")) {
            command.CommandType = CommandType.StoredProcedure;
            SqlHelper.AddParameter(command, "@empMail", SqlDbType.NVarChar, employeeEmail);
            sqlHelper.OpenConnection();

            using (var reader = SqlHelper.ExecuteReader(command)) {
                while (reader.Read()) {
                    projects.Add(new EmployeeProjectsViewModel {
                        Name = reader["PName"].ToString(),
                        Description = reader["PDescription"].ToString(),
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"])
                    });
                }
            }

            sqlHelper.CloseConnection();
        }

        return View(projects);
    }
    public IActionResult Logout()
    {
            
        Response.Cookies.Delete("JWT");
            
        return RedirectToAction("ChoosePersonType", "Home");
    }
  
}
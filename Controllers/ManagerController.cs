using System.Data;
using System.Security.Claims;
using dbapp.Helpers;
using dbapp.Models;
using dbapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace dbapp.Controllers;

public class ManagerController(SqlHelper sqlHelper, JwtService jwtService) : Controller {
    [HttpGet]
    public IActionResult ManagerDashboard() {
        return View();
    }


    public IActionResult GetBugReports() {
        var token = Request.Cookies["JWT"];
        if (token == null) return View();

        var principal = jwtService.ValidateToken(token);
        if (principal == null) return View();


        var managerEmail = principal.FindFirst(ClaimTypes.Email)?.Value;
        var bugReports = new List<GetBugReportViewModel>();


        using (var command = sqlHelper.CreateCommand("pro_VIEW_BUG_REPORTS_MANAGER")) {
            command.CommandType = CommandType.StoredProcedure;
            SqlHelper.AddParameter(command, "@managerMail", SqlDbType.VarChar, managerEmail);

            sqlHelper.OpenConnection();
            using (var reader = SqlHelper.ExecuteReader(command)) {
                while (reader.Read()) {
                    bugReports.Add(new GetBugReportViewModel {
                        ProductName = reader["PName"].ToString(),
                        VersionID = reader["VersionID"].ToString(),
                        Message = reader["Message_"].ToString(),
                        CompanyName = reader["CompanyName"].ToString(),
                        FeedbackDate = Convert.ToDateTime(reader["FeedbackDate"])
                    });
                }
            }

            sqlHelper.CloseConnection();
        }


        return View(bugReports);
    }


    public IActionResult GetFeatureRequests() {
        var token = Request.Cookies["JWT"];
        if (token == null) return View();

        var principal = jwtService.ValidateToken(token);
        if (principal == null) return View();


        var managerEmail = principal.FindFirst(ClaimTypes.Email)?.Value;
        var featureRequests = new List<GetFeatureRequestViewModel>();


        using (var command = sqlHelper.CreateCommand("pro_VIEW_FEATURE_REQUESTS_MANAGER")) {
            command.CommandType = CommandType.StoredProcedure;
            SqlHelper.AddParameter(command, "@managerMail", SqlDbType.VarChar, managerEmail);
            sqlHelper.OpenConnection();
            using (var reader = SqlHelper.ExecuteReader(command)) {
                while (reader.Read()) {
                    featureRequests.Add(new GetFeatureRequestViewModel {
                        ProductName = reader["PName"].ToString(),
                        Rating = Convert.ToInt32(reader["Rating"]),
                        Message = reader["Message_"].ToString(),
                        CompanyName = reader["CompanyName"].ToString(),
                        FeedbackDate = Convert.ToDateTime(reader["FeedbackDate"])
                    });
                }
            }

            sqlHelper.CloseConnection();
        }


        return View(featureRequests);
    }
    public IActionResult Logout()
    {
            
        Response.Cookies.Delete("JWT");
            
        return RedirectToAction("ChoosePersonType", "Home");
    }
}
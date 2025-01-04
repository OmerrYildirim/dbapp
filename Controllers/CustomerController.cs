using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dbapp.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using dbapp.Models;
using dbapp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dbapp.Controllers {
    [Authorize(Roles = "Customer")]
    public class CustomerController(SqlHelper sqlHelper, JwtService jwtService) : Controller {
        [HttpGet]
        public IActionResult CustomerDashboard() {
            return View();
        }


        
        [HttpGet]
        public IActionResult GetLicense() {
            var token = Request.Cookies["JWT"];
            if (token == null) return View();

            var principal = jwtService.ValidateToken(token);
            if (principal == null) return View();


            var customerEmail = principal.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(customerEmail)) {
                Console.WriteLine("User.Identity.Name is null or empty");
                return View(new List<LicenseViewModel>());
            }

            var licenses = new List<LicenseViewModel>();

            using (var command = sqlHelper.CreateCommand("EXEC pro_VIEW_CUSTOMER_COMPANY_LICENCES @CustomerEmail")) {
                SqlHelper.AddParameter(command, "@CustomerEmail", SqlDbType.VarChar, customerEmail);

                sqlHelper.OpenConnection();
                using (var reader = SqlHelper.ExecuteReader(command)) {
                    while (reader.Read()) {
                        licenses.Add(new LicenseViewModel {
                            ProductName = reader["PName"].ToString(),
                            StartDate = Convert.ToDateTime(reader["Start Date"]),
                            EndDate = Convert.ToDateTime(reader["End Date"]),
                            TotalFee = Convert.ToDecimal(reader["Total Fee"])
                        });
                    }
                }

                sqlHelper.CloseConnection();
            }

            return View(licenses);
        }


        
        [HttpGet]
        public IActionResult BuyLicense()
        {
            var products = new List<SelectListItem>();

            using (var command = sqlHelper.CreateCommand("SELECT ProductID, PName FROM PRODUCT_"))
            {
                sqlHelper.OpenConnection();
                using (var reader = SqlHelper.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        products.Add(new SelectListItem
                        {
                            Value = reader["PName"].ToString(),
                            Text = reader["PName"].ToString()
                        });
                    }
                }
                sqlHelper.CloseConnection();
            }

            ViewBag.Products = products;
            return View();
        }

        
        [HttpPost]
        public IActionResult BuyLicense(BuyLicenseModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var token = Request.Cookies["JWT"];
            if (token == null) return View();

            var principal = jwtService.ValidateToken(token);
            if (principal == null) return View();


            var customerEmail = principal.FindFirst(ClaimTypes.Email)?.Value;

            string companyName = null;

            
            using (var command = sqlHelper.CreateCommand(
                       "SELECT c.CompanyName FROM Company c INNER JOIN CUSTOMER cus ON c.CompanyID = cus.CompanyID " +
                       "INNER JOIN PERSON p ON cus.CustomerID = p.PersonID WHERE p.Email = @CustomerEmail"))
            {
                SqlHelper.AddParameter(command, "@CustomerEmail", SqlDbType.NVarChar, customerEmail);

                sqlHelper.OpenConnection();
                var result = SqlHelper.ExecuteScalar(command);
                companyName = result?.ToString(); 
                sqlHelper.CloseConnection();

                if (string.IsNullOrEmpty(companyName))
                {
                    ModelState.AddModelError(string.Empty, "Could not retrieve company name. Please contact support.");
                    return View(model);
                }
            }

            
            using (var command = sqlHelper.CreateCommand(
                       "EXEC pro_CREATE_LICENCE_UI @LicenceTermPar, @ProductNamePar, @CompanyNamePar"))
            {
                SqlHelper.AddParameter(command, "@LicenceTermPar", SqlDbType.Int, model.LicenseTerm);
                SqlHelper.AddParameter(command, "@ProductNamePar", SqlDbType.NVarChar, model.ProductName);
                SqlHelper.AddParameter(command, "@CompanyNamePar", SqlDbType.NVarChar, companyName);

                sqlHelper.OpenConnection();
                SqlHelper.ExecuteNonQuery(command);
                sqlHelper.CloseConnection();
            }

            ViewBag.Message = "License purchased successfully.";
            return RedirectToAction("GetLicense");
        }





        
        [HttpGet]
        public IActionResult BugReport() {
            var products = new List<SelectListItem>();

            using (var command = sqlHelper.CreateCommand("SELECT ProductID, PName FROM PRODUCT_"))
            {
                sqlHelper.OpenConnection();
                using (var reader = SqlHelper.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        products.Add(new SelectListItem
                        {
                            Value = reader["PName"].ToString(),
                            Text = reader["PName"].ToString()
                        });
                    }
                }
                sqlHelper.CloseConnection();
            }

            ViewBag.Products = products;
            return View();
        }

        
        [HttpGet]
        public ActionResult FeatureRequest() {
            var products = new List<SelectListItem>();

            using (var command = sqlHelper.CreateCommand("SELECT ProductID, PName FROM PRODUCT_"))
            {
                sqlHelper.OpenConnection();
                using (var reader = SqlHelper.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        products.Add(new SelectListItem
                        {
                            Value = reader["PName"].ToString(),
                            Text = reader["PName"].ToString()
                        });
                    }
                }
                sqlHelper.CloseConnection();
            }

            ViewBag.Products = products;
            return View();
        }

        
        [HttpPost]
        public IActionResult SubmitBugReport(BugReportModel model) {
            if (!ModelState.IsValid) return View("BugReport");
            
            if (!ModelState.IsValid) return View(model);
            var token = Request.Cookies["JWT"];
            if (token == null) return View();

            var principal = jwtService.ValidateToken(token);
            if (principal == null) return View();


            var customerEmail = principal.FindFirst(ClaimTypes.Email)?.Value;

            string companyName = null;

            
            using (var command = sqlHelper.CreateCommand(
                       "SELECT c.CompanyName FROM Company c INNER JOIN CUSTOMER cus ON c.CompanyID = cus.CompanyID " +
                       "INNER JOIN PERSON p ON cus.CustomerID = p.PersonID WHERE p.Email = @CustomerEmail"))
            {
                SqlHelper.AddParameter(command, "@CustomerEmail", SqlDbType.NVarChar, customerEmail);

                sqlHelper.OpenConnection();
                var result = SqlHelper.ExecuteScalar(command);
                companyName = result?.ToString(); 
                sqlHelper.CloseConnection();

                if (string.IsNullOrEmpty(companyName))
                {
                    ModelState.AddModelError(string.Empty, "Could not retrieve company name. Please contact support.");
                    return View(model);
                }
            }
            using (var command =
                   sqlHelper.CreateCommand(
                       "EXEC pro_CREATE_BUG_REPORT @messagePar, @fdatePar, @productnamePar, @companynamePar, @versionIDPar")) {
                SqlHelper.AddParameter(command, "@messagePar", SqlDbType.NVarChar, model.Message);
                SqlHelper.AddParameter(command, "@fdatePar", SqlDbType.Date, DateTime.Now);
                SqlHelper.AddParameter(command, "@productnamePar", SqlDbType.NVarChar, model.ProductName);
                SqlHelper.AddParameter(command, "@companynamePar", SqlDbType.NVarChar, companyName);
                SqlHelper.AddParameter(command, "@versionIDPar", SqlDbType.Decimal, model.VersionID);

                sqlHelper.OpenConnection();
                SqlHelper.ExecuteNonQuery(command);
                sqlHelper.CloseConnection();
            }

            ViewBag.Message = "Bug report submitted successfully.";
            return RedirectToAction("CustomerDashboard", "Customer");
        }

        
        [HttpPost]
        public IActionResult SubmitFeatureRequest(FeatureRequestModel model) {
            if (!ModelState.IsValid) return View("FeatureRequest");
            if (!ModelState.IsValid) return View(model);
            var token = Request.Cookies["JWT"];
            if (token == null) return View();

            var principal = jwtService.ValidateToken(token);
            if (principal == null) return View();


            var customerEmail = principal.FindFirst(ClaimTypes.Email)?.Value;

            string companyName = null;

            
            using (var command = sqlHelper.CreateCommand(
                       "SELECT c.CompanyName FROM Company c INNER JOIN CUSTOMER cus ON c.CompanyID = cus.CompanyID " +
                       "INNER JOIN PERSON p ON cus.CustomerID = p.PersonID WHERE p.Email = @CustomerEmail"))
            {
                SqlHelper.AddParameter(command, "@CustomerEmail", SqlDbType.NVarChar, customerEmail);

                sqlHelper.OpenConnection();
                var result = SqlHelper.ExecuteScalar(command);
                companyName = result?.ToString(); 
                sqlHelper.CloseConnection();

                if (string.IsNullOrEmpty(companyName))
                {
                    ModelState.AddModelError(string.Empty, "Could not retrieve company name. Please contact support.");
                    return View(model);
                }
            }
            using (var command =
                   sqlHelper.CreateCommand(
                       "EXEC pro_CREATE_FEATURE_REQUEST @messagePar, @fdatePar, @productnamePar, @companynamePar, @ratingPar")) {
                SqlHelper.AddParameter(command, "@messagePar", SqlDbType.NVarChar, model.Message);
                SqlHelper.AddParameter(command, "@fdatePar", SqlDbType.Date, DateTime.Now);
                SqlHelper.AddParameter(command, "@productnamePar", SqlDbType.NVarChar, model.ProductName);
                SqlHelper.AddParameter(command, "@companynamePar", SqlDbType.NVarChar, companyName);
                SqlHelper.AddParameter(command, "@ratingPar", SqlDbType.Int, model.Rating);

                sqlHelper.OpenConnection();
                SqlHelper.ExecuteNonQuery(command);
                sqlHelper.CloseConnection();
            }

            ViewBag.Message = "Feature request submitted successfully.";
            return RedirectToAction("CustomerDashboard", "Customer");
        }
        
        [HttpGet]
        public IActionResult CustomerInformation() {
            var token = Request.Cookies["JWT"];
            if (token == null) return View();

            var principal = jwtService.ValidateToken(token);
            if (principal == null) return View();
            
            var customerEmail = principal.FindFirst(ClaimTypes.Email)?.Value;
            
            var customerInfo = new CustomerInformationViewModel();

            using (var command = sqlHelper.CreateCommand("EXEC pro_VIEW_CUSTOMER_INFORMATION @CustomerEmail")) {
                SqlHelper.AddParameter(command, "@CustomerEmail", SqlDbType.VarChar, customerEmail);

                sqlHelper.OpenConnection();
                using (var reader = SqlHelper.ExecuteReader(command)) {
                    if (reader.Read()) {
                        customerInfo.FullName = reader["FullName"].ToString();
                        customerInfo.CompanyName = reader["CompanyName"].ToString();
                        customerInfo.Email = reader["Email"].ToString();
                        customerInfo.HireDate = Convert.ToDateTime(reader["HireDate"]);
                    }
                }

                sqlHelper.CloseConnection();
            }

            return View(customerInfo);
        }
        public IActionResult Logout()
        {
            
            Response.Cookies.Delete("JWT");
            
            return RedirectToAction("ChoosePersonType", "Home");
        }
    }
}
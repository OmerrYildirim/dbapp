using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dbapp.Helpers;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using dbapp.Models;
using dbapp.Services;

namespace dbapp.Controllers {
    [Authorize(Roles = "Customer")]
    public class CustomerController(SqlHelper sqlHelper, JwtService jwtService) : Controller {
        [HttpGet]
        public IActionResult CustomerDashboard(CustomerProductNames model) {
            if (!ModelState.IsValid) return View(model);

            var productNames = new List<string>();
            try {
                sqlHelper.OpenConnection();
                var command = sqlHelper.CreateCommand("SELECT PName FROM PRODUCT_");
                using (var reader = SqlHelper.ExecuteReader(command)) {
                    while (reader.Read()) {
                        productNames.Add(reader["PName"].ToString());
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            } finally {
                sqlHelper.CloseConnection();
            }

            // Eğer liste boşsa yine boş bir model gönderilir
            var viewModel = new CustomerProductNames {
                ProductNames = productNames
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetProductInfo(string name) {
            // Step 1: Retrieve JWT token from the cookies
            var token = Request.Cookies["JWT"];
            if (token == null) {
                // Handle the case where token is not found
                ModelState.AddModelError(string.Empty, "Token not found.");
            }

            // Step 2: Validate the token and extract claims
            var principal = jwtService.ValidateToken(token);
            if (principal == null) {
                // Handle invalid token
                ModelState.AddModelError(string.Empty, "Token not found.");
            }

            // Step 3: Extract the CustomerID from the JWT claims (assuming it's stored in the claims)
            var customerIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier); // Assuming customer ID is stored as the NameIdentifier
            if (customerIdClaim == null) {
                // Handle missing claim (if customer ID is not available)
                ModelState.AddModelError(string.Empty, "Token not found.");
            }

            int customerId;
            if (!int.TryParse(customerIdClaim.Value, out customerId)) {
                // Handle case where customer ID is not valid
                ModelState.AddModelError(string.Empty, "Token not found.");
            }

            // Step 4: Fetch the CompanyID associated with the CustomerID
            string getCompanyQuery = @"
        SELECT c.CompanyID
        FROM CUSTOMER cu
        INNER JOIN COMPANY c ON cu.CompanyID = c.CompanyID
        WHERE cu.CustomerID = @CustomerID";

            int? companyId = null;

            using (var command = sqlHelper.CreateCommand(getCompanyQuery)) {
                // Add the CustomerID parameter
                SqlHelper.AddParameter(command, "@CustomerID", SqlDbType.Int, customerId);

                // Open connection and retrieve the company ID
                sqlHelper.OpenConnection();
                using (var reader = SqlHelper.ExecuteReader(command)) {
                    if (reader.Read()) {
                        companyId = reader.GetInt32(0);
                    }
                }

                sqlHelper.CloseConnection();
            }

            if (!companyId.HasValue) {
                // Handle case where the user doesn't belong to any company
                ModelState.AddModelError(string.Empty, "Token not found.");
            }

            // Step 5: Fetch product information for the user's company using the CompanyID from the License table
            string query = @"
        SELECT p.PName, p.PDescription, p.ReleaseDate, pv.VersionID, pv.PVDate, pv.PVDescription
        FROM PRODUCT_ p
        INNER JOIN LICENCE l ON p.ProductID = l.ProductID
        LEFT JOIN PRODUCT_VERSION pv ON p.ProductID = pv.ProductID
        WHERE l.CompanyID = @CompanyID
        AND p.PName = @ProductName";

            using (var command = sqlHelper.CreateCommand(query)) {
                // Add parameters for product name and company ID
                SqlHelper.AddParameter(command, "@ProductName", SqlDbType.NVarChar, name);
                SqlHelper.AddParameter(command, "@CompanyID", SqlDbType.Int, companyId);

                // Open connection and execute query
                sqlHelper.OpenConnection();
                using (var reader = SqlHelper.ExecuteReader(command)) {
                    var productInfoList = new List<ProductInfoViewModel>();

                    // Read the data and map it to a list of view models
                    while (reader.Read()) {
                        var productInfo = new ProductInfoViewModel {
                            ProductName = reader.GetString(0),
                            ProductDescription = reader.GetString(1),
                            ReleaseDate = reader.GetDateTime(2),
                            VersionID = reader.IsDBNull(3) ? null : (decimal?)reader.GetDecimal(3),
                            VersionDate = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4),
                            VersionDescription = reader.IsDBNull(5) ? null : reader.GetString(5)
                        };
                        productInfoList.Add(productInfo);
                    }

                    // Close connection
                    sqlHelper.CloseConnection();

                    // Return the view with the product info
                    return View(productInfoList);
                }
            }
        }

        [HttpPost]
        public IActionResult BuyLicense(int productId, int licenseTerm) {
            var token = Request.Cookies["JWT"];
            if (token == null) {
                ModelState.AddModelError(string.Empty, "Token not found.");
                return RedirectToAction("GetProductInfo");
            }

            var principal = jwtService.ValidateToken(token);
            if (principal == null) {
                ModelState.AddModelError(string.Empty, "Invalid token.");
                return RedirectToAction("GetProductInfo");
            }

            var customerIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (customerIdClaim == null) {
                ModelState.AddModelError(string.Empty, "Customer ID not found.");
                return RedirectToAction("GetProductInfo");
            }

            int customerId;
            if (!int.TryParse(customerIdClaim.Value, out customerId)) {
                ModelState.AddModelError(string.Empty, "Invalid customer ID.");
                return RedirectToAction("GetProductInfo");
            }

            string getCompanyQuery = @"
        SELECT c.CompanyID
        FROM CUSTOMER cu
        INNER JOIN COMPANY c ON cu.CompanyID = c.CompanyID
        WHERE cu.CustomerID = @CustomerID";

            int? companyId = null;

            using (var command = sqlHelper.CreateCommand(getCompanyQuery)) {
                SqlHelper.AddParameter(command, "@CustomerID", SqlDbType.Int, customerId);
                sqlHelper.OpenConnection();
                using (var reader = SqlHelper.ExecuteReader(command)) {
                    if (reader.Read()) {
                        companyId = reader.GetInt32(0);
                    }
                }

                sqlHelper.CloseConnection();
            }

            if (!companyId.HasValue) {
                ModelState.AddModelError(string.Empty, "Customer not associated with a company.");
                return RedirectToAction("GetProductInfo");
            }

            // Insert the new license
            string insertLicenseQuery = @"
        INSERT INTO LICENCE (LicenceTerm, StartDate, ProductID, CompanyID)
        VALUES (@LicenceTerm, GETDATE(), @ProductID, @CompanyID)";

            using (var command = sqlHelper.CreateCommand(insertLicenseQuery)) {
                SqlHelper.AddParameter(command, "@LicenceTerm", SqlDbType.Int, licenseTerm);
                SqlHelper.AddParameter(command, "@ProductID", SqlDbType.Int, productId);
                SqlHelper.AddParameter(command, "@CompanyID", SqlDbType.Int, companyId);
                sqlHelper.OpenConnection();
                SqlHelper.ExecuteNonQuery(command);
                sqlHelper.CloseConnection();
            }

            return RedirectToAction("GetProductInfo"); // Redirect back to the product info view
        }
    }
}
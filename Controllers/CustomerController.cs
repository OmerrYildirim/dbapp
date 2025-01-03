using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dbapp.Helpers;
using System.Collections.Generic;
using dbapp.Models; 

namespace dbapp.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly SqlHelper _sqlHelper;

        public CustomerController(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        

        [HttpGet]
        public IActionResult CustomerDashboard(CustomerProductNames model)
        {
            if (!ModelState.IsValid) return View(model);
            
            List<string> productNames = new List<string>();
            try
            {
                _sqlHelper.OpenConnection();
                var command = _sqlHelper.CreateCommand("SELECT PName FROM PRODUCT_");
                using (var reader = SqlHelper.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        productNames.Add(reader["PName"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata loglama
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                _sqlHelper.CloseConnection();
            }

            return View(model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AWSDotnetCoreHandsOnLab.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AWSDotnetCoreHandsOnLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewData["ServerIdentifier"] = _configuration["ServerIdentifier"];
            return View();
        }

        public IActionResult RDS()
        {
            var vm = new ItemListScreenViewModel();
            vm.Items = new List<Item>();

            var connString = _configuration["SqlConnection"];
            using (var connection = new SqlConnection(connString))
            {
                var command = new SqlCommand("SELECT * FROM MyStuff", connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var item = new Item();
                    item.Id = Convert.ToInt32(reader["StuffId"]);
                    item.Description = reader["Description"].ToString();
                    item.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                    vm.Items.Add(item);
                }

                reader.Close();

            }

            vm.Random = connString;

            return View("RDS", vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

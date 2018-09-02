using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FaisalLearningProjectMVC.Models;
using FaisalLearningProjectMVC.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FaisalLearningProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly TsqlContext _context;

        public HomeController(TsqlContext context)
        {
            _context = context;

            //var customers = _context.Customers.Include(c => c.Orders).ToList();
            var order = _context.Orders.Include(c => c.Customer).FirstOrDefault();

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Features()
        {
            return View();
        }
    }
}

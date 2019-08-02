﻿using DocumentGenerator.Excel;
using DocumentGenerator.PowerPoint;
using DocumentGenerator.Word;
using FaisalLearningProjectMVC.Data;
using FaisalLearningProjectMVC.Helper;
using FaisalLearningProjectMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace FaisalLearningProjectMVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ContextDb _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly string documentTitle = "Orders";

        public OrdersController(ContextDb context, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        // GET: Orders

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.Include(o => o.Customer).ToListAsync();
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["Customers"] = new SelectList(_context.Customers, "ID", "ContactName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CustomerId,EmployeeId,ShipperId,OrderDate,RequiredDate,ShiedpDate,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "ID", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.ID == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("SoftDelete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.ID == id);
            order.IsActive = false;
            _context.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }

        public async Task<IActionResult> GetOrdersJsonData()
        {
            var orders = await _context.Orders.
                Include(c => c.Customer).
                Include(c => c.Shipper).
                Where(c => c.IsActive).
                Select(o => new
                {
                    ID = o.ID,
                    OrderDate = o.OrderDate.ToString("MM/dd/yyyy"),
                    ShipAddress = o.ShipAddress,
                    ShipCity = o.ShipCity,
                    ShipPostalCode = o.ShipPostalCode,
                    ShipCountry = o.ShipCountry,
                    Customer = o.Customer.ContactName ?? "",
                    Shipper = o.Shipper.CompanyName ?? ""
                }).ToListAsync();

            return Json(orders);
        }

        public async Task<ActionResult> DownloadWordTableDocument()
        {
            TableGeneratorWord word = new TableGeneratorWord();

            // we need to use anonoymus type to set the custom label names
            var orders = await _context.Orders.
                Include(c => c.Customer).
                Include(c => c.Shipper).
                Where(c => c.IsActive).
                Select(o => new
                {
                    OrderDate = o.OrderDate.ToString("MM/dd/yyyy"),
                    Customer = o.Customer.ContactName ?? "",
                    ShipAddress = o.ShipAddress,
                    ShipCity = o.ShipCity,
                    ShipPostalCode = o.ShipPostalCode,
                    ShipCountry = o.ShipCountry,
                    Shipper = o.Shipper.CompanyName ?? ""
                }).ToListAsync();

            var fileName = Helpers.GetWordDocumentFileName(documentTitle);
            word.Run(orders, documentTitle, fileName);

            //download file 
            byte[] fileBytes = await Helpers.DownloadFile(fileName, _configuration, _hostingEnvironment);
            var file = File(fileBytes, "application/x-msdownload", fileName);

            return file;
        }

        public async Task<ActionResult> DownloadExcelTableDocument()
        {
            TableGeneratorExcel excel = new TableGeneratorExcel();

            // we need to use anonoymus type to set the custom label names
            var orders = await _context.Orders.
                Include(c => c.Customer).
                Include(c => c.Shipper).
                Where(c => c.IsActive).
                Select(o => new
                {
                    OrderDate = o.OrderDate.ToString("MM/dd/yyyy"),
                    Customer = o.Customer.ContactName ?? "",
                    ShipAddress = o.ShipAddress,
                    ShipCity = o.ShipCity,
                    ShipPostalCode = o.ShipPostalCode,
                    ShipCountry = o.ShipCountry,
                    Shipper = o.Shipper.CompanyName ?? ""
                }).ToListAsync();


            var fileName = Helpers.GetExcelDocumentFileName(documentTitle);
            excel.Run(orders, documentTitle, fileName);

            //download file 
            byte[] fileBytes = await Helpers.DownloadFile(fileName, _configuration, _hostingEnvironment);
            var file = File(fileBytes, "application/x-msdownload", fileName);

            return file;
        }

        public async Task<ActionResult> DownloadPowerpointTableDocument()
        {
            TableGeneratorPowerPoint powerpoint = new TableGeneratorPowerPoint();

            // we need to use anonoymus type to set the custom label names
            var orders = await _context.Orders.
                Include(c => c.Customer).
                Include(c => c.Shipper).
                Where(c => c.IsActive).
                Select(o => new
                {
                    OrderDate = o.OrderDate.ToString("MM/dd/yyyy"),
                    Customer = o.Customer.ContactName ?? "",
                    ShipAddress = o.ShipAddress,
                    ShipCity = o.ShipCity,
                    ShipPostalCode = o.ShipPostalCode,
                    ShipCountry = o.ShipCountry,
                    Shipper = o.Shipper.CompanyName ?? ""
                }).ToListAsync();


            var fileName = Helpers.GetPowerpointDocumentFileName(documentTitle);
            powerpoint.Run(orders, documentTitle, fileName);

            //download file 
            byte[] fileBytes = await Helpers.DownloadFile(fileName, _configuration, _hostingEnvironment);
            var file = File(fileBytes, "application/x-msdownload", fileName);

            return file;
        }

    }
}

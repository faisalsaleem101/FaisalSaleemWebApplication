using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FaisalLearningProjectMVC.Data;
using FaisalLearningProjectMVC.Models;
using DocumentGenerator.PowerPoint;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using System.IO;
using DocumentGenerator.Word;

namespace FaisalLearningProjectMVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ContextDb _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public CustomersController(ContextDb context, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {        
            return View(await _context.Customers.ToListAsync());
        }        

        public async Task<ActionResult> DownloadPowerpointTable()
        {
            TableGeneratorPowerPoint tableGenerator = new TableGeneratorPowerPoint();
            var Customers = await _context.Customers.Select(x => new
            {
                Company_Name = x.CompanyName,
                Full_Name = x.ContactName,
                Title = x.ContactTitle,
                x.Address,
                x.City,
                x.Country,
            }).ToListAsync();

            var fileName = tableGenerator.Run(Customers, nameof(Customers));
            return DownloadFile(fileName);
        }

        public async Task<ActionResult> DownloadWordTable()
        {
            TableGeneratorWord tableGeneratorWord = new TableGeneratorWord();
            var Customers = await _context.Customers.Select(x => new
            {
                Company_Name = x.CompanyName,
                Full_Name = x.ContactName,
                Title = x.ContactTitle,
                x.Address,
                x.City,
                x.Country,
            }).ToListAsync();

            var fileName = tableGeneratorWord.Run(Customers, nameof(Customers));
            return DownloadFile(fileName);
        }


        
        private FileContentResult DownloadFile(string fileName)
        {
            var directory = Directory.GetParent(_hostingEnvironment.ContentRootPath).FullName;
            var outputFolder = _configuration.GetValue<string>("WebsiteSettings:OutputFolder");

            var filePath = $"{directory}\\{outputFolder}\\{fileName}";
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/x-msdownload", fileName);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .SingleOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        {
            if (id != customer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.ID))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .SingleOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.ID == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.ID == id);
        }

        public async Task<IActionResult> GetCustomerIndexTableData()
        {
            var customers = await _context.Customers.ToListAsync();
            return Json(customers);
        }
    }
}

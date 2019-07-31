using DocumentGenerator.Excel;
using DocumentGenerator.PowerPoint;
using DocumentGenerator.Word;
using FaisalLearningProjectMVC.Data;
using FaisalLearningProjectMVC.Helper;
using FaisalLearningProjectMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.Where(c => c.IsActive).ToListAsync());
        }


        public async Task<ActionResult> DownloadExcelTableDocument()
        {
            TableGeneratorExcel excel = new TableGeneratorExcel();

            // we need to use anonoymus type to set the custom label names
            var customers = await _context.Customers.Select(x => new
            {
                FullName = x.ContactName,
                Company = x.CompanyName,
                JobTitle = x.ContactTitle,
                x.Address,
                x.City,
            }).ToListAsync();

            var title = "Customers";

            var fileName = Helpers.GetExcelDocumentFileName(title);
            excel.Run(customers, title, fileName);

            //download file 
            byte[] fileBytes = await Helpers.DownloadFile(fileName, _configuration, _hostingEnvironment);
            var file = File(fileBytes, "application/x-msdownload", fileName);

            return file;
        }

        public async Task<ActionResult> DownloadWordTableDocument()
        {
            TableGeneratorWord word = new TableGeneratorWord();

            // we need to use anonoymus type to set the custom label names
            var customers = await _context.Customers.Select(x => new
            {
                FullName = x.ContactName,
                Company = x.CompanyName,
                JobTitle = x.ContactTitle,
                x.Address,
                x.City,
            }).ToListAsync();

            var title = "Customers";

            var fileName = Helpers.GetWordDocumentFileName(title);
            word.Run(customers, title, fileName);

            //download file 
            byte[] fileBytes = await Helpers.DownloadFile(fileName, _configuration, _hostingEnvironment);
            var file = File(fileBytes, "application/x-msdownload", fileName);

            return file;
        }

        public async Task<ActionResult> DownloadPowerpointTableDocument()
        {
            TableGeneratorPowerPoint powerpoint = new TableGeneratorPowerPoint();

            // we need to use anonoymus type to set the custom label names
            var customers = await _context.Customers.Select(x => new
            {
                FullName = x.ContactName,
                Company = x.CompanyName,
                JobTitle = x.ContactTitle,
                x.Address,
                x.City,
            }).ToListAsync();

            var title = "Customers";

            var fileName = Helpers.GetPowerpointDocumentFileName(title);
            powerpoint.Run(customers, title, fileName);

            //download file 
            byte[] fileBytes = await Helpers.DownloadFile(fileName, _configuration, _hostingEnvironment);
            var file = File(fileBytes, "application/x-msdownload", fileName);

            return file;
        }


        // GET: Customers/Details/5
        [Authorize]
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.IsActive = true;
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.ID == id && m.IsActive);
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
        public async Task<IActionResult> Edit(int id, Customer customer)
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
        [Authorize]
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

        // POST: Customers/Delete/5
        [HttpPost, ActionName("SoftDelete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.ID == id);
            customer.IsActive = false;
            _context.Update(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.ID == id);
        }

        public async Task<IActionResult> GetCustomersData()
        {
            var customers = await _context.Customers.Where(c => c.IsActive).ToListAsync();
            return Json(customers);
        }
    }
}
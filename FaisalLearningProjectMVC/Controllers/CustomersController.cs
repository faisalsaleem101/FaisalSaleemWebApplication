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

namespace FaisalLearningProjectMVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ContextDb _context;

        public CustomersController(ContextDb context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            CreatePowerPointTable(_context.Customers.ToList());
            return View(await _context.Customers.ToListAsync());
        }

        public void CreatePowerPointTable(List<Customer> customers)
        {
            // column names to be used by the table 
            var columns = new List<string> { "Company Name", "Contact Name", "Contact Title", "Address", "City", "Region", "Postal Code", "Country", "Phone", "Fax" };

            // initilize an array with the number of rows based on the customers records total and 10 columns 
            string[,] data = new string[customers.Count, 10];

            int counter = 0;

            //assign data
            foreach (var customer in customers)
            {
                data[counter, 0] = customer.CompanyName;
                data[counter, 1] = customer.ContactName;
                data[counter, 2] = customer.ContactTitle;
                data[counter, 3] = customer.Address;
                data[counter, 4] = customer.City;
                data[counter, 5] = customer.Region;
                data[counter, 6] = customer.PostalCode;
                data[counter, 7] = customer.Country;
                data[counter, 8] = customer.Phone;
                data[counter, 9] = customer.Fax;
                counter++;
            }

            TableGeneratorPP tableGenerator = new TableGeneratorPP();
            tableGenerator.Run(columns, data, "Customers");
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

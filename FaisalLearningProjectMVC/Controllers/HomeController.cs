using FaisalLearningProjectMVC.Data;
using FaisalLearningProjectMVC.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FaisalLearningProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContextDb _context;
        private readonly IConfiguration _configuration;

        public HomeController(ContextDb context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

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

        public IActionResult Dashboard()
        {
            return View();
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await SendMail(contact);
                ModelState.Clear();
            }

            return View();
        }

        private async Task SendMail(Contact contact)
        {
            var fromEmailAddress = _configuration.GetValue<string>("Config:FromEmailAddress");
            var fromEmailPassword = _configuration.GetValue<string>("Config:FromEmailPassword");
            var toEmailAddress = _configuration.GetValue<string>("Config:ToEmailAddress");
            string template = GetEmailBody(contact);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(contact.Name, fromEmailAddress));
            message.To.Add(new MailboxAddress("Faisal Saleem", toEmailAddress));
            message.Subject = $"Contact Us {(contact.Subject != null ? $"- {contact.Subject}" : "")} ";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Flowed) { Text = template };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(fromEmailAddress, fromEmailPassword);
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }

        private static string GetEmailBody(Contact contact)
        {
            return $@"From:
            {contact.Email}
            Message:
            {contact.Message}";
        }

        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Features()
        {
            return View();
        }
    }
}
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FaisalLearningProjectMVC.Models;
using FaisalLearningProjectMVC.Data;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

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

        public async Task<IActionResult> Contact()
        {
            ViewData["Message"] = "Your contact page.";

            await SendMail("Test");

            return View();
        }

        private async Task SendMail(string mailbody)
        {
            var fromEmailAddress = _configuration.GetValue<string>("MyConfig:FromEmailAddress");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Faisal Saleem Web App", fromEmailAddress));
            message.To.Add(new MailboxAddress("Faisal Saleem", _configuration.GetValue<string>("MyConfig:ToEmailAddress")));
            message.Subject = "Contact Us";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = "test2"};

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(fromEmailAddress, _configuration.GetValue<string>("MyConfig:FromEmailPassword"));
                await client.SendAsync(message);
                client.Disconnect(true);
            }            
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

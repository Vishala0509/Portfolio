using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VishalaPortfolio.Models;

namespace VishalaPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmailService _emailService;

        public HomeController(ILogger<HomeController> logger, EmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Education()
        {
            return View();
        }

        public IActionResult Skills()
        {
            return View();
        }

        public IActionResult Experience()
        {
            return View();
        }

        public IActionResult Projects()
        {
            return View();
        }

        public IActionResult Certifications()
        {
            return View();
        }

        // GET method for the Contact page
        public IActionResult Contact()
        {
            return View(); // This will render the Contact view
        }

        [HttpPost]
        public async Task<IActionResult> Contact(Contact model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Use the injected _emailService instance to send the email
            await _emailService.SendEmailAsync(
                toEmail: "your-receiving-email@gmail.com", // Replace with actual email
                subject: model.Subject,
                message: $"From: {model.Name} <{model.Email}>\n\n{model.Message}",
                fromName: model.Name
            );

            // Show a confirmation message
            ViewBag.Message = "Thank you for reaching out! We will get back to you soon.";

            // Option 1: Redirect to a "Thank You" page
            return RedirectToAction("ThankYou"); // This can redirect to a ThankYou action if you create one
        }

        // Action for the "Thank You" page
        public IActionResult ThankYou()
        {
            return View();  // Return the ThankYou view
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

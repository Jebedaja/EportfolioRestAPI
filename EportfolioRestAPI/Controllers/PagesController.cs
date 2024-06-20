using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EportfolioRestAPI.Controllers
{
    [Route("pages")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public PagesController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("login")]
        public IActionResult LoginPage()
        {
            var filePath = Path.Combine(_env.WebRootPath, "pages", "login.html");
            return System.IO.File.Exists(filePath) ? File(System.IO.File.ReadAllBytes(filePath), "text/html") : NotFound();
        }

        [HttpGet("register")]
        public IActionResult RegisterPage()
        {
            var filePath = Path.Combine(_env.WebRootPath, "pages", "register.html");
            return System.IO.File.Exists(filePath) ? File(System.IO.File.ReadAllBytes(filePath), "text/html") : NotFound();
        }

        [HttpGet("portfolio")]
        public IActionResult PortfolioPage()
        {
            var filePath = Path.Combine(_env.WebRootPath, "pages", "portfolio.html");
            return System.IO.File.Exists(filePath) ? File(System.IO.File.ReadAllBytes(filePath), "text/html") : NotFound();
        }
    }
}

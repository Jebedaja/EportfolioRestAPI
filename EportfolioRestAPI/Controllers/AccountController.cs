using EportfolioRestAPI.Data;
using EportfolioRestAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EportfolioRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly PortfolioDbContext _context;

        // Konstruktor inicjalizujący niezbędne usługi
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, PortfolioDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        // Endpoint do rejestracji nowego użytkownika
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            // Sprawdza, czy model przekazany w żądaniu jest poprawny
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Error = "Invalid model state", Details = ModelState });
            }

            try
            {
                // Sprawdza, czy użytkownik o podanym adresie e-mail już istnieje
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    return BadRequest(new { Error = "Email already in use" });
                }

                // Tworzy nowego użytkownika
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                // Sprawdza, czy rejestracja się powiodła
                if (result.Succeeded)
                {
                    return Ok(new { Result = "User registered successfully" });
                }

                return BadRequest(new { Error = "Registration failed", Details = result.Errors });
            }
            catch (Exception ex)
            {
                // Obsługuje błędy wewnętrzne serwera
                return StatusCode(500, new { Error = "An error occurred while registering the user", Details = ex.Message });
            }
        }

        // Endpoint do logowania użytkownika
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            // Sprawdza, czy model przekazany w żądaniu jest poprawny
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Error = "Invalid model state", Details = ModelState });
            }

            try
            {
                // Sprawdza, czy użytkownik o podanym adresie e-mail istnieje
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return BadRequest(new { Error = "Invalid login attempt", Details = "User not found" });
                }

                // Próbuje zalogować użytkownika
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Generuje token JWT dla zalogowanego użytkownika
                    var token = GenerateJwtToken(user);
                    return Ok(new { Token = token });
                }

                return BadRequest(new { Error = "Invalid login attempt" });
            }
            catch (Exception ex)
            {
                // Obsługuje błędy wewnętrzne serwera
                return StatusCode(500, new { Error = "An error occurred while logging in", Details = ex.Message });
            }
        }

        // Metoda generująca token JWT
        private string GenerateJwtToken(IdentityUser user)
        {
            // Tworzy listę roszczeń (claims) dla tokenu JWT
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Pobiera klucz szyfrujący z konfiguracji
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tworzy token JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token ważny przez 1 godzinę
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Endpoint do pobierania wszystkich portfeli
        [HttpGet("Portfolio")]
        public async Task<ActionResult<IEnumerable<PortfolioModel>>> GetPortfolios()
        {
            try
            {
                // Pobiera wszystkie portfele z bazy danych
                return await _context.Portfolios.ToListAsync();
            }
            catch (Exception ex)
            {
                // Obsługuje błędy wewnętrzne serwera
                return StatusCode(500, new { Error = "An error occurred while fetching portfolios", Details = ex.Message });
            }
        }

        // Endpoint do pobierania pojedynczego portfela na podstawie ID
        [HttpGet("Portfolio/{id}")]
        public async Task<ActionResult<PortfolioModel>> GetPortfolio(int id)
        {
            try
            {
                // Pobiera portfel z bazy danych na podstawie ID
                var portfolio = await _context.Portfolios.FindAsync(id);

                if (portfolio == null)
                {
                    return NotFound();
                }

                return portfolio;
            }
            catch (Exception ex)
            {
                // Obsługuje błędy wewnętrzne serwera
                return StatusCode(500, new { Error = "An error occurred while fetching the portfolio", Details = ex.Message });
            }
        }

        // Endpoint do tworzenia nowego portfela
        [HttpPost("Portfolio")]
        public async Task<ActionResult<PortfolioModel>> PostPortfolio([FromBody] PortfolioModel portfolio)
        {
            try
            {
                // Ustawia datę dodania portfela na aktualny czas
                portfolio.DateAdded = DateTime.Now;

                // Logowanie przychodzących danych
                Console.WriteLine("Received portfolio data: ");
                Console.WriteLine($"PortfolioName: {portfolio.PortfolioName}");
                Console.WriteLine($"Description: {portfolio.Description}");
                Console.WriteLine($"YouTubeLink: {portfolio.YouTubeLink}");

                // Walidacja danych wejściowych
                if (string.IsNullOrEmpty(portfolio.PortfolioName) ||
                    string.IsNullOrEmpty(portfolio.Description) ||
                    string.IsNullOrEmpty(portfolio.YouTubeLink))
                {
                    Console.WriteLine("Validation failed: All fields are required.");
                    return BadRequest(new { Error = "All fields are required.", Data = portfolio });
                }

                // Dodaje nowy portfel do bazy danych
                _context.Portfolios.Add(portfolio);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPortfolio), new { id = portfolio.Id }, portfolio);
            }
            catch (Exception ex)
            {
                // Obsługuje błędy wewnętrzne serwera
                return StatusCode(500, new { Error = "An error occurred while creating the portfolio", Details = ex.Message });
            }
        }

        // Endpoint do aktualizowania istniejącego portfela
        [HttpPut("Portfolio/{id}")]
        public async Task<IActionResult> PutPortfolio(int id, [FromBody] PortfolioModel portfolio)
        {
            if (id != portfolio.Id)
            {
                return BadRequest("Mismatched id parameter and portfolio id");
            }

            try
            {
                // Pobiera istniejący portfel z bazy danych na podstawie ID
                var existingPortfolio = await _context.Portfolios.FindAsync(id);

                if (existingPortfolio == null)
                {
                    return NotFound($"Portfolio with id {id} not found");
                }

                // Aktualizuje dane portfela
                existingPortfolio.PortfolioName = portfolio.PortfolioName;
                existingPortfolio.Description = portfolio.Description;
                existingPortfolio.YouTubeLink = portfolio.YouTubeLink;

                try
                {
                    // Zapisuje zmiany do bazy danych
                    _context.Entry(existingPortfolio).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioExists(id))
                    {
                        return NotFound($"Portfolio with id {id} not found");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                // Obsługuje błędy wewnętrzne serwera
                return StatusCode(500, new { Error = "An error occurred while updating the portfolio", Details = ex.Message });
            }
        }

        // Endpoint do usuwania portfela na podstawie ID
        [HttpDelete("Portfolio/{id}")]
        public async Task<IActionResult> DeletePortfolio(int id)
        {
            try
            {
                // Pobiera portfel z bazy danych na podstawie ID
                var portfolio = await _context.Portfolios.FindAsync(id);

                if (portfolio == null)
                {
                    return NotFound();
                }

                // Usuwa portfel z bazy danych
                _context.Portfolios.Remove(portfolio);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Obsługuje błędy wewnętrzne serwera
                return StatusCode(500, new { Error = "An error occurred while deleting the portfolio", Details = ex.Message });
            }
        }

        // Sprawdza, czy portfel o podanym ID istnieje w bazie danych
        private bool PortfolioExists(int id)
        {
            try
            {
                return _context.Portfolios.Any(e => e.Id == id);
            }
            catch (Exception ex)
            {
                // Obsługuje błędy wewnętrzne serwera
                return false;
            }
        }
    }
}

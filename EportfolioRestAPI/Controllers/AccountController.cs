using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EportfolioRestAPI.Models;
using Microsoft.Extensions.Configuration;
using EportfolioRestAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authorization;

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

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, PortfolioDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok(new { Result = "User registered successfully" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return BadRequest(new { Error = "Registration failed", Details = result.Errors });
            }

            return BadRequest(new { Error = "Invalid model state", Details = ModelState });
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var token = GenerateJwtToken(user);

                    return Ok(new { Token = token });
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return BadRequest(new { Error = "Login failed", Details = ModelState });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // CRUD operations for PortfolioModel

        [HttpGet("Portfolio")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PortfolioModel>>> GetPortfolios()
        {
            return await _context.Portfolios.ToListAsync();
        }

        [HttpGet("Portfolio/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PortfolioModel>> GetPortfolio(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);

            if (portfolio == null)
            {
                return NotFound();
            }

            return portfolio;
        }

        [HttpPost("Portfolio")]
        [AllowAnonymous]  // Changed to AllowAnonymous
        public async Task<ActionResult<PortfolioModel>> PostPortfolio([FromBody] PortfolioModel portfolio)
        {
            portfolio.DateAdded = DateTime.Now;

            var user = await _userManager.GetUserAsync(User);
            portfolio.Username = user.Email; // Ustawienie nazwy użytkownika na podstawie adresu e-mail

            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPortfolio), new { id = portfolio.Id }, portfolio);
        }

        [HttpPut("Portfolio/{id}")]
        [AllowAnonymous]  // Changed to AllowAnonymous
        public async Task<IActionResult> PutPortfolio(int id, [FromBody] PortfolioModel portfolio)
        {
            if (id != portfolio.Id)
            {
                return BadRequest("Mismatched id parameter and portfolio id");
            }

            var existingPortfolio = await _context.Portfolios.FindAsync(id);
            if (existingPortfolio == null)
            {
                return NotFound($"Portfolio with id {id} not found");
            }

            existingPortfolio.PortfolioName = portfolio.PortfolioName;
            existingPortfolio.Description = portfolio.Description;
            existingPortfolio.YouTubeLink = portfolio.YouTubeLink;

            try
            {
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

        [HttpDelete("Portfolio/{id}")]
        [AllowAnonymous]  // Changed to AllowAnonymous
        public async Task<IActionResult> DeletePortfolio(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(e => e.Id == id);
        }
    }
}

using EportfolioRestAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Dodanie us³ugi CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", // Definiowanie polityki CORS o nazwie "CorsPolicy"
        builder => builder
        .AllowAnyOrigin() // Zezwalanie na zapytania z dowolnego Ÿród³a
        .AllowAnyMethod() // Zezwalanie na wszystkie metody HTTP (GET, POST, etc.)
        .AllowAnyHeader()); // Zezwalanie na wszystkie nag³ówki HTTP
});

// Dodanie us³ug do kontenera DI (Dependency Injection)
builder.Services.AddControllers(); // Dodanie us³ug kontrolerów

// Konfiguracja bazy danych
builder.Services.AddDbContext<PortfolioDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // Ustawienie SQL Server jako dostawcê bazy danych
});

// Konfiguracja to¿samoœci u¿ytkowników
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Ustawienia zasad dotycz¹cych hase³
    options.Password.RequireDigit = true; // Wymaganie cyfr w has³ach
    options.Password.RequiredLength = 6; // Minimalna d³ugoœæ has³a
    options.Password.RequireNonAlphanumeric = false; // Nie wymaga znaków specjalnych
    options.Password.RequireUppercase = true; // Wymaganie wielkich liter
    options.Password.RequireLowercase = true; // Wymaganie ma³ych liter
})
    .AddEntityFrameworkStores<PortfolioDbContext>() // U¿ywanie bazy danych z kontekstem PortfolioDbContext do przechowywania to¿samoœci
    .AddDefaultTokenProviders(); // Dodanie domyœlnych dostawców tokenów (np. do resetowania hase³)

// Konfiguracja uwierzytelniania JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Ustawienie domyœlnego schematu uwierzytelniania na JWT
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Ustawienie domyœlnego schematu wyzwañ na JWT
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Walidacja wystawcy tokena
            ValidateAudience = true, // Walidacja odbiorcy tokena
            ValidateLifetime = true, // Walidacja okresu wa¿noœci tokena
            ValidateIssuerSigningKey = true, // Walidacja klucza podpisuj¹cego token
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Ustawienie prawid³owego wystawcy tokena
            ValidAudience = builder.Configuration["Jwt:Audience"], // Ustawienie prawid³owego odbiorcy tokena
            IssuerSigningKey = new SymmetricSecurityKey(key) // Ustawienie klucza podpisuj¹cego
        };
    });

var app = builder.Build();

// Konfiguracja zachowania aplikacji w œrodowisku produkcyjnym
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // U¿ywanie specjalnej strony b³êdów w przypadku wyj¹tków
    app.UseHsts(); // W³¹czanie HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection(); // Przekierowywanie zapytañ HTTP na HTTPS
app.UseStaticFiles(); // Umo¿liwienie obs³ugi statycznych plików (np. CSS, JS)
app.UseRouting(); // W³¹czenie routingu dla aplikacji
app.UseCors("CorsPolicy"); // U¿ywanie polityki CORS o nazwie "CorsPolicy"
app.UseAuthentication(); // W³¹czenie uwierzytelniania
app.UseAuthorization(); // W³¹czenie autoryzacji
app.MapControllers(); // Mapowanie kontrolerów na œcie¿ki URL
app.MapFallbackToFile("index.html"); // Przekierowanie nieznanych œcie¿ek na index.html (np. dla SPA)
app.Run(); // Uruchomienie aplikacji

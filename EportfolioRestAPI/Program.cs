using EportfolioRestAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Dodanie us�ugi CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", // Definiowanie polityki CORS o nazwie "CorsPolicy"
        builder => builder
        .AllowAnyOrigin() // Zezwalanie na zapytania z dowolnego �r�d�a
        .AllowAnyMethod() // Zezwalanie na wszystkie metody HTTP (GET, POST, etc.)
        .AllowAnyHeader()); // Zezwalanie na wszystkie nag��wki HTTP
});

// Dodanie us�ug do kontenera DI (Dependency Injection)
builder.Services.AddControllers(); // Dodanie us�ug kontroler�w

// Konfiguracja bazy danych
builder.Services.AddDbContext<PortfolioDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // Ustawienie SQL Server jako dostawc� bazy danych
});

// Konfiguracja to�samo�ci u�ytkownik�w
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Ustawienia zasad dotycz�cych hase�
    options.Password.RequireDigit = true; // Wymaganie cyfr w has�ach
    options.Password.RequiredLength = 6; // Minimalna d�ugo�� has�a
    options.Password.RequireNonAlphanumeric = false; // Nie wymaga znak�w specjalnych
    options.Password.RequireUppercase = true; // Wymaganie wielkich liter
    options.Password.RequireLowercase = true; // Wymaganie ma�ych liter
})
    .AddEntityFrameworkStores<PortfolioDbContext>() // U�ywanie bazy danych z kontekstem PortfolioDbContext do przechowywania to�samo�ci
    .AddDefaultTokenProviders(); // Dodanie domy�lnych dostawc�w token�w (np. do resetowania hase�)

// Konfiguracja uwierzytelniania JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Ustawienie domy�lnego schematu uwierzytelniania na JWT
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Ustawienie domy�lnego schematu wyzwa� na JWT
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Walidacja wystawcy tokena
            ValidateAudience = true, // Walidacja odbiorcy tokena
            ValidateLifetime = true, // Walidacja okresu wa�no�ci tokena
            ValidateIssuerSigningKey = true, // Walidacja klucza podpisuj�cego token
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Ustawienie prawid�owego wystawcy tokena
            ValidAudience = builder.Configuration["Jwt:Audience"], // Ustawienie prawid�owego odbiorcy tokena
            IssuerSigningKey = new SymmetricSecurityKey(key) // Ustawienie klucza podpisuj�cego
        };
    });

var app = builder.Build();

// Konfiguracja zachowania aplikacji w �rodowisku produkcyjnym
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // U�ywanie specjalnej strony b��d�w w przypadku wyj�tk�w
    app.UseHsts(); // W��czanie HTTP Strict Transport Security (HSTS)
}

app.UseHttpsRedirection(); // Przekierowywanie zapyta� HTTP na HTTPS
app.UseStaticFiles(); // Umo�liwienie obs�ugi statycznych plik�w (np. CSS, JS)
app.UseRouting(); // W��czenie routingu dla aplikacji
app.UseCors("CorsPolicy"); // U�ywanie polityki CORS o nazwie "CorsPolicy"
app.UseAuthentication(); // W��czenie uwierzytelniania
app.UseAuthorization(); // W��czenie autoryzacji
app.MapControllers(); // Mapowanie kontroler�w na �cie�ki URL
app.MapFallbackToFile("index.html"); // Przekierowanie nieznanych �cie�ek na index.html (np. dla SPA)
app.Run(); // Uruchomienie aplikacji

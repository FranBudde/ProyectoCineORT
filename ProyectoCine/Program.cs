using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProyectoCine.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios: Aqu� agregas el DbContext con la cadena de conexi�n
builder.Services.AddDbContext<CineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging());


// Configurar enrutamiento para URLs en min�sculas
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true; // Convierte todas las URLs a min�sculas
    options.AppendTrailingSlash = false; // Evita barras finales innecesarias
});

// Habilitar sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiraci�n de la sesi�n
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Agregar controladores con vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Llama al inicializador para poblar la base de datos
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CineContext>();
        DbInitializer.Initialize(context); // Inicializa y pobla la base de datos
    }
    catch (Exception ex)
    {
        // Maneja errores durante la inicializaci�n de la base de datos
        Console.WriteLine($"Error al poblar la base de datos: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/home/error"); // URL en min�sculas
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // Habilitar el uso de sesiones
app.UseAuthorization();

// Configurar el enrutamiento predeterminado
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}"); // URLs en min�sculas

app.Run();

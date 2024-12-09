using Microsoft.EntityFrameworkCore;
using ProyectoCine.Data; // Tu espacio de nombres para el DbContext y las entidades
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios: Aquí agregas el DbContext con la cadena de conexión
builder.Services.AddDbContext<CineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
        // Maneja errores durante la inicialización de la base de datos
        Console.WriteLine($"Error al poblar la base de datos: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Si usas autorización e identidad, aquí irían app.UseAuthentication() y app.UseAuthorization()
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

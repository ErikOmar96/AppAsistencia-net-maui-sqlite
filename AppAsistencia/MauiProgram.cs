using Microsoft.Extensions.Logging;
// Agregar contexto de base de datos y Vistas
using AppAsistencia.DataAccess;
using AppAsistencia.Vistas;
// Using de fingerprint
using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;

namespace AppAsistencia
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Crear instancia de DBContext
            var dbContext = new AsistenciaDBContext();
            // Validar si se creo la DB
            dbContext.Database.EnsureCreated();
            // Liberar la DB una vez creada
            dbContext.Dispose();
            // Agregar
            builder.Services.AddDbContext<AsistenciaDBContext>();
            // Inyectar páginas que van usar las base de datos
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegistroPage>();
            // Agregar singleton de AsistenciaPage        
            builder.Services.AddSingleton<AsistenciaPage>();
            builder.Services.AddTransient<MenuPage>();
            

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // Agregar esta linea
            builder.Services.AddSingleton(typeof(IFingerprint), CrossFingerprint.Current);
            return builder.Build();
        }
    }
}

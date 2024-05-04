using Microsoft.Extensions.Logging;
// Agregar contexto de base de datos y Vistas
using AppAsistencia.DataAccess;
using AppAsistencia.Vistas;
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

            
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}

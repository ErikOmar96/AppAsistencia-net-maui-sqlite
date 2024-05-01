using Microsoft.Extensions.Logging;
// Agregar contexto de base de datos y Vistas
using AppAsistencia.DataAccess;
using AppAsistencia.Vistas;

namespace AppAsistencia
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Agregar
            builder.Services.AddDbContext<AsistenciaDBContext>();
            // Inyectar páginas que van usar las base de datos
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegistroPage>();


            // Crear contexto
            var dbContext = new AsistenciaDBContext();
            // Validar si se creo la DB
            dbContext.Database.EnsureCreated();
            // Liberar la DB una vez creada
            dbContext.Dispose();
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}

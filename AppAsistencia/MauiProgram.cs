using Microsoft.Extensions.Logging;
// Agregar contexto de base de datos y Vistas
using AppAsistencia.DataAccess;
using AppAsistencia.Vistas;
using AppAsistencia.VistaModelos;
// Using de Plugin instalado
using Plugin.Maui.Biometric;
//
using CommunityToolkit.Maui;
namespace AppAsistencia
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //// Crear instancia de DBContext
            //var dbContext = new AsistenciaDBContext();
            //// Validar si se creo la DB
            //dbContext.Database.EnsureCreated();
            //// Liberar la DB una vez creada
            //dbContext.Dispose();
            //// Agregar
            //builder.Services.AddDbContext<AsistenciaDBContext>();
            //// Inyectar páginas que van usar las base de datos
            //builder.Services.AddTransient<LoginPage>();
            //builder.Services.AddTransient<RegistroPage>();
            //// Agregar singleton de AsistenciaPage        
            //builder.Services.AddSingleton<AsistenciaPage>();
            //builder.Services.AddTransient<MenuPage>();
            

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // Agregar esta linea
            //builder.Services.AddSingleton(typeof(IFingerprint), CrossFingerprint.Current);
            builder.Services.AddSingleton<AsistenciaDBContext>();
            builder.Services.AddSingleton<AsistenciaVM>();
            builder.Services.AddSingleton<AsistenciaPage>();
            // Inyectar Singleton de plugin
            builder.Services.AddSingleton<IBiometric>(BiometricAuthenticationService.Default);
            return builder.Build();
        }
    }
}

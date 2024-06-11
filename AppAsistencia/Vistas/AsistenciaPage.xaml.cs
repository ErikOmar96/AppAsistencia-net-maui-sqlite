using AppAsistencia.DataAccess;
using AppAsistencia.Modelos;
using AppAsistencia.VistaModelos;
// Using del paquete Plugin.Maui.Biometric
using Plugin.Maui.Biometric;

namespace AppAsistencia.Vistas;

public partial class AsistenciaPage : ContentPage
{
    // Variable para referenciar a la base de datos
    private readonly AsistenciaDBContext _context;
    private bool pulsacionLarga;
    private DateTime pressStartTime;

    public AsistenciaPage(AsistenciaDBContext context)
    {
        InitializeComponent();
        _context = context;
        btnMarcarAsistencia.IsEnabled = false;
    }


    private async void imgAsistencia_Pressed(object sender, EventArgs e)
    {
        pulsacionLarga = true;
        pressStartTime = DateTime.Now;
        await Task.Delay(3000);

        if (pulsacionLarga && (DateTime.Now - pressStartTime).TotalMilliseconds >= 3000)
        {
            await DisplayAlert("AVISO", "Asistencia marcada correctamente", "OK");
            // Pulsación larga exitosa, registrar asistencia
            var asistencia = new Asistencia
            {
                FechaAsistencia = DateTime.Now,
                EstadoAsistencia = "Presente",
                TextoAsistencia = "Asistencia marcada con pulsación larga",
                IdUsuario = 1 // Asigna el IdUsuario correspondiente, asegúrate de que sea correcto
            };

            try
            {
                bool isAdded = await _context.AddItemAsync(asistencia);

                if (isAdded)
                {
                    await DisplayAlert("Éxito", "Asistencia marcada correctamente.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al registrar la asistencia.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
            // Habilitar botón
            btnMarcarAsistencia.IsEnabled = true;
        }
        else
        {
            await DisplayAlert("Pulsación Corta", "La pulsación ha sido corta", "Aceptar");
        }
    }

    private void imgAsistencia_Released(object sender, EventArgs e)
    {
        pulsacionLarga = false;
    }

    private async void btnMarcarAsistencia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuPage(_context));
    }

    private async void btnLectorHuella_Clicked(object sender, EventArgs e)
    {
        // Variable para guardar el resultado
        var resultado = await BiometricAuthenticationService.Default.AuthenticateAsync(new AuthenticationRequest()
        { 
            Title = "Por favor, usa el lector de huella para autenticar la asistencia",
            NegativeText = "Autenticación cancelada"
        }, CancellationToken.None);

        // Validar resultado de la autenticación
        if (resultado.Status == BiometricResponseStatus.Success)
        {
            await DisplayAlert("EXITO", "Autenticación exitosa", "OK");
            var asistencia = new Asistencia 
            {
                FechaAsistencia = DateTime.Now,
                EstadoAsistencia = "Presente",
                TextoAsistencia = "Asistencia marcada con huella digital",
                IdUsuario = 1 // Asigna el IdUsuario correspondiente, asegúrate de que sea correcto
            };
            try
            {
                bool isAdded = await _context.AddItemAsync(asistencia);

                if (isAdded)
                {
                    await DisplayAlert("Éxito", "Asistencia marcada correctamente.", "OK");
                    await Navigation.PushAsync(new MenuPage(_context));
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al registrar la asistencia.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }
        else
        {
            await DisplayAlert("ERROR", "No se pudo autenticar la huella", "OK");
        }
    }
}
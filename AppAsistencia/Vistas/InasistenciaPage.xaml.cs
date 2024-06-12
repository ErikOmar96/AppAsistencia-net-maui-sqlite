using AppAsistencia.DataAccess;
using AppAsistencia.Modelos;
using Plugin.Maui.Biometric;

namespace AppAsistencia.Vistas;

public partial class InasistenciaPage : ContentPage
{
    // Variable para referenciar a la base de datos
    private readonly AsistenciaDBContext _context;
    private readonly Usuario _usuarioAutenticado;
    private bool pulsacionLargaInasistencia;
    private DateTime pressStartTime;

    public InasistenciaPage(AsistenciaDBContext context, Usuario usuarioAutenticado)
    {
        InitializeComponent();
        _context = context;
        btnJustificarInasistencia.IsEnabled = false;
        _usuarioAutenticado = usuarioAutenticado;
    }

    private async void imgInasistencia_Pressed(object sender, EventArgs e)
    {
        pulsacionLargaInasistencia = true;
        pressStartTime = DateTime.Now;
        await Task.Delay(3000);

        if (string.IsNullOrEmpty(txtInasistencia.Text))
        {
            await DisplayAlert("ERROR", "Debes colocar un motivo de la inasistencia", "OK");
            return;
        }
        else
        {
            if (pulsacionLargaInasistencia && (DateTime.Now - pressStartTime).TotalMilliseconds >= 3000)
            {
                await DisplayAlert("AVISO", "Inasistencia justificada correctamente", "OK");

                var inasistencia = new Asistencia
                {
                    FechaAsistencia = DateTime.Now,
                    EstadoAsistencia = "Ausente",
                    TextoAsistencia = txtInasistencia.Text,
                    IdUsuario = 1 // Asigna el IdUsuario correspondiente, aseg�rate de que sea correcto
                };

                try
                {
                    bool isAdded = await _context.AddItemAsync(inasistencia);

                    if (isAdded)
                    {
                        await DisplayAlert("�xito", "Inasistencia justificada correctamente.", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Hubo un problema al justificar la inasistencia.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    await DisplayAlert("Error", $"Ocurri� un error: {ex.Message}", "OK");
                }

                btnJustificarInasistencia.IsEnabled = true;
            }
            else
            {
                await DisplayAlert("Pulsaci�n Corta", "La pulsaci�n ha sido corta", "Aceptar");
            }
        }

        
    }

    private void imgInasistencia_Released(object sender, EventArgs e)
    {
        pulsacionLargaInasistencia = false;
    }

    private async void btnJustificarInasistencia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuPage(_context, _usuarioAutenticado));
    }

    private async void btnLectorHuellaInasistencia_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtInasistencia.Text))
        {
            await DisplayAlert("ERROR", "Debes colocar un motivo de la inasistencia", "OK");
            return;
        }
        else 
        {
            // Variable para guardar el resultado
            var resultado = await BiometricAuthenticationService.Default.AuthenticateAsync(new AuthenticationRequest()
            {
                Title = "Por favor, usa el lector de huella para autenticar la inasistencia",
                NegativeText = "Autenticaci�n cancelada"
            }, CancellationToken.None);

            // Validar resultado de la autenticaci�n
            if (resultado.Status == BiometricResponseStatus.Success)
            {
                await DisplayAlert("EXITO", "Autenticaci�n exitosa", "OK");
                var asistencia = new Asistencia
                {
                    FechaAsistencia = DateTime.Now,
                    EstadoAsistencia = "Atrasado",
                    TextoAsistencia = txtInasistencia.Text,
                    IdUsuario = 1 // Asigna el IdUsuario correspondiente, aseg�rate de que sea correcto
                };
                try
                {
                    bool isAdded = await _context.AddItemAsync(asistencia);

                    if (isAdded)
                    {
                        await DisplayAlert("�xito", "Inasistencia justificada correctamente.", "OK");
                        await Navigation.PushAsync(new MenuPage(_context, _usuarioAutenticado));
                    }
                    else
                    {
                        await DisplayAlert("Error", "Hubo un problema al justificar la inasistencia.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    await DisplayAlert("Error", $"Ocurri� un error: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("ERROR", "No se pudo autenticar la huella", "OK");
            }
        }  
    }
}
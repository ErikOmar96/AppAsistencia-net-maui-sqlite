using AppAsistencia.DataAccess;
using AppAsistencia.Modelos;
using Plugin.Maui.Biometric;


namespace AppAsistencia.Vistas;

public partial class TardanzaPage : ContentPage
{
    private readonly AsistenciaDBContext _dbContext;
    private readonly Usuario _usuarioAutenticado;
    private bool pulsacionLargaTardanza;
    private DateTime pressStartTime;

    public TardanzaPage(AsistenciaDBContext dBContext, Usuario usuarioAutenticado)
	{
		InitializeComponent();
        _dbContext = dBContext;
        _usuarioAutenticado = usuarioAutenticado;
        btnJustificarTardanza.IsEnabled = false;
	}
     
    private async void btnJustificarTardanza_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuPage(_dbContext, _usuarioAutenticado));
    }

    private async void imgTardanza_Pressed(object sender, EventArgs e)
    {
        pulsacionLargaTardanza = true;
        pressStartTime = DateTime.Now;
        await Task.Delay(3000);

        // && !string.IsNullOrEmpty(txtTardanza.Text)
        if (string.IsNullOrEmpty(txtTardanza.Text))
        {
            await DisplayAlert("ERROR", "Debes colocar un motivo de la tardanza", "OK");
            return;
        }
        else
        {
            if (pulsacionLargaTardanza && (DateTime.Now - pressStartTime).TotalMilliseconds >= 3000)
            {
                await DisplayAlert("AVISO", "Tardanza justificada correctamente", "OK");

                var tardanza = new Asistencia
                {
                    FechaAsistencia = DateTime.Now,
                    EstadoAsistencia = "Atrasado",
                    TextoAsistencia = txtTardanza.Text,
                    IdUsuario = 1 // Asigna el IdUsuario correspondiente, asegúrate de que sea correcto
                };

                try
                {
                    bool isAdded = await _dbContext.AddItemAsync(tardanza);

                    if (isAdded)
                    {
                        await DisplayAlert("Éxito", "Tardanza justificada correctamente.", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Hubo un problema al registrar la tardanza.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones
                    await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
                }

                btnJustificarTardanza.IsEnabled = true;
            }
            else
            {
                await DisplayAlert("Pulsación Corta", "La pulsación ha sido corta", "Aceptar");
            }
        }    
    }

    private void imgTardanza_Released(object sender, EventArgs e)
    {
        pulsacionLargaTardanza = false;
    }

    private async void btnLectorHuellaTardanza_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtTardanza.Text))
        {
            await DisplayAlert("ERROR", "Debes colocar un motivo de la inasistencia", "OK");
            return;
        }
        else
        {
            // Variable para guardar el resultado
            var resultado = await BiometricAuthenticationService.Default.AuthenticateAsync(new AuthenticationRequest()
            {
                Title = "Por favor, usa el lector de huella para autenticar la tardanza",
                NegativeText = "Autenticación cancelada"
            }, CancellationToken.None);

            // Validar resultado de la autenticación
            if (resultado.Status == BiometricResponseStatus.Success)
            {
                await DisplayAlert("EXITO", "Autenticación exitosa", "OK");
                var asistencia = new Asistencia
                {
                    FechaAsistencia = DateTime.Now,
                    EstadoAsistencia = "Atrasado",
                    TextoAsistencia = txtTardanza.Text,
                    IdUsuario = 1 // Asigna el IdUsuario correspondiente, asegúrate de que sea correcto
                };
                try
                {
                    bool isAdded = await _dbContext.AddItemAsync(asistencia);

                    if (isAdded)
                    {
                        await DisplayAlert("Éxito", "Tardanza justificada correctamente.", "OK");
                        await Navigation.PushAsync(new MenuPage(_dbContext, _usuarioAutenticado));
                    }
                    else
                    {
                        await DisplayAlert("Error", "Hubo un problema al justificar la tardanza.", "OK");
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
}
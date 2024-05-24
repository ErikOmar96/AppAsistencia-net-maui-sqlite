using AppAsistencia.DataAccess;
using AppAsistencia.Modelos;

namespace AppAsistencia.Vistas;

public partial class TardanzaPage : ContentPage
{
    private readonly AsistenciaDBContext _dbContext;
    private bool pulsacionLargaTardanza;
    private DateTime pressStartTime;

    public TardanzaPage(AsistenciaDBContext dBContext)
	{
		InitializeComponent();
        _dbContext = dBContext;
        btnJustificarTardanza.IsEnabled = false;
	}
     
    private async void btnJustificarTardanza_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuPage(_dbContext));
    }

    private async void imgTardanza_Pressed(object sender, EventArgs e)
    {
        pulsacionLargaTardanza = true;
        pressStartTime = DateTime.Now;
        await Task.Delay(3000);

        // && !string.IsNullOrEmpty(txtTardanza.Text)
        if (pulsacionLargaTardanza && (DateTime.Now - pressStartTime).TotalMilliseconds >= 3000 && !string.IsNullOrEmpty(txtTardanza.Text))
        {
            await DisplayAlert("AVISO", "Tardanza justificada correctamente", "OK");

            var tardanza = new Asistencia
            {
                FechaAsistencia = DateTime.Now,
                EstadoAsistencia = "Tarde",
                TextoAsistencia = txtTardanza.Text,
                IdUsuario = 1 // Asigna el IdUsuario correspondiente, asegúrate de que sea correcto
            };

            try
            {
                bool isAdded = await _dbContext.AddItemAsync(tardanza);

                if (isAdded)
                {
                    await DisplayAlert("Éxito", "Tardanza registrada correctamente.", "OK");
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

            btnJustificarTardanza.IsEnabled = true;
        }
        else
        {
            await DisplayAlert("Pulsación Corta", "La pulsación ha sido corta", "Aceptar");
        }
    }

    private void imgTardanza_Released(object sender, EventArgs e)
    {
        pulsacionLargaTardanza = false;
    }
}
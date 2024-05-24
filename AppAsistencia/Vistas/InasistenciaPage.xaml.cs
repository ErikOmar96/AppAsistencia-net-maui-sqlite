using AppAsistencia.DataAccess;
using AppAsistencia.Modelos;

namespace AppAsistencia.Vistas;

public partial class InasistenciaPage : ContentPage
{
    // Variable para referenciar a la base de datos
    private readonly AsistenciaDBContext _context;
    private bool pulsacionLargaInasistencia;
    private DateTime pressStartTime;

    public InasistenciaPage(AsistenciaDBContext context)
	{
		InitializeComponent();
        _context = context;
        btnJustificarInasistencia.IsEnabled = false;
	}
 
    private async void imgInasistencia_Pressed(object sender, EventArgs e)
    {
        pulsacionLargaInasistencia = true;
        pressStartTime = DateTime.Now;
        await Task.Delay(3000);

        // && !string.IsNullOrEmpty(txtInasistencia.Text)
        if (pulsacionLargaInasistencia && (DateTime.Now - pressStartTime).TotalMilliseconds >= 3000 && !string.IsNullOrEmpty(txtInasistencia.Text))
        {
            await DisplayAlert("AVISO", "Inasistencia justificada correctamente", "OK");

            var inasistencia = new Asistencia
            {
                FechaAsistencia = DateTime.Now,
                EstadoAsistencia = "Ausente",
                TextoAsistencia = txtInasistencia.Text,
                IdUsuario = 1 // Asigna el IdUsuario correspondiente, asegúrate de que sea correcto
            };

            try
            {
                bool isAdded = await _context.AddItemAsync(inasistencia);

                if (isAdded)
                {
                    await DisplayAlert("Éxito", "Inasistencia registrada correctamente.", "OK");
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

            btnJustificarInasistencia.IsEnabled = true;
        }
        else
        {
            await DisplayAlert("Pulsación Corta", "La pulsación ha sido corta", "Aceptar");
        }
    }

    private void imgInasistencia_Released(object sender, EventArgs e)
    {
        pulsacionLargaInasistencia = false;
    }

    private async void btnJustificarInasistencia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuPage(_context));
    }
}
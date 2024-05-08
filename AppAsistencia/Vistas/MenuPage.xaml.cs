using Plugin.Fingerprint.Abstractions;

namespace AppAsistencia.Vistas;

public partial class MenuPage : ContentPage
{
    public MenuPage()
	{
		InitializeComponent();

        // Llamar al método para actualizar el estado del botón de marcar asistencia
        //ActualizarEstadoBotonAsistencia();

    }

    private async void btnMarcarAsistencia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AsistenciaPage()); // Necesita un paráemtro IFingerprint
    }

    private async void btnJustificarTardanza_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TardanzaPage());
    }

    private async void btnJustificarInasistencia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InasistenciaPage());
    }

    private async void btnVerAsistencias_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VerAsistenciasPage());
    }

    private async void btnActualizarDatos_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ActualizarDatosPage());
    }

    private async  void btnSalir_Clicked(object sender, EventArgs e)
    {
        // Mostrar una ventana emergente de confirmación antes de salir de la aplicación
        bool confirmacion = await Application.Current.MainPage.DisplayAlert("Confirmación", "¿Estás seguro de que deseas salir de la aplicación?", "Sí", "Cancelar");

        if (confirmacion)
        {
            // El usuario confirmó salir de la aplicación, así que cierra la aplicación
            Application.Current.Quit();
        }
    }

    // Cambiar estado del botón Asistencia
    private void ActualizarEstadoBotonAsistencia()
    {
        // Obtener la hora actual del dispositivo
        DateTime horaActual = DateTime.Now;

        // Definir la hora actual de entrada permitida y el límite de tiempo
        TimeSpan horaEntrada = new TimeSpan(9, 0, 0); // 9:00 AM
        TimeSpan limiteTiempo = new TimeSpan(0, 5, 0); // 5 Minutos de tolerancia

        // Calcular la hora límite para marcar asistencia
        DateTime horaLimite = horaActual.Date.Add(horaEntrada).Add(limiteTiempo);

        // Verificar si aún se encuentra dentro del límite de tiempo para marcar asistencia
        bool dentroLimiteTiempo = horaActual <= horaLimite;

        // Habilitar o inhabilitar el botón de marcar asistencia según el estado
        btnMarcarAsistencia.IsEnabled = dentroLimiteTiempo;

        if (!dentroLimiteTiempo)
        {
            DisplayAlert("AVISO", "Ya no puedes marcar asistencia", "OK");
        }
    }
}
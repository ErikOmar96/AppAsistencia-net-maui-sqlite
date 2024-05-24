//using Plugin.Fingerprint.Abstractions;

using AppAsistencia.DataAccess;

namespace AppAsistencia.Vistas;

public partial class MenuPage : ContentPage
{
    private readonly AsistenciaDBContext _context;

    public MenuPage(AsistenciaDBContext context)
	{
		InitializeComponent();
        _context = context;
        // Llamar al m�todo para actualizar el estado del bot�n de marcar asistencia
        //ActualizarEstadoBotonAsistencia();

    }

    private async void btnMarcarAsistencia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AsistenciaPage(_context)); // Aqu� es el error
    }

    private async void btnJustificarTardanza_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TardanzaPage(_context));
    }

    private async void btnJustificarInasistencia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InasistenciaPage(_context));
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
        // Mostrar una ventana emergente de confirmaci�n antes de salir de la aplicaci�n
        bool confirmacion = await Application.Current.MainPage.DisplayAlert("Confirmaci�n", "�Est�s seguro de que deseas salir de la aplicaci�n?", "S�", "Cancelar");

        if (confirmacion)
        {
            // El usuario confirm� salir de la aplicaci�n, as� que cierra la aplicaci�n
            Application.Current.Quit();
        }
    }

    // Cambiar estado del bot�n Asistencia
    private void ActualizarEstadoBotonAsistencia()
    {
        // Obtener la hora actual del dispositivo
        DateTime horaActual = DateTime.Now;

        // Definir la hora actual de entrada permitida y el l�mite de tiempo
        TimeSpan horaEntrada = new TimeSpan(9, 0, 0); // 9:00 AM
        TimeSpan limiteTiempo = new TimeSpan(0, 5, 0); // 5 Minutos de tolerancia

        // Calcular la hora l�mite para marcar asistencia
        DateTime horaLimite = horaActual.Date.Add(horaEntrada).Add(limiteTiempo);

        // Verificar si a�n se encuentra dentro del l�mite de tiempo para marcar asistencia
        bool dentroLimiteTiempo = horaActual <= horaLimite;

        // Habilitar o inhabilitar el bot�n de marcar asistencia seg�n el estado
        btnMarcarAsistencia.IsEnabled = dentroLimiteTiempo;

        if (!dentroLimiteTiempo)
        {
            DisplayAlert("AVISO", "Ya no puedes marcar asistencia", "OK");
        }
    }
}